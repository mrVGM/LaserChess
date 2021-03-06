﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    private Text infoText;
    public Text InfoText
    {
        get
        {
            if (infoText == null)
            {
                infoText = transform.Find("Info").GetComponent<Text>();
            }
            return infoText;
        }
    }
    Button finishYourTurn;

    void OnFinishTurn()
    {
        foreach (HumanPiece humanPiece in HumanPiece.HumanPieces)
        {
            humanPiece.Unselect();
        }
        Game.instance.UnselectAllTiles();

        foreach (Drone piece in Drone.Drones)
        {
            piece.Unselect();
        }

        foreach (Dreadnought piece in Dreadnought.Dreadnoughts)
        {
            piece.Unselect();
        }

        foreach (CommandUnit piece in CommandUnit.CommandUnits)
        {
            piece.Unselect();
        }
        Game.instance.currentState = new States.AI.BeginTurn();
    }

    // Use this for initialization
    void Start() {
        finishYourTurn = transform.Find("FinishTurn").GetComponent<Button>();
        finishYourTurn.onClick.AddListener(OnFinishTurn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
