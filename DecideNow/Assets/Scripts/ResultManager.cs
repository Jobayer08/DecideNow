using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI riskText;
    public TextMeshProUGUI accuracyText;

    public PlayerProfile playerProfile;

    void Start()
    {
        ShowResults();
    }

    void ShowResults()
    {
        finalScoreText.text = "Final Score: " + GameManager.finalScore;

        if (playerProfile != null)
        {
            speedText.text = "Speed: " + playerProfile.GetSpeed();
            riskText.text = "Risk: " + playerProfile.GetRisk();
            accuracyText.text = "Accuracy: " + playerProfile.GetAccuracy();
        }
    }
}