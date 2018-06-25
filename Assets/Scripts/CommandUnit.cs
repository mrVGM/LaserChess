using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CommandUnit : AIPiece
{
    public static HashSet<CommandUnit> CommandUnits = new HashSet<CommandUnit>();
    public CommandUnit(MonoBehaviour mb) : base(mb)
    {
        damage = 0;
        hitPoints = 5;
        CommandUnits.Add(this);
    }

    public override void Destroy()
    {
        Game.instance.pieces[x, y] = null;
        MonoBehaviour.Destroy(monoBehaviour.gameObject);
        CommandUnits.Remove(this);
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

    public override bool MakeMoveAndAttack()
    {
        if (!active)
            return false;

        List<Tile> moves = GetPosibleMoves();
        if (moves.Count == 0)
            return false;

        active = false;
        if (moves.Count > 1)
            throw new NotImplementedException();

        Move(moves[0].x, moves[0].y);
        return true;
    }
}
