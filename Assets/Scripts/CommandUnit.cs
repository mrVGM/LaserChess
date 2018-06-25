using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CommandUnit : AIPiece
{
    public CommandUnit(MonoBehaviour mb) : base(mb)
    {
        damage = 0;
        hitPoints = 5;
    }

    public override List<Piece> GetAttackPossibilities(out bool requireChoice)
    {
        requireChoice = false;
        return new List<Piece>();
    }

    public override List<Tile> GetPosibleMoves()
    {
        List<Tile> res = new List<Tile>();
        if (x > 0 && Game.instance.pieces[x - 1, y] == null)
            res.Add(Game.instance.board[x - 1, y]);
        if (x < 7 && Game.instance.pieces[x + 1, y] == null)
            res.Add(Game.instance.board[x + 1, y]);
        return res;
    }
}
