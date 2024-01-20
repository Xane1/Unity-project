using System.Collections;
using UnityEngine;
using Lean.Pool;
using DG.Tweening;
public class PlayerGun : MonoBehaviour
{ 
    [Header("Raycast")]
    [Range(1, 200)] [SerializeField] private float raycastLength;
    private bool _raycastMode;
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
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bulletWithoutRaycast;
    [Range(1, 100)]
    [SerializeField] private float bulletForce = 5;
    [SerializeField] private float bulletTime = 0.1f;
    [SerializeField] private float bulletTimeMultiplier = 5;
    [SerializeField] private bool shootBulletViaRaycast;
    GameObject spawnedBullet;
    
    [Header("Sounds")] 
    [SerializeField] private AudioClip lazerSuccess;
    [SerializeField] private AudioClip lazerFail;
    [Range(0, 1)] [SerializeField] private float lazerVolume = 1;
    

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
            Vector2 playerLocalScale = (Vector2)playerTransform.localScale;
            Debug.Log("Player Local Scale before setting: " + playerLocalScale);
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

    Rigidbody2D SpawnNewBullet(Vector2 bulletPosition, GameObject bulletObj)
    {
        if (spawnedBullet != null) Destroy(spawnedBullet);
        AudioSource.PlayClipAtPoint(lazerSuccess, transform.position, lazerVolume);
        GameObject newBullet = LeanPool.Spawn(bulletObj, bulletPosition, firePoint.rotation);
        spawnedBullet = newBullet;
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        return bulletRb;
    }

    void ShootBullet()
    {
        Vector2 firePointPos = firePoint.position;
        Vector2 firepointDirection;
        if (_playerIsLookingLeft) firepointDirection = -firePoint.right;
        else firepointDirection = firePoint.right;
        if (shootBulletViaRaycast)
        {
            RaycastHit2D bulletRaycast =
                Physics2D.Raycast(firePointPos, firepointDirection, raycastLength, ~playerLayerMask);
            if (bulletRaycast.collider != null) StartCoroutine(MoveBulletViaRaycast(bulletRaycast, firePointPos, firepointDirection));
            else AudioSource.PlayClipAtPoint(lazerFail, transform.position, lazerVolume);
        }
        else if (!shootBulletViaRaycast)
        {
            Rigidbody2D newBullet = SpawnNewBullet(firePointPos, bulletWithoutRaycast);
            newBullet.velocity = firepointDirection * bulletForce;
        }
    }

    private IEnumerator MoveBulletViaRaycast(RaycastHit2D bulletRaycast, Vector2 firepointPosition, Vector2 firepointDirection)
    {  
        Rigidbody2D newBulletRb = SpawnNewBullet(firepointPosition, bulletPrefab);
        newBulletRb.DOMove(bulletRaycast.point, bulletTime);
        yield return new WaitForSeconds(bulletTime);
        if (!bulletRaycast.collider.CompareTag("Target") &&
            !bulletRaycast.collider.CompareTag("PlayerZoom") && bulletRaycast.collider != null) 
            playerTransform.position = bulletRaycast.point;
        if (newBulletRb != null)
        {
            Destroy(newBulletRb.gameObject);
        }
    }
}

// Adapted from PlayerController and Weapon script. 
// Acknowledgements. 
// Thanks to Alice Bottino on Discord for helping out with the SetGunAim() and SetRobotLocalScale() functions.
// Thanks to Guitar Kid on Discord for helping out with moving the bullet, suggesting to move the bullet via raycasting.
// References.
// https://medium.com/@youngchae.depriest/detecting-objects-using-2d-raycasting-in-unity-40cfa9c79234
// https://discussions.unity.com/t/how-can-i-have-a-raycast-ignore-a-layer-completely/116196

