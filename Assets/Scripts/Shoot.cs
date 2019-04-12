using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    private Pool pool;
    private float timer = 0.0f;
    private float shootInterval = .2f;

    // Start is called before the first frame update
    void Start() 
    {
        pool = Pool.GetPool(bullet.GetComponent<Fireball>());   
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shootInterval)
        {
            var test = pool.Get<Fireball>();
            test.gameObject.SetActive(true);
            test.gameObject.transform.position = transform.position;
            timer = 0.0f;
        }
    }
}
