using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainScene : MonoBehaviour
{
   public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
