using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public abstract class Piece
{
    public int x;
    public int y;

    public int hitPoints;
    public int damage;

    public bool isSelected;
    public enum Type
    {
        AI,
        Human
    }
    public Type type;

    public MonoBehaviour monoBehaviour;

    public Piece(MonoBehaviour mb)
    {
        monoBehaviour = mb;
        isSelected = false;
    }

    public void Select()
    {
        if (!isSelected)
            monoBehaviour.transform.Translate(new Vector3(0.0f, 0.2f, 0.0f));
        isSelected = true;
    }
    public void Unselect()
    {
        if (isSelected)
            monoBehaviour.transform.Translate(new Vector3(0.0f, -0.2f, 0.0f));
        isSelected = false;
    }
}

public abstract class AIPiece : Piece
{
    public AIPiece(MonoBehaviour mb) : base(mb)
    {
        type = Type.AI;
    }
}

public abstract class HumanPiece : Piece
{
    public HumanPiece(MonoBehaviour mb) : base(mb)
    {
        type = Type.Human;
    }

    public abstract void markPosibleMoves();
}