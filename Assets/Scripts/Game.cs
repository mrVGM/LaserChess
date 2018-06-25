using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        Attack,
        EndTurn
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

        GameObject[] prefabs =
        {
            null,
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/HumanPieces/Grunt.prefab"),
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/HumanPieces/Jumpship.prefab"),
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/HumanPieces/Tank.prefab"),
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/AIPieces/Drone.prefab"),
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/AIPieces/Dreadnought.prefab"),
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/AIPieces/CommandUnit.prefab"),
        };

        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                GameObject current = prefabs[GameSettings.level1Configuration[7 - i, j]];
                if (current == null)
                    continue;
                GameObject tmp = Instantiate(current) as GameObject;
                tmp.transform.Translate(new Vector3(j - 3.5f, 0.0f, i - 3.5f));
            }
        }
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

    void SelectPieceStage(HumanPiece piece = null)
    {
        Dictionary<HumanPiece, List<Tile>> active = HumanPiece.ActivePieces();
        if (active.Count == 0)
        {
            SetAIPiecesActive();
            turn = Turn.AITurn;
            return;
        }

        currentlySelected = piece;
        if (currentlySelected == null)
            currentlySelected = SelectedPiece() as HumanPiece;

        if (currentlySelected == null)
            return;

        List<Tile> possibleMoves = null;
        try
        {
            possibleMoves = active[currentlySelected];
        }
        catch(Exception)
        { }

        if (possibleMoves == null)
        {
            currentlySelected = null;
            return;
        }

        currentlySelected.Select();

        foreach (Tile t in possibleMoves)
            t.Select();

        state = State.Move;
    }

    void UnselectAllTiles()
    {
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                board[i, j].UnSelect();
            }
        }
    }

    void MoveStage()
    {
        if (currentlySelected == null)
        {
            state = State.SelectPiece;
            return;
        }
        HumanPiece tmp = SelectedPiece() as HumanPiece;
        if (tmp != null)
        {
            if (tmp == currentlySelected)
            {
                currentlySelected.Unselect();
                currentlySelected = null;
                UnselectAllTiles();
                state = State.SelectPiece;
            }
            else
            {
                currentlySelected.Unselect();
                currentlySelected = null;
                UnselectAllTiles();
                SelectPieceStage(tmp);
            }
            return;
        }
        Tile tile = SelectedTile();
        if (tile == null || !tile.isSelected)
            return;

        UnselectAllTiles();
        currentlySelected.Unselect();

        currentlySelected.Move(tile.x, tile.y);
        currentlySelected.active = false;

        state = State.Attack;
    }

    void AttackStage()
    {
        bool requireChoice;
        List<Piece> attackPosibilities = currentlySelected.GetAttackPossibilities(out requireChoice);

        if (requireChoice)
        {
            foreach (Piece piece in attackPosibilities)
                piece.Select();

            AIPiece p = SelectedPiece() as AIPiece;
            if (p == null || !p.isSelected)
                return;

            foreach (Piece piece in attackPosibilities)
                piece.Unselect();

            attackPosibilities.Clear();
            attackPosibilities.Add(p);
        }

        currentlySelected.Attack(attackPosibilities);
        currentlySelected.Unselect();
        currentlySelected = null;
        state = State.SelectPiece;
    }

    void AITurn()
    {
        bool inProgress = false;
        do
        {
            inProgress = false;
            foreach (Drone drone in Drone.Drones)
            {
                if (drone.MakeMoveAndAttack())
                {
                    inProgress = true;
                    break;
                }
            }
        }
        while (inProgress);
        
        do
        {
            inProgress = false;
            foreach (Dreadnought dreadnought in Dreadnought.Dreadnoughts)
            {
                if (dreadnought.MakeMoveAndAttack())
                {
                    inProgress = true;
                    break;
                }
            }
        }
        while (inProgress);

        do
        {
            inProgress = false;
            foreach (CommandUnit commandUnit in CommandUnit.CommandUnits)
            {
                if (commandUnit.MakeMoveAndAttack())
                {
                    inProgress = true;
                    break;
                }
            }
        }
        while (inProgress);

        SetHumanPiecesActive();
        turn = Turn.HumanTurn;
        state = State.SelectPiece;
    }

    void SetAIPiecesActive()
    {
        foreach (Drone drone in Drone.Drones)
            drone.active = true;
        foreach (Dreadnought dreadnought in Dreadnought.Dreadnoughts)
            dreadnought.active = true;
        foreach (CommandUnit commandUnit in CommandUnit.CommandUnits)
            commandUnit.active = true;
    }

    void SetHumanPiecesActive()
    {
        foreach (HumanPiece humanPiece in HumanPiece.HumanPieces)
            humanPiece.active = true;
    }

    // Update is called once per frame
    void Update () {
        if (turn == Turn.AITurn)
        {
            AITurn();
            return;
        }

        
        switch (state)
        {
            case State.SelectPiece:
                SelectPieceStage();
                break;
            case State.Move:
                MoveStage();
                break;
            case State.Attack:
                AttackStage();
                break;
        }
    }
}
