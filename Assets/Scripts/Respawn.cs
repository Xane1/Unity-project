using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
    
private Transform spawnPoint;
private Transform playerPos;

void Start()
{
    playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    
}

private void OnCollisionEnter2D(Collision2D coll)
{
    if (coll.transform.tag == "Spike")
    {
        playerPos.position = spawnPoint.position;
    }
}
}