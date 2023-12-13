using UnityEngine;
using Lean.Pool;
public class PlayerGun : MonoBehaviour
{ 
    private Vector2 _mousePosition;
    private Camera _playerCam;
    private Vector2 _playerPosition;
    [SerializeField] private bool canFlipPlayer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform pivotTransform;
    public GameObject bulletPrefab;
    private bool _playerCanShoot = true;
    private bool _useRaycast;
    private bool _raycastMode;
    public Transform firePoint;
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

    void FixedUpdate()
    {
        if (_raycastMode) FireRaycast();
    }
    private void SetGunAim()
    {
        SetRobotLocalScale();
        _playerPosition = pivotTransform.position;
        Vector2 aimDirection = _mousePosition - _playerPosition;
        pivotTransform.localRotation =
            Quaternion.FromToRotation(Vector2.right, aimDirection * playerTransform.localScale);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gun is colliding with " + other.name);
        if (other.gameObject.CompareTag("Tilemap")) _playerCanShoot = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Gun is no longer colliding with " + other.name);
        _playerCanShoot = true;
    }

    void Fire()
    {
        if (_playerCanShoot)
        {
            if (_useRaycast) _raycastMode = true;
            else LeanPool.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void FireRaycast()
    {
        
    }
}

// Adapted from PlayerController script. 
// Acknowledgements. 
// Thanks to Alice Bottino on Discord for helping out with the SetGunAim() and SetRobotLocalScale() functions.


