using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private RespawnScript respawn;

    void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.respawnPoint = this.gameObject;

            // Find all enemies and update their respawn points
            RespawnScript[] enemies = FindObjectsOfType<RespawnScript>();
            foreach (RespawnScript enemy in enemies)
            {
                enemy.respawnPoint = this.gameObject;
            }
        }
    }
}
