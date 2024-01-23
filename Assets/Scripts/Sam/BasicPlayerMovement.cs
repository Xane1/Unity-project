using System;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    private float _movementDirection;
    
    [Range(1f, 10f)]
    [SerializeField] private float characterSpeed = 5f;
    
    private Rigidbody2D _rigidbody2D;

    [SerializeField] bool playerCanMove;

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) _movementDirection = Vector2.left.x;
        else if (Input.GetKey(KeyCode.D)) _movementDirection = Vector2.right.x;
        else _movementDirection = 0;

    }

    private void FixedUpdate()
    {
        if (playerCanMove) _rigidbody2D.velocity = new Vector2(_movementDirection * characterSpeed, _rigidbody2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spikes")) _gameManager.KillPlayer(this.gameObject);

    }
}

// References.
// https://youtu.be/DQYj8Wgw3O0?si=YMOKUhBwTrGhOd25
