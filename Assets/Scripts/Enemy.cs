using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    //initial sprite
    public Sprite sprite;
    [Range(-1.1f,1.1f)]
    public float xSpawn;
    [Range(-1.1f, 1.1f)]
    public float ySpawn;
    //store attack/moving animations in here somehow

    public TextAsset[] jsonAttackPatterns;
    public MovementPattern[] movementPatterns;
}