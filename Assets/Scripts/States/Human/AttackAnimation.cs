using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.Human
{
    class AttackAnimation : State
    {
        HumanPiece attacking;
        List<AIPiece> targets;

        public AttackAnimation(HumanPiece attacking, List<AIPiece> targets)
        {
            this.attacking = attacking;
            this.targets = targets;

            List<Piece> AITargets = new List<Piece>();
            foreach (AIPiece p in targets)
            {
                AITargets.Add(p);
            }

            LazerAnimation.lazerAnimation.Animate(attacking, AITargets);
        }

        public void Update()
        {
            if (LazerAnimation.lazerAnimation.IsAnimating())
                return;

            Game.instance.currentState = new TakeDamage(attacking.damage, targets);
        }
    }
}
