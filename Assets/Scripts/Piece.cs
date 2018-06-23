using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class Piece
{
    public int x;
    public int y;

    public int hitPoints;
    public int damage;

    public bool isSelected;

    public MonoBehaviour monoBehaviour;

    public Piece(MonoBehaviour mb)
    {
        monoBehaviour = mb;
        isSelected = false;
    }

    public virtual void Select()
    {
        if (!isSelected)
            monoBehaviour.transform.Translate(new Vector3(0.0f, 0.2f, 0.0f));
        isSelected = true;
    }
    public virtual void Unselect()
    {
        if (isSelected)
            monoBehaviour.transform.Translate(new Vector3(0.0f, -0.2f, 0.0f));
        isSelected = false;
    }
}