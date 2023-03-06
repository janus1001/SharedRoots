using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerReceiver : MonoBehaviour
{
    Renderer Renderer;
    Material ogMaterial;

    public Material shiningMaterial;

    private void Start()
    {
        Renderer = GetComponent<Renderer>();
        ogMaterial = Renderer.material;
    }

    private void Update()
    {
        
    }
    
    public void Shining()
    {
        
    }
}
