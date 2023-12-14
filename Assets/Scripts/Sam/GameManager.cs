using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float delayTime = 3;
    [SerializeField] private GameObject player;
    private Vector2 _originalPlayerPosition;

    [SerializeField] CameraPosition cameraPosition;

    private void Start()
    {
        _originalPlayerPosition = player.transform.position;
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(delayTime);
        GameObject newPlayer = Instantiate(player, _originalPlayerPosition, quaternion.identity);
        cameraPosition.UpdatePlayerTransform(newPlayer.transform);
        
    }
}
