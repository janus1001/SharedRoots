using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public AudioSource correctSound;

    public Dialogue aaa;
    bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish") && !triggered)
        {
            triggered = true;
            DialogueSystem.CreateDialogue(aaa);
            correctSound.Play();
        }
    }
}
