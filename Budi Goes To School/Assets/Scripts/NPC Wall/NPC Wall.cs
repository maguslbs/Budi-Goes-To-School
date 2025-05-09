using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWall : MonoBehaviour
{
    private Dialogue dialogueScript;

    void Start()
    {
        dialogueScript = GetComponent<Dialogue>();
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
