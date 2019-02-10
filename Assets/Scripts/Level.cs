using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    public string levelName;

    [System.Serializable]
    public class LevelPart
    {
        public Enemy enemy;
        public Vector2 spawnPosition;
        public float delayToNext;
    }

    public LevelPart[] enemySpawns;
}
