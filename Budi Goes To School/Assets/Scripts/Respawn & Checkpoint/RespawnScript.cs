using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] public GameObject objPlayer;
    [SerializeField] public GameObject respawnPoint;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objPlayer.transform.position = respawnPoint.transform.position;
        }
    }
}
