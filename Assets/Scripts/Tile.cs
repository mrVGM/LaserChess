using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public int x;
    public int y;

	// Use this for initialization
	void Start () {
        x = System.Convert.ToInt32(transform.position.x + 3.5);
        y = System.Convert.ToInt32(transform.position.z + 3.5);
        Game.instance.board[x,y] = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
