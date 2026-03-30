using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainSceneManager : MonoBehaviour
{
   public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
   
    public void LoadScannerScene()
    {
        SceneManager.LoadScene("ScannerScene");
    }
}
