using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadingSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {
        bool Fullscreen;
        if (PlayerPrefs.HasKey("ResolutionWidth") && PlayerPrefs.HasKey("ResolutionHeight") && PlayerPrefs.HasKey("Fullscreen"))
        {
            if (PlayerPrefs.GetInt("Fullscreen") == 1)
            {
                Fullscreen = true;
            }
            else
            {
                Fullscreen = false;
            }
            Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth"), PlayerPrefs.GetInt("ResolutionHeight"), Fullscreen);
        }
        if (PlayerPrefs.HasKey("Master"))
        {
            audioMixer.SetFloat("master", PlayerPrefs.GetFloat("Master"));
        }
        if (PlayerPrefs.HasKey("SFX"))
        {
            audioMixer.SetFloat("sfx", PlayerPrefs.GetFloat("SFX"));
        }
        if (PlayerPrefs.HasKey("Music"))
        {
            audioMixer.SetFloat("music", PlayerPrefs.GetFloat("Music"));
        }
    }
}
