using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace States.AI
{
    class MoveCommandUnit : State
    {
        CommandUnit commandUnit;
        List<Tile> moves;

        public MoveCommandUnit(CommandUnit commandUnit, List<Tile> moves)
        {
            this.commandUnit = commandUnit;
            this.moves = moves;
        }

        public void Update()
        {
            List<Tile> moves = commandUnit.GetPosibleMoves();
            if (moves.Count == 0)
                return;
            
            Vector2Int bestMove = new Vector2Int(commandUnit.x, commandUnit.y);
            int minPossibleDamage = commandUnit.EstimateDamage(Game.instance.board[commandUnit.x, commandUnit.y]);

            foreach (Tile move in moves)
            {
                int estimatedDamage = commandUnit.EstimateDamage(move);
                if (estimatedDamage < minPossibleDamage)
                {
                    minPossibleDamage = estimatedDamage;
                    bestMove.x = move.x;
                    bestMove.y = move.y;
                }
            }

            moves[0] = Game.instance.board[bestMove.x, bestMove.y];

            if (moves[0].x != commandUnit.x || moves[0].y != commandUnit.y)
            {
                Game.instance.currentState = new MoveCommandUnitAnimation(commandUnit, new Vector2Int(moves[0].x, moves[0].y));
            }
            else
            {
                commandUnit.active = false;
                Game.instance.currentState = new ChooseCommandUnit();
            }

        }
    }
}
