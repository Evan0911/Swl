using System.Collections;
using System.Collections.Generic;
using TouchScript;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject pauseScreen;

    public static BattleManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de BattleManager dans la scène");
            return;
        }

        instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Win()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
