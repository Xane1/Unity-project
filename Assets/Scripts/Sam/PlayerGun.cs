using UnityEngine;
using Lean.Pool;
using DG.Tweening;
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
    private bool _playerCanShoot = true;


    [Header("Gizmos")] 
    private Vector2 _gizmoTo;
    private bool _startGizmoDebug;

    [Header("Bullet")]
    [SerializeField] private bool useRaycastForBullet;
    public Transform firePoint;
    public float bulletForce = 20f;
    public GameObject bulletPrefab;

    private void Start()
    {
        _playerCam = Camera.main;
    }
    void Update()
    {
        _mousePosition = _playerCam.ScreenToWorldPoint(Input.mousePosition);
        SetGunAim();
        if (Input.GetKeyDown(KeyCode.Mouse0)) Fire(false);
        if (Input.GetKeyDown(KeyCode.Mouse1)) Fire(true);
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
            else if (!fireRaycast) ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject newBullet = LeanPool.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
        if (useRaycastForBullet)
        {
            RaycastHit2D bulletRaycast =
                Physics2D.Raycast(firePoint.position, firePoint.right, raycastLength, ~playerLayerMask);
            if (bulletRaycast.collider != null)
            {
                newBulletRb.DOMove(bulletRaycast.point, 0.1f);
            }
        }
        else newBulletRb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse); 
    }
    void FireRaycast()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(firePoint.position, firePoint.right, 
            raycastLength, ~playerLayerMask);
        // Shoots out a raycast.
        if (raycastHit2D.collider != null)
        {
            RaycastHitMessage(raycastHit2D, 1, firePoint.position, 
                raycastHit2D.point);
            if (raycastHit2D.collider.CompareTag("Target"))
            {
                RaycastHit2D reflectionRaycast2D = Physics2D.Raycast(raycastHit2D.point,
                    Vector2.Reflect(firePoint.right, raycastHit2D.normal));
                if (reflectionRaycast2D.collider != null && !reflectionRaycast2D.collider.CompareTag("Target")) 
                    playerTransform.position = reflectionRaycast2D.point + Vector2.up;
                RaycastHitMessage(reflectionRaycast2D, 2, raycastHit2D.point, 
                    reflectionRaycast2D.point);
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
}

// Adapted from PlayerController and Weapon script. 
// Acknowledgements. 
// Thanks to Alice Bottino on Discord for helping out with the SetGunAim() and SetRobotLocalScale() functions.
// Also thanks to Alice Bottino and theChief on Discord for helping out with some of the code in FireRaycast().
// References.
// https://medium.com/@youngchae.depriest/detecting-objects-using-2d-raycasting-in-unity-40cfa9c79234
// https://discussions.unity.com/t/how-can-i-have-a-raycast-ignore-a-layer-completely/116196

