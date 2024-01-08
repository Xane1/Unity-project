using UnityEngine;
using UnityEngine.Serialization;

public class BulletObj : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    
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
        if (!moveViaRaycast)
        {
            _bounces++;
            if (_bounces == maxBulletBounces) TeleportPlayer(other.transform.position);
        }
    }

    void TeleportPlayer(Vector2 newLocation)
    {
        if (playerObj != null) playerObj.transform.position = newLocation;
    }
}
