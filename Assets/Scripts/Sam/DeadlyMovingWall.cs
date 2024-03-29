using UnityEngine;

public class DeadlyMovingWall : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D _rigidbody2D;

    [SerializeField] Vector2 movementVelocity;

    GameManager gameManager;

    float transformX;

    [SerializeField] private bool isVerticalWall;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        transformX = transform.position.x;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity += movementVelocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movementVelocity = -movementVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _rigidbody2D.velocity.y < 0)
        {
            KillPlayer(collision.gameObject);
        }
    }

    void KillPlayer(GameObject player)
    {
        Destroy(player);
        StartCoroutine(gameManager.RestartGame());
    }
}
