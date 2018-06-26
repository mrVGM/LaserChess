using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace States.Human
{
    class Move : State
    {
        HumanPiece current;
        Dictionary<HumanPiece, List<Tile>> active;
        List<Tile> moves;
        public Move(HumanPiece current, Dictionary<HumanPiece, List<Tile>> active)
        {
            this.current = current;
            this.active = active;
            moves = active[current];
        }

        public void Update()
        {
            foreach (Tile t in moves)
                t.Select();

            HumanPiece piece = Game.instance.SelectedPiece() as HumanPiece;
            if (piece != null)
            {
                Game.instance.UnselectAllTiles();
                current.Unselect();

                Game.instance.currentState = new ActivePieces();

                return;
            }

            Tile tile = Game.instance.SelectedTile();
            if (tile == null || !tile.isSelected)
                return;

            Game.instance.UnselectAllTiles();
            current.Unselect();

            Game.instance.currentState = new MoveAnimation(current, new Vector2Int(tile.x, tile.y));
        }
    }
}
