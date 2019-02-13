using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFireball : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject rocket;

    public float speed = 10f;

    void FireRocket()
    {
        var rocketClone = Instantiate(rocket, transform.position, transform.rotation);
        rocketClone.GetComponent<Rigidbody2D>().velocity = transform.forward * speed;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireRocket();
        }
    }
}