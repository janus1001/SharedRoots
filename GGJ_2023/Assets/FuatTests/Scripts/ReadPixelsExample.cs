using UnityEngine;
using System.IO;
using System;

public class ReadPixelsExample : MonoBehaviour {
    private string userPath;

    //8192, 4320
    //4096, 2160
    //2560 , 1440
    int screenshot_width = 8192;
    int screenshot_height = 4320;

    void Start() {
        //userPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        userPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
        userPath += "\\" + "gamescreen_" + DateTime.Now.ToString("DDMMYY-HHmmssfff") + ".png";
    }

    void Update() {
        // When the user presses the space key, perform the screen grab operation
        if (Input.GetKeyDown(KeyCode.U)) {
            takeScreenshot(1920, 1080, userPath);
        }

        else if (Input.GetKeyDown(KeyCode.I)) {
            takeScreenshot(2560, 1440, userPath);
        }

        else if (Input.GetKeyDown(KeyCode.O)) {
            takeScreenshot(4096, 2160, userPath);
        }

        else if (Input.GetKeyDown(KeyCode.P)) {
            takeScreenshot(8192, 4320, userPath);
        }
    }

    void takeScreenshot(int width, int height, string pathToSave) {
        Debug.Log("taking screenshot: " + width.ToString() + " x " + height.ToString());
        RenderTexture targetTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        Camera.main.targetTexture = targetTexture;
        Camera.main.Render();
        RenderTexture.active = targetTexture;
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();
        byte[] data = texture.EncodeToPNG();
        File.WriteAllBytes(pathToSave, data);
    }
}