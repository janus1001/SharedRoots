using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Dialogue dialogue;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DialogueSystem.CreateDialogue(dialogue);
        }    
    }
}
