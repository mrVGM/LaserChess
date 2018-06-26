using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.AI
{
    class DroneAttack : State
    {
        Drone drone;
        public DroneAttack(Drone drone)
        {
            this.drone = drone;
        }

        public void Update()
        {
            bool requireChoice;
            List<Piece> attackPosibilities = drone.GetAttackPossibilities(out requireChoice);

            if (requireChoice)
            {
                Piece bestChoice = attackPosibilities[0];
                if (attackPosibilities[1].y > attackPosibilities[0].y)
                    bestChoice = attackPosibilities[1];

                attackPosibilities.Clear();
                attackPosibilities.Add(bestChoice);
            }

            if (attackPosibilities.Count == 1)
            {
                Game.instance.currentState = new DroneAttackAnimation(drone, attackPosibilities[0] as HumanPiece);
            }
            else
            {
                Game.instance.currentState = new ChooseDrone();
            }
        }
    }
}
