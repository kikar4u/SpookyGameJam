using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool gameOver = false;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseUI;
    public bool isWin = false;

    void Start()
    {
        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
    }
    public void GameOver(bool isGameOver)
    {
        gameOver = isGameOver;
        gameOverUI.SetActive(true);
        if (isGameOver)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }


    }
    public void pauseMenu()
    {
        if (pauseUI.activeSelf)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1.0f;
            
        }
        else
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0.0f;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && gameOver)
        {
            SceneManager.LoadScene("Level1");
            GameOver(false);
        }
        if (Input.GetButtonDown("Start"))
        {
            pauseMenu();
        }
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevel()
    {
        
        SceneManager.LoadScene("Level1");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Application.OpenURL("about:blank");
#endif
        Application.Quit();
    }
}
