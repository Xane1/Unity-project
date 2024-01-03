using System.Collections;
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
    private bool _playerIsLookingLeft;
    
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
    [SerializeField] private float bulletTime = 0.1f;

    [Header("Bullet Reflections")] 
    [SerializeField] private int maxBulletBounces = 4;

    private void Start()
    {
        _playerCam = Camera.main;
    }
    void Update()
    {
        _mousePosition = _playerCam.ScreenToWorldPoint(Input.mousePosition);
        SetGunAim();
        if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
        // if (Input.GetKeyDown(KeyCode.Mouse1)) Fire(true);
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
            if (mouseOffset.x > 0)
            {
                playerTransform.localScale = new Vector3(1, 1, 1);
                _playerIsLookingLeft = false;
            }
            else
            {
                playerTransform.localScale = new Vector3(-1, 1, 1);
                _playerIsLookingLeft = true;
            }
        }
    }
    void Fire()
    {
        if (_playerCanShoot) ShootBullet();
    }

    void ShootBullet()
    {
        Vector2 firePointPos = firePoint.position;
        GameObject newBullet = LeanPool.Spawn(bulletPrefab, firePointPos, firePoint.rotation);
        Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
        Vector2 firepointDirection;
        if (_playerIsLookingLeft) firepointDirection = -firePoint.right;
        else firepointDirection = firePoint.right;
        RaycastHit2D bulletRaycast =
            Physics2D.Raycast(firePointPos, firepointDirection, raycastLength, ~playerLayerMask);
        if (bulletRaycast.collider != null && newBulletRb != null) StartCoroutine(MoveBulletViaRaycast(newBulletRb, bulletRaycast, newBullet, firepointDirection));
    }

    private IEnumerator MoveBulletViaRaycast(Rigidbody2D newBulletRb, RaycastHit2D bulletRaycast, GameObject bullet, Vector2 firepointDirection)
    {  
        newBulletRb.DOMove(bulletRaycast.point, bulletTime);
        yield return new WaitForSeconds(bulletTime);
        if (!bulletRaycast.collider.CompareTag("Target") && 
            !bulletRaycast.collider.CompareTag("PlayerZoom")) 
            playerTransform.position = bulletRaycast.point;  
        else if (bulletRaycast.collider.CompareTag("Target"))
        {
            // Code for reflecting on walls.
        }
        Destroy(bullet.gameObject);
    }
}

// Adapted from PlayerController and Weapon script. 
// Acknowledgements. 
// Thanks to Alice Bottino on Discord for helping out with the SetGunAim() and SetRobotLocalScale() functions.
// Thanks to Guitar Kid on Discord for helping out with moving the bullet, suggesting to move the bullet via raycasting.
// References.
// https://medium.com/@youngchae.depriest/detecting-objects-using-2d-raycasting-in-unity-40cfa9c79234
// https://discussions.unity.com/t/how-can-i-have-a-raycast-ignore-a-layer-completely/116196

