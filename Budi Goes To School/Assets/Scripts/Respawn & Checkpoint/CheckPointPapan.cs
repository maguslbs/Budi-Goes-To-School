using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPapan : MonoBehaviour
{
    private RespawnScript respawn;
    private bool isInCheckPointPapan;

    void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnScript>();
        isInCheckPointPapan = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInCheckPointPapan)
        {
            CheckPointPapan();
        }
    }

    private void CheckPointPapan()
    {
        respawn.respawnPoint = this.gameObject;

        // Find all enemies and update their respawn points
        RespawnScript[] enemies = FindObjectsOfType<RespawnScript>();
        foreach (RespawnScript enemy in enemies)
        {
            enemy.respawnPoint = this.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            isInCheckPointPapan = true;
        }
    }
}
