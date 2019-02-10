using UnityEngine;
using System.Collections;

public class Bullet
{
    //angle
    private float direction;
    private float speed;
    private bool reflectable;
    //0 or more actions
    Queue actions;
    //TODO make gameobject
    string bulletObject;
    public Bullet(string bulletObject, float direction, float speed, Queue actions, bool reflectable)
    {
        this.bulletObject = bulletObject;
        this.direction = direction;
        this.speed = speed;
        this.actions = actions;
        this.reflectable = reflectable;
    }
}
