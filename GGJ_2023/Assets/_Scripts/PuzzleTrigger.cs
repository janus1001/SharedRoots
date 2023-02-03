using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private float counter = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && counter < 2)
        {
            _audioSource.Play();
            counter++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PuzzlePiece")
        {
            var puzzlePiece = other.gameObject;

            if (puzzlePiece.transform.parent == null)
            {
                if (puzzlePiece.GetComponent<AudioSource>().clip == this._audioSource.clip)
                {
                    Helpers._puzzleSolved = true;
                    puzzlePiece.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
        }
    }
}