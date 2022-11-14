using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown cboResolution;

    Resolution[] resolutions;

    private void Start()
    {
        //Sélectionne les résolutions disponibles pour l'écran utilisé sans doublon
        resolutions = Screen.resolutions.Select(Resolution => new Resolution { width = Resolution.width, height = Resolution.height }).Distinct().ToArray();
        cboResolution.ClearOptions();
        List<string> options = new List<string>();
        foreach(Resolution uneRes in resolutions)
        {
            options.Add(uneRes.width + " x " + uneRes.height);

            if (Screen.width == uneRes.width && Screen.height == uneRes.height)
            {
                cboResolution.value = cboResolution.options.Count() - 1;
                cboResolution.RefreshShownValue();
            }
        }
        cboResolution.AddOptions(options);
    }

    public void MasterChanged(float value)
    {
        audioMixer.SetFloat("master", value);
    }

    public void SFXChanged(float value)
    {
        audioMixer.SetFloat("sfx", value);
    }

    public void MusicChanged(float value)
    {
        audioMixer.SetFloat("music", value);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
    }

    public void SaveParameters()
    {
        float volume;
        audioMixer.GetFloat("master", out volume);
        PlayerPrefs.SetFloat("Master", volume);
        audioMixer.GetFloat("sfx", out volume);
        PlayerPrefs.SetFloat("SFX", volume);
        audioMixer.GetFloat("master", out volume);
        PlayerPrefs.SetFloat("Master", volume);
        if (Screen.fullScreen == true)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        PlayerPrefs.SetInt("ResolutionWidth", Screen.currentResolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", Screen.currentResolution.height);
        PlayerPrefs.Save();
    }
}
