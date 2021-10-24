using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager cela;

    public static GameManager instance
    {
        get
        {
            if (!cela) cela = FindObjectOfType<GameManager>();
            return cela;
        }
    }
    
    // Start is called before the first frame update
    
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject winUI;
    private bool isWin;
    private bool gameOver;

    void Start()
    {
        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
        winUI.SetActive(false);
    }
    public void GameOver(bool isGameOver)
    {
        gameOver = isGameOver;
        gameOverUI.SetActive(true);
        DoPause(isGameOver);
    }

    public void Win()
    {
        isWin = true;
        winUI.SetActive(true);
        DoPause(true);
    }

    private void DoPause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
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
        if (Input.GetButtonDown("Submit") && (gameOver || isWin))
        {
            SceneManager.LoadScene("Level1");
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
