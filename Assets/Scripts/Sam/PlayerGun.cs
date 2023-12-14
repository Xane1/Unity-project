using UnityEngine;
using Lean.Pool;
public class PlayerGun : MonoBehaviour
{ 
    [Header("Raycast")]
    [Range(1, 200)] [SerializeField] private float raycastLength;
    [SerializeField] private bool useRaycast;
    private bool _raycastMode;
    [SerializeField] private int maxBounces = 3;
    public LayerMask playerLayerMask;
    
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

    [Header("Gizmos")] 
    private Vector2 _gizmoTo;
    private bool _startGizmoDebug;
    private void Start()
    {
        _playerCam = Camera.main;
    }
    void Update()
    {
        _mousePosition = _playerCam.ScreenToWorldPoint(Input.mousePosition);
        SetGunAim();
        if (Input.GetMouseButtonDown(0)) Fire(false);
        else if (Input.GetMouseButtonDown(1)) Fire(true);
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
    void Fire(bool fireRaycast)
    {
        if (_playerCanShoot)
        {
            if (useRaycast && fireRaycast) _raycastMode = true;
            else if (!fireRaycast) LeanPool.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
    void FireRaycast()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(firePoint.position, firePoint.right, raycastLength, ~playerLayerMask);
        // Shoots out a raycast.
        if (raycastHit2D.collider != null)
        {
            RaycastHitMessage(raycastHit2D, 1, firePoint.position, raycastHit2D.collider.transform.position);
            if (raycastHit2D.collider.CompareTag("Target"))
            {
                Vector2 inDirection = (raycastHit2D.transform.position - firePoint.transform.position).normalized;
                Debug.Log("indirection: " + inDirection);
                RaycastHit2D reflectionRaycast2D = Physics2D.Raycast(raycastHit2D.transform.position,
                    Vector2.Reflect(inDirection, Vector2.down));
                RaycastHitMessage(reflectionRaycast2D, 2, raycastHit2D.transform.position, reflectionRaycast2D.collider.transform.position);
            }
                _raycastMode = false;
            // Disables the raycast from reoccurring in FixedUpdate(), capping it's usage.
        }
    }
    private static void RaycastHitMessage(RaycastHit2D raycastHit2D, int raycastNumber, Vector2 hitStart, Vector2 hitEnd)
    {
        Debug.Log("raycastHit2D" + raycastNumber + " hitting: " + raycastHit2D.collider.name);
        Debug.DrawLine(hitStart, hitEnd, Color.white, 1f);
        
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

