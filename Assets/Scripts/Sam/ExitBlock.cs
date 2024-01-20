using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBlock : MonoBehaviour
{
    private int _nextScene;
    [SerializeField] bool lastLvl;
    [SerializeField] int mainMenuSceneNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!lastLvl) _nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        else if (lastLvl) _nextScene = mainMenuSceneNumber;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) SceneManager.LoadScene(_nextScene);
    }
}
