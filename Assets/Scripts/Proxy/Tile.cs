using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public int x;
    public int y;

    public Texture tex;

    public bool isSelected;

    public void Select()
    {
        if (!isSelected)
            GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
        isSelected = true;
    }

    public void UnSelect()
    {
        if (isSelected)
            GetComponent<Renderer>().material.SetTexture("_MainTex", null);
        isSelected = false;
    }

    public Tile()
    {
        isSelected = false;
    }

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
