using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace States.AI
{
    class MoveDrone : State
    {
        Drone drone;
        List<Tile> moves;

        public MoveDrone(Drone drone, List<Tile> moves)
        {
            this.drone = drone;
            this.moves = moves;
        }

        public void Update()
        {
            Game.instance.currentState = new MoveDroneAnimation(drone, new Vector2Int(moves[0].x, moves[0].y));
        }
    }
}
