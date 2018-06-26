using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace States.AI
{
    class MoveCommandUnitAnimation : State
    {
        CommandUnit commandUnit;
        Vector2Int position;

        public MoveCommandUnitAnimation(CommandUnit commandUnit, Vector2Int position)
        {
            this.commandUnit = commandUnit;
            this.position = position;
            commandUnit.Move(position.x, position.y);
        }

        public void Update()
        {
            if (MovementAnimation.movementAnimation.IsAnimating())
                return;
            commandUnit.active = false;
            Game.instance.currentState = new ChooseCommandUnit();
        }
    }
}
