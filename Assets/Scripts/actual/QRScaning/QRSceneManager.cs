using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TBEasyWebCam;

public class QRSceneManager : MonoBehaviour
{
    [Header("Plugin setup")]
    public QRCodeDecodeController qrController;

    [Serializable]
    public class SceneMapping
    {
        public string qrID;        // Key (ID)
        public string sceneName;   // Value (Scene name)
    }

    [Header("Scene Dictionary")]
    public List<SceneMapping> sceneDictionary = new List<SceneMapping>();

    //public QRDecodeTest qrController;

    public void OnQRScanFinished(string scannedData)
    {
        // Searching for Scene in Dictionary by Key (ID)
        SceneMapping match = sceneDictionary.Find(m => m.qrID == scannedData);

        if (match != null)
        {
            // Stop camera and then go to the new scene
            if (qrController != null)
            {
                qrController.StopWork();
            }

            // Go to the detected scene by ID
            SceneManager.LoadScene(match.sceneName);
        }
    }
}