using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.Human
{
    class ChooseTarget : State
    {
        public HumanPiece attacking;
        public List<AIPiece> targets;

        public ChooseTarget(HumanPiece attacking, List<AIPiece> targets)
        {
            this.attacking = attacking;
            this.targets = targets;

            foreach (AIPiece piece in targets)
                piece.Select();

            Game.instance.InfoPanel.SetActive(true);
            Game.instance.InfoPanel.GetComponent<InfoPanel>().InfoText.text = "Choose a Target to Attack";
        }

        public void Update()
        {
            AIPiece p = Game.instance.SelectedPiece() as AIPiece;
            if (p == null || !p.isSelected)
                return;

            foreach (Piece piece in targets)
                piece.Unselect();

            targets.Clear();
            targets.Add(p);

            Game.instance.InfoPanel.SetActive(false);
            Game.instance.currentState = new AttackAnimation(attacking, targets);
        }
    }
}
