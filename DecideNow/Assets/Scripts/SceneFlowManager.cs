using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
