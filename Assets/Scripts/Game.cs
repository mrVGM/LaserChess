using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public States.State currentState;

    public static Game instance;

    public Tile[,] board;
    public Piece[,] pieces;

    public GameObject InfoPanel;

    // Use this for initialization
    void Start() {

        HumanPiece.HumanPieces.Clear();
        Drone.Drones.Clear();
        Dreadnought.Dreadnoughts.Clear();
        CommandUnit.CommandUnits.Clear();

        board = new Tile[8, 8];
        pieces = new Piece[8, 8];
        instance = this;

        GameObject[] prefabs =
        {
            null,
            Resources.Load<GameObject>("Prefabs/HumanPieces/Grunt"),
            Resources.Load<GameObject>("Prefabs/HumanPieces/Jumpship"),
            Resources.Load<GameObject>("Prefabs/HumanPieces/Tank"),
            Resources.Load<GameObject>("Prefabs/AIPieces/Drone"),
            Resources.Load<GameObject>("Prefabs/AIPieces/Dreadnought"),
            Resources.Load<GameObject>("Prefabs/AIPieces/CommandUnit"),
        };

        int[,] configuration = null;

        switch (GameSettings.level)
        {
            case 1:
                configuration = GameSettings.level1Configuration;
                break;
            case 2:
                configuration = GameSettings.level2Configuration;
                break;
            case 3:
                configuration = GameSettings.level3Configuration;
                break;
        }

        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                GameObject current = prefabs[configuration[7 - i, j]];
                if (current == null)
                    continue;
                GameObject tmp = Instantiate(current) as GameObject;
                tmp.transform.Translate(new Vector3(j - 3.5f, 0.0f, i - 3.5f));
            }
        }

        InfoPanel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;

        currentState = new States.Human.BeginTurn();
    }

    public Tile SelectedTile()
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

    public Piece SelectedPiece()
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
    
    public void UnselectAllTiles()
    {
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                board[i, j].UnSelect();
            }
        }
    }
    
    public void SetAIPiecesActive()
    {
        foreach (Drone drone in Drone.Drones)
            drone.active = true;
        foreach (Dreadnought dreadnought in Dreadnought.Dreadnoughts)
            dreadnought.active = true;
        foreach (CommandUnit commandUnit in CommandUnit.CommandUnits)
            commandUnit.active = true;
    }

    public void SetHumanPiecesActive()
    {
        foreach (HumanPiece humanPiece in HumanPiece.HumanPieces)
            humanPiece.active = true;
    }

    // Update is called once per frame
    void Update () {
        currentState.Update();
    }

    public enum Winner
    {
        Human,
        AI
    };

    public void EndGame(Winner winner)
    {
        //throw new NotImplementedException();
        SceneManager.LoadScene("EndScreen");
        GameSettings.winner = winner;
    }
}
