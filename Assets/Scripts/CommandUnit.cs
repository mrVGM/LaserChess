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
        {
            Vector2Int bestMove = new Vector2Int(x, y);
            int minPossibleDamage = EstimateDamage(Game.instance.board[x, y]);

            foreach (Tile move in moves)
            {
                int estimatedDamage = EstimateDamage(move);
                if (estimatedDamage < minPossibleDamage)
                {
                    minPossibleDamage = estimatedDamage;
                    bestMove.x = move.x;
                    bestMove.y = move.y;
                }
            }

            moves[0] = Game.instance.board[bestMove.x, bestMove.y];
        }

        Move(moves[0].x, moves[0].y);
        return true;
    }

    int PossibleDamage(Tile position, HumanPiece humanPiece)
    {
        int res = 0;

        Vector2Int myPos = new Vector2Int(x, y);
        Vector2Int enemyPosition = new Vector2Int(humanPiece.x, humanPiece.y);

        Move(position.x, position.y);

        List<Tile> possibleMoves = humanPiece.GetPosibleMoves();

        foreach (Tile move in possibleMoves)
        {
            humanPiece.Move(move.x, move.y);
            bool requireChoice;
            List<Piece> attacksPossibilities = humanPiece.GetAttackPossibilities(out requireChoice);
            if (attacksPossibilities.Contains(this))
            {
                res += humanPiece.damage;
                break;
            }
        }

        Move(myPos.x, myPos.y);
        humanPiece.Move(enemyPosition.x, enemyPosition.y);

        return res;
    }

    int EstimateDamage(Tile position)
    {
        int res = 0;
        foreach (HumanPiece humanPiece in HumanPiece.HumanPieces)
            res += PossibleDamage(position, humanPiece);
        return res;
    }
}
