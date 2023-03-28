using System;
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

    public PickupController playersPickup;
    public Rigidbody[] rigidbodies;

    void Update()
    {
        // Yes, I don't have time for a sensible solution, how did you know? ~Jan
        if (thisLazer.timeSinceLastPower < 0.01f && secondLazer.timeSinceLastPower < 0.01f && playersPickup.timeSinceHoldingObject > 0.2f && AreRigidsZeroVelocity()) {
            if ((thisLazer.tagHitBy.Equals(tagReflector_1) || thisLazer.tagHitBy.Equals(tagReflector_2)) &&
                (secondLazer.tagHitBy.Equals(tagReflector_1) || secondLazer.tagHitBy.Equals(tagReflector_2))) {
                correctSound.Play();
                DialogueSystem.CreateDialogue(endDialogue);
                enabled = false;
            }
        }
    }

    private bool AreRigidsZeroVelocity()
    {
        foreach (var item in rigidbodies)
        {
            if(item.velocity.sqrMagnitude > 0.01f)
            {
                return false;
            }
        }
        return true;
    }
}
