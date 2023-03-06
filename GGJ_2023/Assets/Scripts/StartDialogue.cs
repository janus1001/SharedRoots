using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public Dialogue dialogueToRun;

    void Start()
    {
        DialogueSystem.CreateDialogue(dialogueToRun);
    }
}
