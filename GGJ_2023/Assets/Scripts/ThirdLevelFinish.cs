using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLevelFinish : MonoBehaviour
{
    public LazerScript thisLazer;
    public LazerScript secondLazer;

    public Dialogue endDialogue;

    public AudioSource correctSound;

    void Update()
    {
        if (thisLazer.timeSinceLastPower < 0.01f && secondLazer.timeSinceLastPower < 0.01f)
        {
            correctSound.Play();
            DialogueSystem.CreateDialogue(endDialogue);
            enabled = false;
        }
    }
}
