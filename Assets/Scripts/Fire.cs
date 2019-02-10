using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
