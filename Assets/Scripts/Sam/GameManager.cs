using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float delayTime = 3;
    [SerializeField] private GameObject player;
    private Vector2 _originalPlayerPosition;
    CinemachineVirtualCamera _virtualCamera;
    private PlayerZoom _playerZoom;

    [SerializeField] CameraPosition cameraPosition;

    private void Start()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _originalPlayerPosition = FindObjectOfType<BasicPlayerMovement>().gameObject.transform.position;
        _playerZoom = FindObjectOfType<PlayerZoom>();
    }

    private void Update()
    {
        KillPlayerUsingKey();
    }

    void KillPlayerUsingKey()
    {
        if (Input.GetKey(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(delayTime); // Delays the exection of the rest of the script based on the delayTime script. 
        BulletObj[] bullets = FindObjectsOfType<BulletObj>(); // Gets all the bullets based on their BulletObj script. 
        foreach (var bullet in bullets) Destroy(bullet.gameObject); // Finds all the bullets set in the previous array and destroys them. 
        GameObject newPlayer = Instantiate(player, _originalPlayerPosition, quaternion.identity); // Creates a new player. 
        _virtualCamera.Follow = FindObjectOfType<PlayerGun>().transform;
    }
    
    public void KillPlayer(GameObject playerToBeKilled)
    {
        Destroy(playerToBeKilled.gameObject);
        StartCoroutine(RestartGame());
    }
    
    
}
