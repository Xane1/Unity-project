using UnityEngine;
using Lean.Pool;
public class PlayerGun : MonoBehaviour
{ 
    [Header("Raycast")]
    [Range(1, 200)] [SerializeField] private float raycastLength;
    [SerializeField] private bool useRaycast;
    private bool _raycastMode;
    [SerializeField] private int maxBounces = 3;
    
    [Header("Player")]
    private Camera _playerCam;
    private Vector2 _playerPosition;
    [SerializeField] private bool canFlipPlayer;
    [SerializeField] private Transform playerTransform;
    
    [Header("Mouse")]
    private Vector2 _mousePosition;
    
    [Header("Pivot")]
    [SerializeField] private Transform pivotTransform;

    [Header("Shoot")]
    public GameObject bulletPrefab;
    private bool _playerCanShoot = true;
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
    void Fire()
    {
        if (_playerCanShoot)
        {
            if (useRaycast) _raycastMode = true;
            else LeanPool.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void FireRaycast()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(firePoint.position, firePoint.right, raycastLength);
        // Shoots out a raycast.
        if (raycastHit2D.collider != null)
        {
            Debug.Log("Hitting: " + raycastHit2D.collider.name);
            if (raycastHit2D.collider.CompareTag("Target"))
            {
                
            }
            _raycastMode = false;
            // Disables the raycast from reoccurring in FixedUpdate(), capping it's usage.
        }
    }

    private void SetPlayerRaycastLocation(RaycastHit2D raycastHit2D)
    {
        Vector2 newPlayerPosition = raycastHit2D.collider.transform.position;
        playerTransform.position = newPlayerPosition;
    }
}

// Adapted from PlayerController script. 
// Acknowledgements. 
// Thanks to Alice Bottino on Discord for helping out with the SetGunAim() and SetRobotLocalScale() functions.
// References.
// https://medium.com/@youngchae.depriest/detecting-objects-using-2d-raycasting-in-unity-40cfa9c79234

