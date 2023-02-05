using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject _gameOverScreen;
    public GameObject _pauseScreen;
    public Dialogue _endDialogue;

    private void Start()
    {
        Helpers._puzzleSolved = false;
        Helpers._piecesInPlace = 0;
        _gameOverScreen.SetActive(false);
        _pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Helpers._piecesInPlace >= 3)
        {
            Helpers._puzzleSolved = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        _pauseScreen.SetActive(!_pauseScreen.activeSelf);
        if (_pauseScreen.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1f;
    }
}
