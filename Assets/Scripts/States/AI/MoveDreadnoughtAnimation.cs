using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace States.AI
{
    class MoveDreadnoughtAnimation : State
    {
        Dreadnought dreadnought;
        Vector2Int position;
        public MoveDreadnoughtAnimation(Dreadnought dreadnought, Vector2Int position)
        {
            this.dreadnought = dreadnought;
            this.position = position;

            dreadnought.Move(position.x, position.y);
        }
        public void Update()
        {
            if (MovementAnimation.movementAnimation.IsAnimating())
                return;
            dreadnought.active = false;
            Game.instance.currentState = new DreadnoughtAttack(dreadnought);
        }
    }
}
