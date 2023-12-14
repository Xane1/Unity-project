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
    private PlayerMovement _playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        _playerGun = FindObjectOfType<PlayerGun>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerObj = _playerObj.gameObject;
        _rb2d = GetComponent<Rigidbody2D>();
        _bulletStartPosition = transform.position;
        _rb2d.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            Vector2 inDirection = ((Vector2)other.transform.position - _bulletStartPosition).normalized;
            _rb2d.AddForce(Vector2.Reflect(inDirection, Vector2.down) * bulletForce, ForceMode2D.Impulse);
        }
        else TeleportPlayer(other.gameObject.transform.position, this.gameObject);
    }

    IEnumerator TeleportPlayer(Vector2 teleportLocation, GameObject bulletObj)
    {
        _playerObj.transform.position = teleportLocation + Vector2.up;
        yield return new WaitForSeconds(0.1f);
        Destroy(bulletObj);
    }
}
