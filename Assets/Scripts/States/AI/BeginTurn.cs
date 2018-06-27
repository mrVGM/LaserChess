using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.AI
{
    class BeginTurn : State
    {
        public void Update()
        {
            Game.instance.InfoPanel.SetActive(false);

            Game.instance.SetAIPiecesActive();
            Game.instance.currentState = new ChooseDrone();
        }
    }
}
