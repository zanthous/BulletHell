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
        [Range(-1.1f, 1.1f)]
        public float xSpawn;
        [Range(-1.1f, 1.1f)]
        public float ySpawn;
        [Tooltip("How long to wait before spawning the enemy after this one")]
        public float secondsDelayToNext;
    }

    public LevelPart[] enemySpawns;
}
