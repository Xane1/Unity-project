using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBlock : MonoBehaviour
{
    private int _nextSceme;
    // Start is called before the first frame update
    void Start()
    {
        _nextSceme = SceneManager.GetActiveScene().buildIndex + 1;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) SceneManager.LoadScene(_nextSceme);
    }
}
