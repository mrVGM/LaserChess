using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.AI
{
    class DroneAttackAnimation : State
    {
        Drone drone;
        HumanPiece target;
        public DroneAttackAnimation(Drone drone, HumanPiece target)
        {
            this.drone = drone;
            this.target = target;
            List<Piece> tmp = new List<Piece>();
            tmp.Add(target);
            LazerAnimation.lazerAnimation.Animate(drone, tmp);
        }
        public void Update()
        {
            if (LazerAnimation.lazerAnimation.IsAnimating())
                return;

            List<HumanPiece> tmp = new List<HumanPiece>();
            tmp.Add(target);
            Game.instance.currentState = new TakeDamage(drone.damage, tmp, TakeDamage.NextAction.ChooseDrone);
        }
    }
}
