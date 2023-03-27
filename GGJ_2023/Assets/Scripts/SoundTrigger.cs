using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] ParticleSystem _sFXParticle;
    [SerializeField] PuzzleTrigger _puzzleTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_puzzleTrigger._puzzleTriggerSolved)
        {
            _audioSource.Play();
            _sFXParticle.Play();
        }
    }
}
