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
	
	// Update is called once per frame
	void Update () {
		
	}
}
