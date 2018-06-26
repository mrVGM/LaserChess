using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace States.Human
{
    class SelectPiece : State
    {
        Dictionary<HumanPiece, List<Tile>> active;
        HumanPiece current;
        public SelectPiece(Dictionary<HumanPiece, List<Tile>> active, HumanPiece current = null)
        {
            this.active = active;
            this.current = current;
        }

        public void Update()
        {
            if (current == null)
                current = Game.instance.SelectedPiece() as HumanPiece;

            if (current == null)
                return;

            List<Tile> possibleMoves = null;
            try
            {
                possibleMoves = active[current];
            }
            catch (Exception)
            { }

            if (possibleMoves == null)
            {
                current = null;
                return;
            }

            foreach (KeyValuePair<HumanPiece, List<Tile>> entry in active)
            {
                entry.Key.Unselect();
            }

            current.Select();
            Game.instance.currentState = new Move(current, active);
        }
    }
}
