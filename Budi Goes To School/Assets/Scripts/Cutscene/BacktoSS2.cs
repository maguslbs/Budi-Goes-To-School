using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoSS2 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("SampleScene 2", LoadSceneMode.Single);
    }
}
