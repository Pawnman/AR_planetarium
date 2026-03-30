using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainSceneManager : MonoBehaviour
{
   public void LoadMainScene()
    {
        SceneManager.LoadScene("StartScene");
    }
   
    public void LoadScannerScene()
    {
        SceneManager.LoadScene("ScannerSceneTest");
    }
}
