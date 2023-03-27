using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicIfIdle : MonoBehaviour
{
    private const float timeUntilKinematic = 0.5f;
    Rigidbody rb;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (rb.velocity.magnitude < 0.01f)
        {
            timer += Time.fixedDeltaTime;

            if(timer > timeUntilKinematic)
            {
                //rb.isKinematic = true; // TODO: softlock
            }
        }
        else
        {
            timer = 0;
        }

        if(rb.isKinematic)
        {
            timer = 0;
        }
    }
}
