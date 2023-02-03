using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject _gameOverScreen;
    // Update is called once per frame
    void Update()
    {
        if (Helpers._puzzleSolved)
        {
            _gameOverScreen.SetActive(true);
        }
    }
}
