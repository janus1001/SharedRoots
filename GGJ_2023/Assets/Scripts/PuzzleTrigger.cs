using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _particleEffectPrefab;
    [SerializeField] private float _particleOffset;
    [SerializeField] private Dialogue _wrongPuzzleDialogue;
    [SerializeField] private Dialogue _gameOverDialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PuzzlePiece")
        {
            var puzzlePiece = other.gameObject;

            if (puzzlePiece.transform.parent == null)
            {
                if (puzzlePiece.GetComponent<AudioSource>().clip == this._audioSource.clip)
                {
                    puzzlePiece.tag = "Untagged";

                    Helpers._piecesInPlace++;
                    puzzlePiece.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    Destroy(Instantiate(_particleEffectPrefab, new Vector3(this.transform.position.x, this.transform.position.y - _particleOffset, this.transform.position.z), Quaternion.identity), 3);
                    Debug.Log(Helpers._piecesInPlace);

                    if (Helpers._puzzleSolved)
                    {
                        Debug.Log("Here");
                        DialogueSystem.CreateDialogue(_gameOverDialogue);
                    }
                }
                else
                {
                    DialogueSystem.CreateDialogue(_wrongPuzzleDialogue);
                }
            }
        }
    }
}