using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game instance;

    public Tile[,] board;
    public Piece[,] pieces;

	// Use this for initialization
	void Start () {
        board = new Tile[8, 8];
        pieces = new Piece[8, 8];
        instance = this;
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

    // Update is called once per frame
    void Update () {
        Tile selected = SelectedTile();
        Piece piece = SelectedPiece();
        if (piece != null)
        {
            piece.Select();
        }
        if (selected != null)
        {
            selected.Select();
        }
    }
}
