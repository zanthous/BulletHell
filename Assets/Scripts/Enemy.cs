using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    //initial sprite
    public Sprite sprite;
    //store attack/moving animations in here somehow

    public TextAsset[] jsonAttackPatterns;
    public MovementPattern[] movementPatterns;
}