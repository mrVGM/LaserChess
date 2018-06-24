using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Jumpship : HumanPiece
{
    public Jumpship(MonoBehaviour mb) : base(mb)
    {
        damage = 2;
        hitPoints = 2;
    }

    public override List<Piece> GetAttackPossibilities(out bool requireChoice)
    {
        requireChoice = false;
        List<Piece> res = new List<Piece>();

        if (x > 0 && Game.instance.pieces[x - 1, y] as AIPiece != null)
            res.Add(Game.instance.pieces[x - 1, y]);
        if (x < 7 && Game.instance.pieces[x + 1, y] as AIPiece != null)
            res.Add(Game.instance.pieces[x + 1, y]);
        if (y > 0 && Game.instance.pieces[x, y - 1] as AIPiece != null)
            res.Add(Game.instance.pieces[x, y - 1]);
        if (y < 7 && Game.instance.pieces[x, y + 1] as AIPiece != null)
            res.Add(Game.instance.pieces[x, y + 1]);

        return res;
    }

    public override List<Tile> GetPosibleMoves()
    {
        List<Tile> res = new List<Tile>();
        if (y - 2 >= 0)
        {
            if (x - 1 >= 0 && Game.instance.pieces[x - 1, y - 2] == null)
                res.Add(Game.instance.board[x - 1, y - 2]);

            if (x + 1 <= 7 && Game.instance.pieces[x + 1, y - 2] == null)
                res.Add(Game.instance.board[x + 1, y - 2]);
        }

        if (y + 2 <= 7)
        {
            if (x - 1 >= 0 && Game.instance.pieces[x - 1, y + 2] == null)
                res.Add(Game.instance.board[x - 1, y + 2]);

            if (x + 1 <= 7 && Game.instance.pieces[x + 1, y + 2] == null)
                res.Add(Game.instance.board[x + 1, y + 2]);
        }

        if (x - 2 >= 0)
        {
            if (y - 1 >= 0 && Game.instance.pieces[x - 2, y - 1] == null)
                res.Add(Game.instance.board[x - 2, y - 1]);

            if (y + 1 <= 7 && Game.instance.pieces[x - 2, y + 1] == null)
                res.Add(Game.instance.board[x - 2, y + 1]);
        }

        if (x + 2 <= 7)
        {
            if (y - 1 >= 0 && Game.instance.pieces[x + 2, y - 1] == null)
                res.Add(Game.instance.board[x + 2, y - 1]);

            if (y + 1 <= 7 && Game.instance.pieces[x + 2, y + 1] == null)
                res.Add(Game.instance.board[x + 2, y + 1]);
        }
        return res;
    }
}
