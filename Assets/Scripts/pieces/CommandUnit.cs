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

    int PossibleDamage(Tile position, HumanPiece humanPiece)
    {
        int res = 0;

        Vector2Int myPos = new Vector2Int(x, y);
        Vector2Int enemyPosition = new Vector2Int(humanPiece.x, humanPiece.y);

        Move(position.x, position.y, false);

        List<Tile> possibleMoves = humanPiece.GetPosibleMoves();

        foreach (Tile move in possibleMoves)
        {
            humanPiece.Move(move.x, move.y, false);
            bool requireChoice;
            List<Piece> attacksPossibilities = humanPiece.GetAttackPossibilities(out requireChoice);
            if (attacksPossibilities.Contains(this))
            {
                res += humanPiece.damage;
                break;
            }
        }

        Move(myPos.x, myPos.y, false);
        humanPiece.Move(enemyPosition.x, enemyPosition.y, false);

        return res;
    }

    public int EstimateDamage(Tile position)
    {
        int res = 0;
        foreach (HumanPiece humanPiece in HumanPiece.HumanPieces)
            res += PossibleDamage(position, humanPiece);
        return res;
    }
}
