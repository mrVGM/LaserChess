﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dreadnought : AIPiece
{
    public static HashSet<Dreadnought> Dreadnoughts = new HashSet<Dreadnought>();

    public Dreadnought(MonoBehaviour mb) : base(mb)
    {
        damage = 2;
        hitPoints = 5;
        Dreadnoughts.Add(this);
    }

    public override void Destroy()
    {
        Game.instance.pieces[x, y] = null;
        MonoBehaviour.Destroy(monoBehaviour.gameObject);
        Dreadnoughts.Remove(this);
    }

    public override List<Piece> GetAttackPossibilities(out bool requireChoice)
    {
        List<Piece> res = new List<Piece>();
        for (int k = 0; k < 9; ++k)
        {
            if (k == 4)
                continue;
            Vector2Int adjacentLocation = new Vector2Int(k / 3, k % 3) - new Vector2Int(1, 1) + new Vector2Int(x, y);

            try
            {
                HumanPiece piece = Game.instance.pieces[adjacentLocation.x, adjacentLocation.y] as HumanPiece;
                if (piece != null)
                    res.Add(piece);
            }
            catch (Exception)
            { }
        }
        if (res.Count > 1)
            requireChoice = true;
        else
            requireChoice = false;

        return res;
    }

    public override List<Tile> GetPosibleMoves()
    {
        List<Tile> res = new List<Tile>();
        for (int k = 0; k < 9; ++k)
        {
            if (k == 4)
                continue;
            Vector2Int move = new Vector2Int(k / 3, k % 3) - new Vector2Int(1, 1) + new Vector2Int(x, y);

            try
            {
                if (Game.instance.pieces[move.x, move.y] == null)
                    res.Add(Game.instance.board[move.x, move.y]);
            }
            catch (Exception)
            { }
        }
        return res;
    }

    HumanPiece getNearestEnemy()
    {
        double minDist = 1000;
        HumanPiece nearest = null;

        foreach (HumanPiece humanPiece in HumanPiece.HumanPieces)
        {
            Vector3 offset = humanPiece.monoBehaviour.transform.position - monoBehaviour.transform.position;
            if (offset.magnitude < minDist)
            {
                minDist = offset.magnitude;
                nearest = humanPiece;
            }
        }
        return nearest;
    }

    public override bool MakeMoveAndAttack()
    {
        if (!active)
            return false;

        List<Tile> moves = GetPosibleMoves();
        if (moves.Count == 0)
            return false;

        if (moves.Count > 1)
        {
            HumanPiece nearest = getNearestEnemy();
            Tile enemyPosition = Game.instance.board[nearest.x, nearest.y];
            double minDist = 100;
            Tile bestMove = null;
            foreach (Tile move in moves)
            {
                Vector3 offset = enemyPosition.transform.position - move.transform.position;
                if (offset.magnitude < minDist)
                {
                    minDist = offset.magnitude;
                    bestMove = move;
                }
            }

            moves.Clear();
            moves.Add(bestMove);
        }
        

        Move(moves[0].x, moves[0].y);
        active = false;

        bool requireChoice;
        List<Piece> attackPossibilities = GetAttackPossibilities(out requireChoice);

        Attack(attackPossibilities);
        return true;
    }
}
