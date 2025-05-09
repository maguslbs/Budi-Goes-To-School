using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (vCam != null && collision.gameObject.tag == "Player")
        {
            vCam.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (vCam != null && collision.gameObject.tag == "Player")
        {
            vCam.gameObject.SetActive(false);
        }
    }
}
