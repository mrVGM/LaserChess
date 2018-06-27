using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct GameSettings
{
    public static int level = 1;
    public static void Reset()
    {
        level = 1;
    }
    public static int[,] level1Configuration = 
    {
        { 0,0,0,0,6,0,0,0 },
        { 0,5,0,5,0,5,0,0 },
        { 4,4,0,4,4,0,4,4 },
        { 0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0 },
        { 1,1,1,1,1,1,1,1 },
        { 2,2,3,2,2,3,2,2 }
    };

    public static int[,] level2Configuration =
    {
        { 6,0,0,0,0,0,0,6 },
        { 0,5,0,5,0,5,0,0 },
        { 4,4,0,4,4,0,4,4 },
        { 0,0,4,0,0,4,0,0 },
        { 0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0 },
        { 1,1,1,1,1,1,1,1 },
        { 2,2,3,2,2,3,2,2 }
    };

    public static int[,] level3Configuration =
    {
        { 6,0,0,0,6,0,0,6 },
        { 5,5,5,0,0,5,5,5 },
        { 4,4,0,4,4,0,4,4 },
        { 0,4,4,0,0,4,4,0 },
        { 0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0 },
        { 1,1,1,1,1,1,1,1 },
        { 2,2,3,2,2,3,2,2 }
    };

    public static Game.Winner winner;
}
