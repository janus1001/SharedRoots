using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target;  // The object to rotate around
    public float distance = 25f;  // The distance from the target
    public float angle = 0f;  // The starting angle

    private void Update()
    {
        // Calculate the position based on the angle and distance
        float x = target.position.x + distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        float z = target.position.z + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        transform.position = new Vector3(x, transform.position.y, z);

        // Look at the target
        transform.LookAt(target);

        // Increase the angle
        angle += Time.deltaTime * 5f;  // 5 is a rotation speed factor
    }
}
