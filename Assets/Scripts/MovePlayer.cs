using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float width;
    private float height;
    private void Awake()
    {
        width = GetComponent<SpriteRenderer>().bounds.max.x - GetComponent<SpriteRenderer>().bounds.min.x;
        height = GetComponent<SpriteRenderer>().bounds.max.y - GetComponent<SpriteRenderer>().bounds.min.y;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveOffset = 
            new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 
            Input.GetAxisRaw("Vertical") * Time.deltaTime * speed);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveOffset /= 2.0f;
        }

        transform.position += (Vector3)moveOffset;

        HandleBounds();

    }

    private void HandleBounds()
    {
        if(transform.position.x - width/2.0f < Game.Left)
        {
            transform.position = new Vector3(Game.Left + width / 2.0f, transform.position.y, transform.position.z);
        }
        if(transform.position.x + width / 2.0f > Game.Right)
        {
            transform.position = new Vector3(Game.Right - width / 2.0f, transform.position.y, transform.position.z);
        }

        if(transform.position.y - height / 2.0f < Game.Bottom)
        {
            transform.position = new Vector3(transform.position.x, Game.Bottom + height / 2.0f, transform.position.z);
        }
        if(transform.position.y + height / 2.0f > Game.Top)
        {
            transform.position = new Vector3(transform.position.x, Game.Top - height / 2.0f, transform.position.z);
        }
    }
}
