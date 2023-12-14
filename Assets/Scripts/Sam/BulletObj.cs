using Lean.Pool;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    [Range(20, 200)]
    [SerializeField] private float bulletForce = 50;

    private PlayerGun _playerGun;
    // Start is called before the first frame update
    void Start()
    {
        _playerGun = FindObjectOfType<PlayerGun>();
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Target")) TargetHit(other.gameObject.transform.position);
        LeanPool.Despawn(this);
    }

    void TargetHit(Vector2 targetLocation)
    {
        
    }
}
