using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    [Header("Player Behavior Profile")]
    public float speedScore;      // lower = faster
    public float riskScore;       // higher = risk taker
    public float successRate;     // 0 to 1

    private float totalDecisionTime;
    private int totalDecisions;
    private int correctDecisions;

    // Call after every decision
    public void UpdateProfile(float decisionTime, bool isCorrect, int riskLevel)
    {
        totalDecisions++;
        totalDecisionTime += decisionTime;

        speedScore = totalDecisionTime / totalDecisions;

        if (isCorrect)
            correctDecisions++;

        successRate = (float)correctDecisions / totalDecisions;

        if (riskLevel == 3)
            riskScore += 1f;
        else
            riskScore += 0.3f;
    }
}
