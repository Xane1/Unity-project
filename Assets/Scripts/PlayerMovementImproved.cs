using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovementImproved : MonoBehaviour
{
    public float speed = 8f;
    private bool isFacingRight = true;
    public Rigidbody2D rb;
    public Weapon weapon;
    private float horizontal;

    private Vector2 mousePosition;
    private Vector2 moveDirection;
    

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        
        Flip();
        
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
        Vector2 aimdirection = mousePosition - rb.position;
        float aimAngle = MathF.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }
}