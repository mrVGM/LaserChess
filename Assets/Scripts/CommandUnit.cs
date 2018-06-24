using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CommandUnit : AIPiece
{
    public CommandUnit(MonoBehaviour mb) : base(mb)
    {
        damage = 0;
        hitPoints = 5;
    }
}
