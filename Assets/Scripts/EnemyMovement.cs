using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Each pattern is made of pattern commands
    //Enemies can have as many patterns as we want to add to them
    //For now patterns will default to looping, unless a command has vanishAfterComplete set (subject to change)
    private MovementPattern[] patterns;
    private int patternIndex = 0;
    private int patternCommandIndex = 0;
    private float durationTimer;
    private bool isDuration;
    private float scaledX;
    private float scaledY;
    private Vector2 target;

    private float closeEnough = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO handle delays
        scaledX = transform.position.x / Game.Right;
        scaledY = transform.position.y / Game.Top;
        //Resolve current pattern + command
        if(isDuration)
        {

        }
        else//point
        {
            if(Vector2.Distance(transform.position,new Vector2(scaledX,scaledY)) < closeEnough)
            {
                GetNextCommand();
            }
        }
        //Do command + pattern
    }

    private void GetNextCommand()
    {
        patternCommandIndex++;
        if(patternCommandIndex==patterns[patternIndex].movementCommands.Length)
        {
            //Disable
            if(patterns[patternIndex].movementCommands[patternCommandIndex].vanishAfterComplete)
            {
                gameObject.SetActive(false);
            }
            patternCommandIndex = 0;
            //Wrap patterns
            patternIndex = (patternIndex + 1) % patterns.Length;
        }

        isDuration = (patterns[patternIndex].movementCommands[patternCommandIndex].movementEnd == MovementEnd.Duration);
        target = patterns[patternIndex].movementCommands[patternCommandIndex].point;
        durationTimer = patterns[patternIndex].movementCommands[patternCommandIndex].duration;


    }

    public void Setup(MovementPattern[] patterns_in)
    {
        patternIndex = 0;
        patternCommandIndex = 0;
        durationTimer = 0.0f;
        isDuration = false;
        patterns = patterns_in;
    }
}
