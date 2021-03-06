﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Grunt : HumanPiece
{
    public Grunt(MonoBehaviour mb) : base(mb)
    {
        damage = 1;
        hitPoints = 2;
        maxHealth = 2;
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
        if (x > 0 && Game.instance.pieces[x - 1,y] == null)
        {
            res.Add(Game.instance.board[x - 1, y]);
        }
        if (x < 7 && Game.instance.pieces[x + 1, y] == null)
        {
            res.Add(Game.instance.board[x + 1, y]);
        }
        if (y > 0 && Game.instance.pieces[x, y - 1] == null)
        {
            res.Add(Game.instance.board[x, y - 1]);
        }
        if (y < 7 && Game.instance.pieces[x, y + 1] == null)
        {
            res.Add(Game.instance.board[x, y + 1]);
        }
        return res;
    }
}

