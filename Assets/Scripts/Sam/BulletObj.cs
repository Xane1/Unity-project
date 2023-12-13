using UnityEngine;

public class BulletObj : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    [Range(20, 200)]
    [SerializeField] private float bulletForce = 50;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
    }
}
