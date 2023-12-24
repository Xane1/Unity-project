using System;
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

    [Header("Bounces")] 
    [SerializeField] private int maxBounces = 2;
    int _bounces;
    // Start is called before the first frame update
    void Start()
    {
        _basicPlayerMovement = FindObjectOfType<BasicPlayerMovement>();
        _rb2d = GetComponent<Rigidbody2D>();
        _bulletStartPosition = transform.position;
        StartCoroutine(KillBullet());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("The bullet is colliding with " + other.gameObject.name);
        _bounces++;
        if (_bounces == maxBounces && !other.gameObject.CompareTag("Target") && !other.gameObject.CompareTag("Player") &&
            _basicPlayerMovement != null)
        {
            _basicPlayerMovement.SetPlayerLocation((Vector2)transform.position + Vector2.up);
            LeanPool.Despawn(this.gameObject);
        }
    }

    IEnumerator KillBullet()
    {
        yield return new WaitForSeconds(bulletKillTime);
        LeanPool.Despawn(this.gameObject);
    }
}
