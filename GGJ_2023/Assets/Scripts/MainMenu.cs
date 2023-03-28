using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "IntroScene")
        {
            StartCoroutine(SeeSoundWarning());
        }
        if (SceneManager.GetActiveScene().name == "SoundInformation")
        {
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator SeeSoundWarning()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("SoundInformation");
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GoAroundCircles");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
