using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer amMainMenu;
    public Toggle toggleFullScreen;
    public TMP_Dropdown dropdownQuality;
    public Slider sliderVolume;

    public void UpdateUI()
    {
        if (PlayerPrefs.HasKey("FullScreen"))
        {
            int fullScreen = PlayerPrefs.GetInt("FullScreen");
            if (fullScreen == 1)
            {
                toggleFullScreen.isOn = true;
            }
            else
            {
                toggleFullScreen.isOn = false;
            }
        }

        if (PlayerPrefs.HasKey("Quality"))
        {
            dropdownQuality.SetValueWithoutNotify(PlayerPrefs.GetInt("Quality"));
        }

        if (PlayerPrefs.HasKey("Volume"))
        {
            sliderVolume.SetValueWithoutNotify(PlayerPrefs.GetFloat("Volume"));
        }
    }

    private void UpdateSettings()
    {
        Screen.fullScreen = toggleFullScreen.isOn;
        QualitySettings.SetQualityLevel(dropdownQuality.value);
        amMainMenu.SetFloat("MainMenuVolume", sliderVolume.value);
    }

    private void Start()
    {
        UpdateUI();
        UpdateSettings();

        gameObject.SetActive(false);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void SetVolume(float fltVolume) //Make sure to change this later in the future
    {
        amMainMenu.SetFloat("MainMenuVolume", fltVolume);
        PlayerPrefs.SetFloat("Volume", fltVolume);
    }


}
