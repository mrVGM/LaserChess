using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.Human
{
    class TakeDamage : State
    {
        int hitPoints;
        List<AIPiece> pieces;
        bool done;

        public TakeDamage(int hitPoints, List<AIPiece> pieces)
        {
            this.hitPoints = hitPoints;
            this.pieces = pieces;
            done = false;
        }

        public void Update()
        {
            if (!done)
            {
                foreach (AIPiece p in pieces)
                {
                    p.TakeDamage(hitPoints);
                }
                done = true;
                return;
            }
            Game.instance.currentState = new ActivePieces();
        }
    }
}
