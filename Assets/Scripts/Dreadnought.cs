using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dreadnought : AIPiece
{
    public Dreadnought(MonoBehaviour mb) : base(mb)
    {
        damage = 2;
        hitPoints = 5;
    }
}
