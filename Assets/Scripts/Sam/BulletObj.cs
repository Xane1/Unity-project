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
    
    [SerializeField] private bool moveViaRaycast;
    
    [Header("Bullets")]
    [SerializeField] private float bulletForce = 5;

    [Header("Bounces")] 
    private int _bounces;
    [SerializeField] private int maxBulletBounces = 4;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D other)   
    {
        if (!moveViaRaycast && !other.gameObject.CompareTag("Target")) TeleportPlayer();
        if (other.gameObject.CompareTag("Player")) Destroy(this.gameObject);
    }

    void TeleportPlayer()
    {
        if (playerObj != null)
        {
            // playerObj.transform.position = transform.position;
            playerObj.transform.DOMove(transform.position, playerMoveDuration);
            Destroy(this.gameObject);
        }
    }
}
