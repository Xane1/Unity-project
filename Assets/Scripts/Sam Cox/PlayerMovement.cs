using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _movementDirection;
    
    [Range(1f, 10f)]
    [SerializeField] private float characterSpeed = 5f;
    
    private Rigidbody2D _rigidbody2D;

    public int localScaleX;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) _movementDirection = Vector2.left.x;
        else if (Input.GetKey(KeyCode.D)) _movementDirection = Vector2.right.x;
        else _movementDirection = 0;
        
        if (_movementDirection < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (_movementDirection > 0) transform.localScale = new Vector3(1, 1, 1);
        localScaleX = (int)transform.localScale.x;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_movementDirection * characterSpeed, _rigidbody2D.velocity.y);
    }
}

// References.
// https://discussions.unity.com/t/possible-to-convert-a-float-into-an-int/18453