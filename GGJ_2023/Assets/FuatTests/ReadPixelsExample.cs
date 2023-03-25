using UnityEngine;
using System.IO;
public class ReadPixelsExample : MonoBehaviour {
    // Set this reference to a GameObject that has a Renderer component,
    // and a material that displays a texure (such as the Default material).
    // A standard Cube or other primitive works for the purposes of this example.
    public Renderer screenGrabRenderer;

    private Texture2D destinationTexture;
    private bool isPerformingScreenGrab = false;

    //8192, 4320
    //4096, 2160
    //2560 , 1440
    int screenshot_width = 8192;
    int screenshot_height = 4320;

    void Start() {
    }

    void Update() {
        // When the user presses the space key, perform the screen grab operation
        if (Input.GetKeyDown(KeyCode.P)) {
            Debug.Log("taking screenshot");
            // isPerformingScreenGrab = true;
            RenderTexture targetTexture = new RenderTexture(screenshot_width, screenshot_height, 24, RenderTextureFormat.ARGB32);
            Texture2D texture = new Texture2D(screenshot_width, screenshot_height, TextureFormat.RGB24, false);
            Camera.main.targetTexture = targetTexture;
            Camera.main.Render();
            RenderTexture.active = targetTexture;
            texture.ReadPixels(new Rect(0, 0, screenshot_width, screenshot_height), 0, 0);
            texture.Apply();
            byte[] data = texture.EncodeToPNG();
            File.WriteAllBytes("D:\\texture2d.png", data);
        }
    }
}