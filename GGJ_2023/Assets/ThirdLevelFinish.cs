using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLevelFinish : MonoBehaviour
{
    public LazerScript thisLazer;
    public LazerScript secondLazer;

    public Dialogue endDialogue;

    // Update is called once per frame
    void Update()
    {
        if (thisLazer.timeSinceLastPower < 0.01f && secondLazer.timeSinceLastPower < 0.01f)
        {
            Debug.Log("win");
            DialogueSystem.CreateDialogue(endDialogue);
            enabled = false;
        }
    }
}
