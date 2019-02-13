using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Reads level scriptable object and sets up everything in the scene 
//Currently setup to not persist between scenes but can be changed
public class LevelManager : MonoBehaviour
{
    public GameObject enemyBasePrefab;
    public Level level;
    private float timer;
    private float currentDelay;
    private string levelName;
    private int enemyIndex = 0;
    //Level will be determined to be over if the enemyIndex == length of enemyspawns 
    //and all enemies in the pool are disabled
    
    // Start is called before the first frame update
    void Start()
    {
        levelName = level.levelName;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyIndex < level.enemySpawns.Length)
        {
            if(currentDelay <= 0.0f)
            {
                currentDelay = level.enemySpawns[enemyIndex].delayToNext;
                //TODO get from pool instead
                GameObject temp = Instantiate(enemyBasePrefab);
                temp.GetComponent<SpriteRenderer>().sprite = level.enemySpawns[enemyIndex].enemy.sprite;
                temp.transform.position = new Vector3(
                    Game.Right * level.enemySpawns[enemyIndex].spawnPosition.x,
                    Game.Top * level.enemySpawns[enemyIndex].spawnPosition.y,
                    temp.transform.position.z);
                //Add components for enemy movement and enemy attack and call their startup 
                //with the information they need, so probably pass in level.enemySpawns[enemyIndex].enemy.jsonAttackPatterns
                //and level.enemySpawns[enemyIndex].enemy.movementPatterns

            }
            else
            {
                currentDelay -= Time.deltaTime;
            }
        }
    }

    public static void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public static void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
