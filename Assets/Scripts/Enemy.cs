using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    //initial sprite
    public Sprite sprite;

    //List<AttackPatterns>
    //List<MovementPatterns> - contain animations in here?
}

public enum EnemyType
{
    test
}