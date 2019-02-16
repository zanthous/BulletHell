using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Set either direction or point
[System.Serializable]
public class MovementCommand
{
    public MovementType moveType;
    //public float moveSpeed;
    public float sinFrequency;
    public float sinMagnitude;
    public MovementEnd movementEnd;
    public float duration;
    public Vector2 pointOrDirection;
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