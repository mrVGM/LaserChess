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
        maxHealth = 5;

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

    public HumanPiece getNearestEnemy()
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

}
