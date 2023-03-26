using UnityEngine;

public class PulseMeshRenderer : MonoBehaviour
{
    // Public variables for controlling the pulse
    public Color baseColor = Color.white;   // The starting color of the mesh
    public Color pulseColor = Color.red;    // The color to pulse to
    public float pulseSpeed = 1.0f;         // The speed at which to pulse (in Hz)

    // Private variables for internal use
    private MeshRenderer meshRenderer;      // The MeshRenderer component attached to this GameObject
    private float timeOffset;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        timeOffset = Random.Range(0.0f, 1.0f); // Add some randomness to the pulsing
    }

    private void Update()
    {
        // Calculate the pulse amount based on the current time and pulse speed
        float pulseAmount = Mathf.Sin((Time.time + timeOffset) * pulseSpeed * Mathf.PI * 2.0f) * 0.5f + 0.5f;

        // Lerp the color between the base color and pulse color based on the pulse amount
        Color lerpedColor = Color.Lerp(baseColor, pulseColor, pulseAmount);

        // Set the mesh's material emission color to the lerped color
        meshRenderer.material.SetColor("_EmissionColor", lerpedColor);

        // Make sure the material is set to use emission
        meshRenderer.material.EnableKeyword("_EMISSION");
    }
}