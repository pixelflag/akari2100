using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    string folder = "Screenshot";

    void Start ()
    {
        Time.captureFramerate = 25;
        System.IO.Directory.CreateDirectory(folder);
    }

    void Update()
    {
        string fileName = string.Format("{0}/{1:D04} shot.png", folder, Time.frameCount);
        ScreenCapture.CaptureScreenshot(fileName);
    }
}