using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QRSceneManager : MonoBehaviour
{
    [Header("Plugin setup")]
    public QRCodeDecodeController qrController;

    [Header("Alarm Window")]
    public GameObject alarmWindow;

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
            // Does the SceneName exist in Inspector
            if (string.IsNullOrEmpty(match.sceneName))
            {
                Debug.LogWarning($"ID {scannedData} doesn't have value SceneName in Dictionary. Check SceneManager!");
                OpenAlarm();
                return;
            }

            // Does the Scene exist in Build Settings
            if (SceneUtility.GetBuildIndexByScenePath(match.sceneName) == -1)
            {
                Debug.LogWarning($"Scene with ID {scannedData} doesn't exist in Build Settings!");
                OpenAlarm();
                return;
            }

            // Stop camera and then go to the new scene
            if (qrController != null)
            {
                qrController.StopWork();
            }

            // Go to the detected scene by ID
            SceneManager.LoadScene(match.sceneName);
        }
        else
        {
            Debug.LogWarning("Unknown ID. Check SceneManager!");
            OpenAlarm();
            return;
        }
    }

    // Does exist something better? To think about it
    private void ResetScanner()
    {
        if (qrController != null)
        {
            qrController.StopWork();
            qrController.StartWork();
        }
    }

    // Open alarm window
    private void OpenAlarm()
    {
        if (alarmWindow != null)
        {
            alarmWindow.SetActive(true);
        }
    }

    // Action for button in Alarm window - close window and reset QRScanner
    public void CloseAlarmAndReset()
    {
        if (alarmWindow != null)
        {
            alarmWindow.SetActive(false);
        }

        ResetScanner();
    }
}