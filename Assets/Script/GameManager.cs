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

    void Start()
    {
        gameOverUI.SetActive(false);
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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && gameOver)
        {
            SceneManager.LoadScene("Level1");
            GameOver(false);
        }
    }
}
