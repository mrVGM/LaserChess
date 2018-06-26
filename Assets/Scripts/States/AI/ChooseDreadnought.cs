using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.AI
{
    class ChooseDreadnought : State
    {
        public void Update()
        {
            foreach (Dreadnought dreadnought in Dreadnought.Dreadnoughts)
            {
                if (dreadnought.active)
                {
                    List<Tile> possibleMoves = dreadnought.GetPosibleMoves();
                    if (possibleMoves.Count > 0)
                    {
                        Game.instance.currentState = new MoveDreadnought(dreadnought, possibleMoves);
                        return;
                    }
                }
            }

            Game.instance.currentState = new ChooseCommandUnit();
        }
    }
}
