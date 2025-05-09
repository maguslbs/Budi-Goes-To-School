using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject objPausePanel;

    private bool isPause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Resume()
    {
        isPause = !isPause;
        objPausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        isPause = !isPause;

        if (isPause)
        {
            objPausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            objPausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Settings()
    {
        Time.timeScale = 1f;
    }   
    

    public void MainMenu()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.StopMusicAudio();
        AudioManager.Instance.StopSfxAudio();
        SceneManager.LoadScene("MainMenuScene");
    }
}
