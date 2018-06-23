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

    public MonoBehaviour monoBehaviour;

    public Piece(MonoBehaviour mb)
    {
        monoBehaviour = mb;
    }
}