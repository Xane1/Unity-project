using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovementScript : MonoBehaviour
{
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.MovePosition(transform.position + Vector3.right * speed);

            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.MovePosition(transform.position + Vector3.left * speed);
            
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
