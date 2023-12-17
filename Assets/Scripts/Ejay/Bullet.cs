using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 lastVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.transform.tag);
        if (coll.transform.tag == "Deflect")
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

            rb.velocity = direction * MathF.Max(speed, 0f);
        }
        else
        {
            Destroy(gameObject);
            //Enemy, wall hit
        }
    }
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}