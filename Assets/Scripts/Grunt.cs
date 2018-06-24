using System;
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
    }

    public override void markPosibleMoves()
    {
        if (x > 0 && Game.instance.pieces[x - 1,y] == null)
        {
            Game.instance.board[x - 1, y].Select();
        }
        if (x < 7 && Game.instance.pieces[x + 1, y] == null)
        {
            Game.instance.board[x + 1, y].Select();
        }
        if (y > 0 && Game.instance.pieces[x, y - 1] == null)
        {
            Game.instance.board[x, y - 1].Select();
        }
        if (y < 7 && Game.instance.pieces[x, y + 1] == null)
        {
            Game.instance.board[x, y + 1].Select();
        }
    }
}

