using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLevelFinish : MonoBehaviour
{
    public LazerScript thisLazer;
    public LazerScript secondLazer;
    public string tagReflector_1 = "";
    public string tagReflector_2 = "";

    public Dialogue endDialogue;

    public AudioSource correctSound;

    void Update()
    {
        if (thisLazer.timeSinceLastPower < 0.01f && secondLazer.timeSinceLastPower < 0.01f) {
            if ((thisLazer.tagHitBy.Equals(tagReflector_1) || thisLazer.tagHitBy.Equals(tagReflector_2)) &&
                (secondLazer.tagHitBy.Equals(tagReflector_1) || secondLazer.tagHitBy.Equals(tagReflector_2))) {
                correctSound.Play();
                DialogueSystem.CreateDialogue(endDialogue);
                enabled = false;
            }
        }
    }
}
