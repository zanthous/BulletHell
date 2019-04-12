using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    public Sprite sprite;
    //TODO store attack/moving animations in here somehow

    public TextAsset[] jsonAttackPatterns;
    public MovementPattern[] movementPatterns;
}