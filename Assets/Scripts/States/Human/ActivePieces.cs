using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.Human
{
    class ActivePieces : State
    {
        public void Update()
        {
            Dictionary<HumanPiece, List<Tile>> active = HumanPiece.ActivePieces();
            if (active.Count == 0)
            {
                Game.instance.currentState = new AI.BeginTurn();
                return;
            }

            foreach (KeyValuePair<HumanPiece, List<Tile>> entry in active)
            {
                entry.Key.Select();
            }

            Game.instance.currentState = new SelectPiece(active);
        }
    }
}
