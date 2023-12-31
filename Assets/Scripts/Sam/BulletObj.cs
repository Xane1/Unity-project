using System.Collections;
using Lean.Pool;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    [Range(20, 200)]
    [SerializeField] private float bulletForce = 50;
    private Vector2 _bulletStartPosition;
    private PlayerGun _playerGun;
    private GameObject _playerObj;
    private BasicPlayerMovement _basicPlayerMovement;
    [SerializeField] private float bulletKillTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _basicPlayerMovement = FindObjectOfType<BasicPlayerMovement>();
        _rb2d = GetComponent<Rigidbody2D>();
        _bulletStartPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Target")) StartCoroutine(TeleportPlayer(other.transform));

    }

    IEnumerator TeleportPlayer(Transform playerTransform)
    {
        yield return new WaitForSeconds(bulletKillTime);
        LeanPool.Despawn(this.gameObject);
    }
}
