using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.Human
{
    class BeginTurn : State
    {
        public void Update()
        {
            Game.instance.SetHumanPiecesActive();
            Game.instance.currentState = new ActivePieces();
        }
    }
}
