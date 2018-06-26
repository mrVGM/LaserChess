using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Drone : AIPiece
{
    public static HashSet<Drone> Drones = new HashSet<Drone>();
    public Drone(MonoBehaviour mb) : base(mb)
    {
        damage = 1;
        hitPoints = 2;
        Drones.Add(this);
    }

    public override void Destroy()
    {
        Game.instance.pieces[x, y] = null;
        MonoBehaviour.Destroy(monoBehaviour.gameObject);
        Drones.Remove(this);
    }

    public override List<Piece> GetAttackPossibilities(out bool requireChoice)
    {
        List<Piece> res = new List<Piece>();

        Vector2Int[] directions =
        {
            new Vector2Int(1,1),
            new Vector2Int(1,-1),
            new Vector2Int(-1,1),
            new Vector2Int(-1,-1)
        };

        foreach (Vector2Int dir in directions)
        {
            Vector2Int curPosition = new Vector2Int(x, y);

            try
            {
                Piece piece = null;
                do
                {
                    curPosition += dir;
                    piece = Game.instance.pieces[curPosition.x, curPosition.y];
                    if (piece != null && piece as HumanPiece != null)
                        res.Add(piece);

                } while (piece == null);
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
        if (y > 0 && Game.instance.pieces[x, y - 1] == null)
            res.Add(Game.instance.board[x, y - 1]);
        return res;
    }

    public override bool MakeMoveAndAttack()
    {
        if (!active)
            return false;

        List<Tile> moves = GetPosibleMoves();
        if (moves.Count == 0)
            return false;

        Move(moves[0].x, moves[0].y);
        active = false;

        bool requireChoice;
        List<Piece> attackPosibilities = GetAttackPossibilities(out requireChoice);

        if (requireChoice)
        {
            Piece bestChoice = attackPosibilities[0];
            if (attackPosibilities[1].y > attackPosibilities[0].y)
                bestChoice = attackPosibilities[1];

            attackPosibilities.Clear();
            attackPosibilities.Add(bestChoice);
        }

        if (attackPosibilities.Count == 1)
        {
            Attack(attackPosibilities);
        }
        return true;
    }
}
