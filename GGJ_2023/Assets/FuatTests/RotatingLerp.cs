using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RotatingLerp : MonoBehaviour
{
    public GameObject otherCube;
    public float timeDelta = 0;
    public float desiredDuration = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeDelta += Time.deltaTime;


        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(otherCube.transform.position - transform.position), Time.deltaTime);
    }
}
