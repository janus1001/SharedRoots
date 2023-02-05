using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PuzzlePiece")
        {
            var puzzlePiece = other.gameObject;

            if (puzzlePiece.transform.parent == null)
            {
                if (puzzlePiece.GetComponent<AudioSource>().clip == this._audioSource.clip)
                {
                    Helpers._piecesInPlace++;
                    puzzlePiece.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }
            }
        }
    }
}