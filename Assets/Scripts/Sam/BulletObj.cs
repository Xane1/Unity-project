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
    // Start is called before the first frame update
    void Start()
    {
        _basicPlayerMovement = FindObjectOfType<BasicPlayerMovement>();
        _rb2d = GetComponent<Rigidbody2D>();
        _bulletStartPosition = transform.position;
        _rb2d.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Target") && !other.gameObject.CompareTag("Player") && _basicPlayerMovement != null) _basicPlayerMovement.SetPlayerLocation(transform.position);
        Destroy(this);
    }
}
