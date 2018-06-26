using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace States.AI
{
    class MoveDreadnought : State
    {
        Dreadnought dreadnought;
        List<Tile> moves;
        public MoveDreadnought(Dreadnought dreadnought, List<Tile> moves)
        {
            this.dreadnought = dreadnought;
            this.moves = moves;
        }

        public void Update()
        {
            if (moves.Count > 1)
            {
                HumanPiece nearest = dreadnought.getNearestEnemy();
                Tile enemyPosition = Game.instance.board[nearest.x, nearest.y];
                double minDist = 100;
                Tile bestMove = null;
                foreach (Tile move in moves)
                {
                    Vector3 offset = enemyPosition.transform.position - move.transform.position;
                    if (offset.magnitude < minDist)
                    {
                        minDist = offset.magnitude;
                        bestMove = move;
                    }
                }

                moves.Clear();
                moves.Add(bestMove);
            }
            
            Game.instance.currentState = new MoveDreadnoughtAnimation(dreadnought, new Vector2Int(moves[0].x, moves[0].y));
        }
    }
}
