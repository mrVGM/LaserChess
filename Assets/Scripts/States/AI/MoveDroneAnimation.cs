using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace States.AI
{
    class MoveDroneAnimation : State
    {
        Drone drone;
        Vector2Int position;
        public MoveDroneAnimation(Drone drone, Vector2Int position)
        {
            this.drone = drone;
            this.position = position;
            drone.Move(position.x, position.y);
        }

        public void Update()
        {
            if (MovementAnimation.movementAnimation.IsAnimating())
                return;

            drone.active = false;
            Game.instance.currentState = new DroneAttack(drone);
        }
    }
}
