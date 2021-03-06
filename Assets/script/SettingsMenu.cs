﻿using UnityEngine.Audio;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
