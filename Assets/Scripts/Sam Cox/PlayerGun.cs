using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{ 
    public Weapon weapon;
    private Vector2 _mousePosition;
    private Camera _playerCam;
    private Vector2 _playerPosition;
    [SerializeField] private float offset;
    private PlayerMovement _playerMovement;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform pivotTransform;
    [SerializeField] private float2x2 angleRange;
    
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 2f;
    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _playerCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Fire();
        _mousePosition = _playerCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        SetGunAim();
    }

    private void SetGunAim()
    {
        _playerPosition = pivotTransform.position;
        Vector2 aimDirection = _mousePosition - _playerPosition;
        Debug.Log("Aim Direction: " + aimDirection);
        float aimAngle = MathF.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - offset;
        pivotTransform.rotation = Quaternion.Euler(0, 0, aimAngle);
        if (aimDirection.x > 0) playerTransform.localScale = new Vector3(1, 1, 1);
        else if (aimDirection.x < 0) playerTransform.localScale = new Vector3(-1, 1, 1);
    }
    
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
}

// Adapted from PlayerController script. 


