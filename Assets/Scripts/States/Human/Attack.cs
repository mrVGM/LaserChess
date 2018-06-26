using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.Human
{
    class Attack : State
    {
        HumanPiece piece;

        public Attack(HumanPiece piece)
        {
            this.piece = piece;
        }

        public void Update()
        {
            bool requireChoice;
            List<Piece> attackPosibilities = piece.GetAttackPossibilities(out requireChoice);

            if (attackPosibilities.Count == 0)
            {
                Game.instance.currentState = new ActivePieces();
                return;
            }

            List<AIPiece> AITargets = new List<AIPiece>();
            foreach (Piece p in attackPosibilities)
            {
                if (p as AIPiece != null)
                    AITargets.Add(p as AIPiece);
            }

            if (requireChoice)
            {
                Game.instance.currentState = new ChooseTarget(piece, AITargets);
                return;
            }

            Game.instance.currentState = new AttackAnimation(piece, AITargets);
        }
    }
}
