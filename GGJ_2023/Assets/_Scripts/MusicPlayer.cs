using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public string destroyInScene;
    

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public AudioSource musicSource;

    private void Start()
    {
        musicSource.Play();
    }

    void Update()
    {
        if (destroyInScene == SceneManager.GetActiveScene().name)
        {
            Destroy(gameObject);
        }
    }

}
