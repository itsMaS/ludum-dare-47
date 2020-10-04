using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
    public int scale = 1;
    private void Start()
    {
        Debug.Log(Application.dataPath);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("Screenshot taken");
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/../Screenshots/Screenshot" + PlayerPrefs.GetInt("sc", 0) + ".png", scale);
            PlayerPrefs.SetInt("sc", PlayerPrefs.GetInt("sc", 0)+1);
        }       
    }
}
