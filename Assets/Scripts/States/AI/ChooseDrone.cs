using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.AI
{
    class ChooseDrone : State
    {
        public void Update()
        {
            foreach (Drone drone in Drone.Drones)
            {
                if (drone.active)
                {
                    List<Tile> possibleMoves = drone.GetPosibleMoves();
                    if (possibleMoves.Count > 0)
                    {
                        Game.instance.currentState = new MoveDrone(drone, possibleMoves);
                        return;
                    }
                }
            }
            Game.instance.currentState = new ChooseDreadnought();
        }
    }
}
