using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;


public static class Game 
{
    public static float Left    = -6.22222f;
    public static float Right   = 6.22222f;
    public static float Top     = 5.0f;
    public static float Bottom  = -5.0f;

    public static float Width   = 12.44444f;
    public static float Height  = 10.0f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ToWorld(Vector2 point)
    {
        //0 to 1,0 to 1 range
        //return new Vector2((point.x * Width) - Right, (point.y * Height) - Top);
        //-1 to 1,-1 to 1 range
        return new Vector2((point.x * Right), (point.y * Top));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ToGame(Vector2 point)
    {
        //return new Vector2((point.x + Right)/Width, (point.y + Top) / Height);
        return new Vector2((point.x / Right), (point.y / Top));
    }
}
