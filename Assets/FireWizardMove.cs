using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWizardMove : MonoBehaviour
{

    private float timer = 0.0f;
    private float changeDirInterval = 1.0f;
    private bool direction = false;
    private Rigidbody2D rb;
    private float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position += direction ? Vector3.left * Time.deltaTime * speed: Vector3.right * Time.deltaTime * speed;
        if(timer > changeDirInterval)
        {
            direction = !direction;
            timer = 0.0f;
        }

    }
}
