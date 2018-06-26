using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Tank : HumanPiece
{
    public Tank(MonoBehaviour mb) : base(mb)
    {
        damage = 2;
        hitPoints = 4;
        maxHealth = 4;
    }

    public override List<Piece> GetAttackPossibilities(out bool requireChoice)
    {
        List<Piece> res = new List<Piece>();

        Vector2Int[] directions =
        {
            new Vector2Int(0,1),
            new Vector2Int(0,-1),
            new Vector2Int(1,0),
            new Vector2Int(-1,0)
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
                    if (piece != null && piece as AIPiece != null)
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

        for (int k = 0; k < 9; ++k)
        {
            if (k == 4)
                continue;
            Vector2Int dir = new Vector2Int(k / 3, k % 3) - new Vector2Int(1,1);
            Vector2Int curPosition = new Vector2Int(x, y);

            try
            {
                for (int i = 0; i < 3; ++i)
                {
                    curPosition += dir;
                    if (Game.instance.pieces[curPosition.x, curPosition.y] == null)
                        res.Add(Game.instance.board[curPosition.x, curPosition.y]);
                    else
                        break;
                }
            }
            catch (Exception)
            { }
        }

        return res;
    }
}