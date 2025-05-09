using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingController : MonoBehaviour
{

    public void SaveGame()
    {
        SavingSystem.instance.SaveGame();
    }

    public void LoadGame()
    {
        SavingSystem.instance.LoadGame();
    }

    public void StartButton()
    {
        SavingSystem.instance.StartButton();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
