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
    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _playerCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) weapon.Fire();
        _mousePosition = _playerCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        SetGunAim();
    }

    private void SetGunAim()
    {
        _playerPosition = transform.position;
        Vector2 aimDirection = _mousePosition - _playerPosition;
        float aimAngle = MathF.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - offset;
        // if (_playerMovement.localScaleX == -1) aimAngle = -aimAngle;
        Debug.Log("Aim Angle: " + aimAngle);
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }
}

// Adapted from PlayerController script. 


