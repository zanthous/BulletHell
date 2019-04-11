using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Test bullet script
//Placeholder until bulletML system is complete
public class Fireball : PooledMonobehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5.0f || transform.position.y > 5.0f ||
            transform.position.x < -6.2222f || transform.position.x > 6.2222f)
        {
            //Setting active to false readds it to the pool
            gameObject.SetActive(false);
        }

        transform.position += Vector3.down * Time.deltaTime * 3.0f;
    }
}
