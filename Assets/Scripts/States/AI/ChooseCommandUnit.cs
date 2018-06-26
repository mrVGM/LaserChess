using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.AI
{
    class ChooseCommandUnit : State
    {
        public void Update()
        {
            foreach (CommandUnit commandUnit in CommandUnit.CommandUnits)
            {
                if (commandUnit.active)
                {
                    List<Tile> possibleMoves = commandUnit.GetPosibleMoves();
                    if (possibleMoves.Count > 0)
                    {
                        Game.instance.currentState = new MoveCommandUnit(commandUnit, possibleMoves);
                        return;
                    }
                }
            }

            Game.instance.currentState = new Human.BeginTurn();
        }
    }
}
