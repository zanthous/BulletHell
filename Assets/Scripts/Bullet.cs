﻿using UnityEngine;
using System.Collections;

public class Bullet
{
    //angle
    private float direction;
    private float speed;
    //0 or more actions
    Queue actions;
    //TODO make gameobject
    string bulletObject;
    public Bullet(string bulletObject, float direction, float speed, Queue actions)
    {
        this.bulletObject = bulletObject;
        this.direction = direction;
        this.speed = speed;
        this.actions = actions;
    }
}
