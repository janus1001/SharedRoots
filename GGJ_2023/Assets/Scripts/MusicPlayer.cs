using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public string destroyInScene;
    public static MusicPlayer instance;

    public AudioSource musicSource;

    void Start()
    {
        if (instance != null)
        {
            if (instance.musicSource.clip == musicSource.clip)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(instance.gameObject);
            }
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        musicSource.Play();
    }

    void Update()
    {
        //if (destroyInScene == SceneManager.GetActiveScene().name)
        //{
        //    Destroy(gameObject);
        //}
    }

}
