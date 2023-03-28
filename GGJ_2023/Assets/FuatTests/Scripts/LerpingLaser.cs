using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpingLaser : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition = new Vector3(0, 5, 0);
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float desiredDuration = 5;
    [SerializeField] private float timeElapsed;
    [SerializeField] private AnimationCurve animCurve;

    [SerializeField] private bool bAnimateLerp = false;
    [SerializeField] private float lerpProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = GameObject.Find("Cube1").transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            transform.position = startPosition;
            timeElapsed = 0;
            bAnimateLerp = true;
            Debug.Log("GetKeyDown()");
        }
        else {
        }

        if (Input.GetKey(KeyCode.A)) {
            
            
            Debug.Log("GetKey()");
        }

        if (bAnimateLerp) {
            timeElapsed += Time.deltaTime;
            lerpProgress = animCurve.Evaluate(timeElapsed / desiredDuration);
            transform.position = Vector3.Lerp(startPosition, endPosition, lerpProgress);
            if (lerpProgress >= 1) {
                bAnimateLerp = false;
                transform.position = Vector3.Lerp(startPosition, endPosition, lerpProgress);
                Debug.Log("lerp finished");
            }
        }
    }

    private void FixedUpdate() {

    }
}
