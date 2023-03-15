using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneOnKeyPress : MonoBehaviour
{
    public string sceneName;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
