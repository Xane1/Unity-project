using UnityEngine;

public class MovingWallImproved : MonoBehaviour
{
    [Header("X Movement")]
    public float ampX;
    public float freqX;
    
    [Header("Y Movement")]
    public float ampY;
    public float freqY;
    
    [Header("Components")]
    Vector3 _initPos;
    private Rigidbody2D _rigidbody2D;

    [Header("Game Object")] 
    [SerializeField] private Transform playerPoint;

    [SerializeField] private bool parentPlayerToMovingWall;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _initPos = transform.position;
    }
    private void Update()
    {
        _rigidbody2D.position = new Vector3(Mathf.Sin(Time.time * freqX) * ampX + _initPos.x, 
            Mathf.Sin(Time.time * freqY) * ampY + _initPos.y, 0);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && parentPlayerToMovingWall)
            col.transform.SetParent(playerPoint, true);


    }

    private void OnCollisionExit2D(Collision2D col)
    {
       if (col.gameObject.CompareTag("Player") && parentPlayerToMovingWall) 
           col.transform.SetParent(playerPoint, true);
    }
}
// References.
// https://youtu.be/O6wlIqe2lTA?si=rX00dHgfEIrbYXEe