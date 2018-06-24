using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Drone : AIPiece
{
    public Drone(MonoBehaviour mb) : base(mb)
    {
        damage = 1;
        hitPoints = 2;
    }
}

