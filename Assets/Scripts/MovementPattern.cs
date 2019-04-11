using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO look into conditional variables
[System.Serializable]
public class MovementCommand
{
    public MovementType moveType;
    [Tooltip("End when a point is reached, or after a certain amount of time")]
    public MovementEnd movementEnd;
    [Header("MovementType: Sine")]
    public float sinFrequency;
    public float sinMagnitude;
    [Header("MovementEnd: Duration")]
    public float duration;
    [Tooltip("1 movespeed moves 1 half of the screen per second")]
    public float durationMoveSpeed;
    public Vector2 durationDirection;
    [Header("MovementEnd: Point")]
    [Tooltip("Grid goes from -1x left to +1x right, and -1y bottom to +1y top")]
    public Vector2 point;

    public bool vanishAfterComplete;
    public float delay;
}

[CreateAssetMenu]
public class MovementPattern : ScriptableObject
{
    public MovementCommand[] movementCommands;
}

//TODO add more
public enum MovementType
{
    SmoothArrive,
    Normal,
    Sine
}

public enum MovementEnd
{
    Point,
    Duration
}