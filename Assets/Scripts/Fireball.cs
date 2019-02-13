using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : PooledMonobehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5.0f || transform.position.y > 5.0f ||
            transform.position.x < -6.2222f || transform.position.x > 6.2222f)
        {
            gameObject.SetActive(false);
        }
    }
}
