﻿using System;
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
    }

    public override void markPosibleMoves()
    {
        int right = Math.Min(7 - x, 2);
        int left = Math.Min(x, 2);
        int up = Math.Min(7 - y, 2);
        int down = Math.Min(y, 2);

        for (int i = 0; i <= right; ++i)
        {
            for (int j = 0; j <= up; ++j)
            {
                if (i == 0 && j == 0)
                    continue;
                if ((i == 2 || j == 2) && (i - j) % 2 != 0)
                    continue;
                if (Game.instance.pieces[x + i, y + j] == null)
                    Game.instance.board[x + i, y + j].Select();
            }
        }

        for (int i = 0; i <= left; ++i)
        {
            for (int j = 0; j <= up; ++j)
            {
                if (i == 0 && j == 0)
                    continue;
                if ((i == 2 || j == 2) && (i - j) % 2 != 0)
                    continue;
                if (Game.instance.pieces[x - i, y + j] == null)
                    Game.instance.board[x - i, y + j].Select();
            }
        }

        for (int i = 0; i <= right; ++i)
        {
            for (int j = 0; j <= down; ++j)
            {
                if (i == 0 && j == 0)
                    continue;
                if ((i == 2 || j == 2) && (i - j) % 2 != 0)
                    continue;
                if (Game.instance.pieces[x + i, y - j] == null)
                    Game.instance.board[x + i, y - j].Select();
            }
        }

        for (int i = 0; i <= left; ++i)
        {
            for (int j = 0; j <= down; ++j)
            {
                if (i == 0 && j == 0)
                    continue;
                if ((i == 2 || j == 2) && (i - j) % 2 != 0)
                    continue;
                if (Game.instance.pieces[x - i, y - j] == null)
                    Game.instance.board[x - i, y - j].Select();
            }
        }
    }
}