using UnityEngine;
using System.Collections;

public class Fire
{
    Bullet bullet;
    //Angle in degrees
    float direction;
    float speed;

    public Fire( Bullet bullet, float direction, float speed)
    {
        this.bullet = bullet;
        this.direction = direction;
        this.speed = speed;
    }
}
