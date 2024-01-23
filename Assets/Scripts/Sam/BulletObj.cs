using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    
    [Range(0.1f, 1f)]
    [SerializeField] private float playerMoveDuration = 0.5f;
    
    [Header("Player")]
    private PlayerGun _playerGun;
    public GameObject playerObj;
    private Transform gunPoint;
    
    [SerializeField] private bool moveViaRaycast;
    
    [Header("Bullets")]
    [SerializeField] private float bulletForce = 5;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] 
    private float bulletKillTime = 0.5f;

    [Header("Bounces")] 
    private int _bounces;
    [SerializeField] private int maxBulletBounces = 4;

    [Header("Game Manager")] 
    private GameManager _gameManager;

    [Header("Cinemachine")] 
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private bool cinemachineFollowBall = true;

    [Header("Camera Follow")] 
    Transform _cameraFollowObject;
    [SerializeField] private string cameraFollowObjectName = "Camera Follower";
    private Vector2 _camVelocity = Vector2.zero;
    [SerializeField] private float cameraSmoothTime = 0.3f;
    private bool _camIsFollowingBall;
    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        gunPoint = GameObject.FindWithTag("GunPoint").transform;
        _cameraFollowObject = GameObject.FindWithTag("CameraFollow").transform;
        UpdateFollowCamera();
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachineVirtualCamera.m_Follow = _cameraFollowObject;
        _gameManager = FindObjectOfType<GameManager>();
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateFollowCamera();
    }

    void UpdateFollowCamera()
    {
        _cameraFollowObject.transform.position = (playerObj.transform.position + transform.position)/2;
    }

    private void OnCollisionEnter2D(Collision2D other)   
    {
        if (!moveViaRaycast && !other.gameObject.CompareTag("Target")) TeleportPlayer();
        if (other.gameObject.CompareTag("ExitBlock")) TeleportPlayer();
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Spikes")) _gameManager.canKillPlayer = true;
    }

    void TeleportPlayer()
    {
        if (playerObj != null)
        {
            _rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            _spriteRenderer.enabled = false;
            _camIsFollowingBall = true;
            Tween moveTween = playerObj.transform.DOMove(transform.position, playerMoveDuration)
                .OnStart(() => _gameManager.canKillPlayer = false)
                .OnComplete(() => _gameManager.canKillPlayer = true)
                .OnKill(() => _gameManager.canKillPlayer = true);
            StartCoroutine(DestroyBullet());
        }
    }

    IEnumerator DestroyBullet()
    {
        _cinemachineVirtualCamera.m_Follow = gunPoint;
        yield return new WaitForSeconds(bulletKillTime);
        Destroy(this.gameObject);
    }
}
