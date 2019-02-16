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
    private MovementCommand currentCommand;

    private int patternIndex = 0;
    private int patternCommandIndex = 0;

    private float closeEnough = 0.05f;
    private float durationTimer;
    private float duration;
    private float delayTimer;
    private float delay;
    private float scaledX;
    private float scaledY;
    private float currentVelocity;

    private bool isDuration;
    private Vector2 point;

    // Start is called before the first frame update
    void Start()
    {
        currentCommand = patterns[patternIndex].movementCommands[patternCommandIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if(delay > 0 && delayTimer < delay)
        {
            delayTimer += Time.deltaTime;
            return;
        }

        //TODO handle delays
        scaledX = transform.position.x / Game.Right;
        scaledY = transform.position.y / Game.Top;
        //Resolve current pattern + command
        if(isDuration)
        {
            if(durationTimer >= duration)
            {
                GetNextCommand();
            }
            else
            {
                //switch on algorithm and move in direction of point scaled down to magnitude of 1 * speed
                switch(currentCommand.moveType)
                {
                    case MovementType.SmoothArrive:
                        //Vector2.SmoothDamp
                        break;
                    case MovementType.Normal:
                        //Vector2.Lerp
                        break;
                    case MovementType.Sine:
                        break;
                    default:
                        break;
                }
            }
        }
        else//point
        {
            if(Vector2.Distance(transform.position, new Vector2(scaledX, scaledY)) < closeEnough)
            {
                GetNextCommand();
            }
            else
            {
                //switch on algorithm (smoothed, non smoothed, sine etc) and lerp to point
                switch(currentCommand.moveType)
                {
                    case MovementType.SmoothArrive:
                        //smoothdamp
                        break;
                    case MovementType.Normal:
                        break;
                    case MovementType.Sine:
                        break;
                    default:
                        break;
                }
            }
        }
        }

        //Increment patternIndex and pattern(if necessary)
        private void GetNextCommand()
    {
        //Disable
        if(patterns[patternIndex].movementCommands[patternCommandIndex].vanishAfterComplete)
        {
            gameObject.SetActive(false);
            return;
        }

        patternCommandIndex++;
        durationTimer = 0;
        delayTimer = 0;

        if(patternCommandIndex >= patterns[patternIndex].movementCommands.Length) 
        {
            patternCommandIndex = 0;
            //Wrap patterns
            patternIndex = (patternIndex + 1) % patterns.Length;
        }

        currentCommand = patterns[patternIndex].movementCommands[patternCommandIndex];

        delay         = currentCommand.delay;
        isDuration    = currentCommand.movementEnd == MovementEnd.Duration;
        point         = currentCommand.pointOrDirection;
        durationTimer = currentCommand.duration;
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