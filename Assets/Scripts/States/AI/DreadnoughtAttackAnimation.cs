using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.AI
{
    class DreadnoughtAttackAnimation : State
    {
        Dreadnought dreadnought;
        List<HumanPiece> targets;

        public DreadnoughtAttackAnimation(Dreadnought dreadnought, List<HumanPiece> targets)
        {
            this.dreadnought = dreadnought;
            this.targets = targets;

            List<Piece> tmp = new List<Piece>();
            foreach (HumanPiece humanPiece in targets)
            {
                tmp.Add(humanPiece);
            }
            LazerAnimation.lazerAnimation.Animate(dreadnought, tmp);
        }

        public void Update()
        {
            if (LazerAnimation.lazerAnimation.IsAnimating())
                return;

            Game.instance.currentState = new TakeDamage(dreadnought.damage, targets, TakeDamage.NextAction.ChooseDreadnought);
        }
    }
}
