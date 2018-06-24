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

    public override void markPosibleMoves()
    {
        if (y - 2 >= 0)
        {
            if (x - 1 >= 0 && Game.instance.pieces[x - 1, y - 2] == null)
                Game.instance.board[x - 1, y - 2].Select();

            if (x + 1 <= 7 && Game.instance.pieces[x + 1, y - 2] == null)
                Game.instance.board[x + 1, y - 2].Select();
        }

        if (y + 2 <= 7)
        {
            if (x - 1 >= 0 && Game.instance.pieces[x - 1, y + 2] == null)
                Game.instance.board[x - 1, y + 2].Select();

            if (x + 1 <= 7 && Game.instance.pieces[x + 1, y + 2] == null)
                Game.instance.board[x + 1, y + 2].Select();
        }

        if (x - 2 >= 0)
        {
            if (y - 1 >= 0 && Game.instance.pieces[x - 2, y - 1] == null)
                Game.instance.board[x - 2, y - 1].Select();

            if (y + 1 <= 7 && Game.instance.pieces[x - 2, y + 1] == null)
                Game.instance.board[x - 2, y + 1].Select();
        }

        if (x + 2 <= 7)
        {
            if (y - 1 >= 0 && Game.instance.pieces[x + 2, y - 1] == null)
                Game.instance.board[x + 2, y - 1].Select();

            if (y + 1 <= 7 && Game.instance.pieces[x + 2, y + 1] == null)
                Game.instance.board[x + 2, y + 1].Select();
        }
    }
}
