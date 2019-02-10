using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MovementPattern : ScriptableObject
{
    //Set either duration or point
    [System.Serializable]
    public class MovementCommand
    {
        public MovementType moveType;
        public float moveSpeed;
        public MovementEnd movementEnd;
        public float duration;
        public Vector2 point;
        public float delay;
    }

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