using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoSS1 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("SampleScene 1", LoadSceneMode.Single);
    }
}
