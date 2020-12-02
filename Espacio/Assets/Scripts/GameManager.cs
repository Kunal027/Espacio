using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCoopMode = false;

    [SerializeField]
    private bool _isGameOver;

    [SerializeField]
    private GameObject _pauseMenuPannel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPannel.SetActive(true);
            Time.timeScale = 0;
        }


    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void ResumeGame()
    {
        _pauseMenuPannel.SetActive(false);
        Time.timeScale = 1;
    }
}
