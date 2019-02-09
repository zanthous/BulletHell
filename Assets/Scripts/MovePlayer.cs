using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Awake()
    {
        
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

        //SpriteRenderer a = gameObject.GetComponent<SpriteRenderer>();

        transform.position += (Vector3)moveOffset;
    }
    
}
