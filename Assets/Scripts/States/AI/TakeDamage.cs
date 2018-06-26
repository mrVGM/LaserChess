using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.AI
{
    class TakeDamage : State
    {
        public enum NextAction
        {
            ChooseDrone,
            ChooseDreadnought,
            ChooseCommandUnit,
            EndTurn
        };
        NextAction nextAction;
        List<HumanPiece> targets;
        int hitPoints;
        public TakeDamage(int hitPoints, List<HumanPiece> targets, NextAction next)
        {
            nextAction = next;
            this.targets = targets;
            this.hitPoints = hitPoints;
        }

        public void Update()
        {
            foreach (HumanPiece humanPiece in targets)
            {
                humanPiece.TakeDamage(hitPoints);
            }
            switch (nextAction)
            {
                case NextAction.ChooseDrone:
                    Game.instance.currentState = new ChooseDrone();
                    break;
                case NextAction.ChooseDreadnought:
                    Game.instance.currentState = new ChooseDreadnought();
                    break;
                case NextAction.ChooseCommandUnit:
                    throw new NotImplementedException();
                    break;
                case NextAction.EndTurn:
                    Game.instance.currentState = new Human.BeginTurn();
                    break;
            }
        }
    }
}
