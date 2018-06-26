using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace States.Human
{
    class MoveAnimation : State
    {
        HumanPiece piece;
        public MoveAnimation(HumanPiece piece, Vector2Int position)
        {
            this.piece = piece;
            piece.Move(position.x, position.y);
        }

        public void Update()
        {
            if (MovementAnimation.movementAnimation.IsAnimating())
                return;

            piece.active = false;
            Game.instance.currentState = new Attack(piece);
        }
    }
}
