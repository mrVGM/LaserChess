using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    enum Turn
    {
        HumanTurn,
        AITurn
    }

    enum State
    {
        SelectPiece,
        Move,
        Attack
    }

    public static Game instance;

    public Tile[,] board;
    public Piece[,] pieces;

    Turn turn;
    State state;

    HumanPiece currentlySelected;

	// Use this for initialization
	void Start () {
        board = new Tile[8, 8];
        pieces = new Piece[8, 8];
        instance = this;

        turn = Turn.HumanTurn;
        state = State.SelectPiece;

        currentlySelected = null;
	}

    Tile SelectedTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Tile")
            {
                int x = System.Convert.ToInt32(hit.transform.position.x + 3.5);
                int y = System.Convert.ToInt32(hit.transform.position.z + 3.5);
                return board[x, y];
            }
        }
        return null;
    }

    Piece SelectedPiece()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Piece")
            {
                int x = System.Convert.ToInt32(hit.transform.position.x + 3.5);
                int y = System.Convert.ToInt32(hit.transform.position.z + 3.5);
                return pieces[x, y];
            }
        }
        return null;
    }

    void SelectPieceStage()
    {
        currentlySelected = SelectedPiece() as HumanPiece;
        if (currentlySelected == null)
            return;

        currentlySelected.Select();
        currentlySelected.markPosibleMoves();
    }

    // Update is called once per frame
    void Update () {
        if (turn == Turn.AITurn)
            return;

        if (state == State.SelectPiece)
        {
            SelectPieceStage();
        }
    }
}
