using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.AI
{
    class DreadnoughtAttack : State
    {
        public Dreadnought dreadnought;

        public DreadnoughtAttack(Dreadnought dreadnought)
        {
            this.dreadnought = dreadnought;
        }

        public void Update()
        {
            bool requireChoice;
            List<Piece> attackPosibilities = dreadnought.GetAttackPossibilities(out requireChoice);

            if (attackPosibilities.Count > 0)
            {
                List<HumanPiece> tmp = new List<HumanPiece>();
                foreach (Piece p in attackPosibilities)
                {
                    tmp.Add(p as HumanPiece);
                }
                Game.instance.currentState = new DreadnoughtAttackAnimation(dreadnought, tmp);
            }
            else
            {
                Game.instance.currentState = new ChooseDreadnought();
            }
        }
    }
}
