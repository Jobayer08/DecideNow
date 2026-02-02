using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        finalScoreText.text = "Final Score: " + GameManager.finalScore;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
