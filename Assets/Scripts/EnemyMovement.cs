using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Each pattern is made of pattern commands
    //Enemies can have as many patterns as we want to add to them
    //For now patterns will default to looping, unless a command has vanishAfterComplete set 
    private MovementPattern[] patterns;
    private MovementCommand currentCommand;
    
    private int patternIndex = 0;
    private int patternCommandIndex = 0;

    //How close an enemy needs to be to a point before moving to the next command
    private float closeEnough = 0.5f;
    //Duration command timer
    private float durationTimer;
    private float duration;
    //Timer between moves
    private float delayTimer;
    private float delay;

    private bool isDuration;

    private Vector2 point;
    private Vector2 direction;
    private Vector2 worldPoint;
    private Vector2 durationResultantPoint;
    private Vector2 currentVelocity;
    private Vector2 startPos;
    
    // Update is called once per frame
    void Update()
    {
        //Setup hasn't been called yet
        if (currentCommand == null)
            return;
        //Wait if there is a delay until the next command
        if(delay > 0 && delayTimer < delay)
        {
            delayTimer += Time.deltaTime;
            return;
        }

        durationTimer += Time.deltaTime;
        
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
                        transform.position = Vector2.SmoothDamp(transform.position, durationResultantPoint, ref currentVelocity, duration);
                        break;
                    case MovementType.Normal:
                        transform.position = Vector2.Lerp(startPos, durationResultantPoint, (durationTimer / duration));
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
            if(Vector2.Distance(Game.ToWorld(point), transform.position) < closeEnough)
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
                        transform.position = Vector2.SmoothDamp(transform.position,worldPoint, ref currentVelocity, duration);
                        break;
                    case MovementType.Normal:
                        transform.position = Vector2.Lerp(startPos, worldPoint, (durationTimer / duration));
                        break;
                    case MovementType.Sine:
                        //TODO
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
        //Delay after command has completed
        delay = currentCommand.delay;

        //Disable
        if (patterns[patternIndex].movementCommands[patternCommandIndex].vanishAfterComplete)
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
        //Read command and update class variables
        UpdateCommandVariables();
    }

    //Called after Levelmanager parses enemy scriptableobjects
    public void Setup(MovementPattern[] patterns_in)
    {
        patternIndex = 0;
        patternCommandIndex = 0;
        durationTimer = 0.0f;
        patterns = patterns_in;

        //Level -> Enemies -> Movement patterns -> Movement Commands
        UpdateCommandVariables();
    }

    private void UpdateCommandVariables()
    {
        currentCommand = patterns[patternIndex].movementCommands[patternCommandIndex];
        startPos = transform.position;
        point = currentCommand.point;
        isDuration = (currentCommand.movementEnd == MovementEnd.Duration);
        duration = currentCommand.duration;
        direction = currentCommand.durationDirection;
        worldPoint = Game.ToWorld(point);
        durationResultantPoint = Game.ToWorld(direction.normalized * currentCommand.durationMoveSpeed * duration);
    }
}