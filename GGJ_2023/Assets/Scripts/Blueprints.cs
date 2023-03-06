using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprints : MonoBehaviour
{
    public static Blueprints instance;

    public GameObject dialoguePrefab;

    private void Awake()
    {
        if (instance)
            DestroyImmediate(gameObject);
        else
        {
            instance = this;
        }    
    }
}
