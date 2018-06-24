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
    }

    public override List<Piece> GetAttackPossibilities(out bool requireChoice)
    {
        List<Piece> res = new List<Piece>();

        Vector2Int curPosition = new Vector2Int(x, y);
        Vector2Int dir = new Vector2Int(0, 1);

        try
        {
            AIPiece piece = null;
            do
            {
                curPosition += dir;
                piece = Game.instance.pieces[curPosition.x, curPosition.y] as AIPiece;
                if (piece != null)
                    res.Add(piece);

            } while (piece == null);
        }
        catch (Exception)
        { }

        curPosition.x = x;
        curPosition.y = y;
        dir.x = 0;
        dir.y = -1;

        try
        {
            AIPiece piece = null;
            do
            {
                curPosition += dir;
                piece = Game.instance.pieces[curPosition.x, curPosition.y] as AIPiece;
                if (piece != null)
                    res.Add(piece);

            } while (piece == null);
        }
        catch (Exception)
        { }

        curPosition.x = x;
        curPosition.y = y;
        dir.x = 1;
        dir.y = 0;

        try
        {
            AIPiece piece = null;
            do
            {
                curPosition += dir;
                piece = Game.instance.pieces[curPosition.x, curPosition.y] as AIPiece;
                if (piece != null)
                    res.Add(piece);

            } while (piece == null);
        }
        catch (Exception)
        { }

        curPosition.x = x;
        curPosition.y = y;
        dir.x = -1;
        dir.y = 0;

        try
        {
            AIPiece piece = null;
            do
            {
                curPosition += dir;
                piece = Game.instance.pieces[curPosition.x, curPosition.y] as AIPiece;
                if (piece != null)
                    res.Add(piece);

            } while (piece == null);
        }
        catch (Exception)
        { }

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