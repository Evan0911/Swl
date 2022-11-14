using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelect;
    public GameObject settingsMenu;

    public void LevelSelect()
    {
        levelSelect.SetActive(true);
    }

    public void LevelSelectBack()
    {
        levelSelect.SetActive(false);
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }
    public void SettingsButtonBack()
    {
        settingsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Battle");
    }
}
