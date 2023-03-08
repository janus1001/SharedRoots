using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicIfIdle : MonoBehaviour
{
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

            if(timer > 0.5f)
            {
                rb.isKinematic = true;
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
