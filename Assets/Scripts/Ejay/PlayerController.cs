using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0;
    public Rigidbody2D rb;
    public Weapon weapon;
    private float horizontal;

    private Vector2 moveDirection;
    private Vector2 mousePosition;
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y);

        Vector2 aimdirection = mousePosition - rb.position;
        float aimAngle = MathF.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}