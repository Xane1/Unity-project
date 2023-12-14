using System;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) KillPlayer(other.gameObject);
    }
    void KillPlayer(GameObject player)
    {
        Destroy(player);
        StartCoroutine(_gameManager.RestartGame());
    }
}
