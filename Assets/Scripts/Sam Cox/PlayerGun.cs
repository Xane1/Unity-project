using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{ 
    private Vector2 _mousePosition;
    private Camera _playerCam;
    private Vector2 _playerPosition;
    [SerializeField] private bool canFlipPlayer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform pivotTransform;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 2f;
    private void Start()
    {
        _playerCam = Camera.main;
    }

    void Update()
    {
        _mousePosition = _playerCam.ScreenToWorldPoint(Input.mousePosition);
        SetGunAim();
        if (Input.GetMouseButtonDown(0)) Fire();
    }
    private void SetGunAim()
    {
        _playerPosition = pivotTransform.position;
        Vector2 aimDirection = _mousePosition - _playerPosition;
        Debug.Log("Aim Direction: " + aimDirection);
        float aimAngle = MathF.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        pivotTransform.rotation = Quaternion.Euler(0, 0, aimAngle);
        SetRobotLocalScale();
        
    }

    void SetRobotLocalScale()
    {
        if (canFlipPlayer)
        {
            Vector2 mouseOffset = _mousePosition - (Vector2)playerTransform.position;
            if (mouseOffset.x > 0) playerTransform.localScale = new Vector3(1, 1, 1);
            else playerTransform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
}

// Adapted from PlayerController script. 


