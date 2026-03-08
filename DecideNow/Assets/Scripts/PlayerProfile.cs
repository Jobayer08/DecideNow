using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    [Header("Player Behavior Profile")]
    public float speedScore;      // lower = faster decision
    public float riskScore;       // higher = risk taker
    public float successRate;     // 0 to 1 accuracy

    private float totalDecisionTime = 0f;
    private int totalDecisions = 0;
    private int correctDecisions = 0;
    private float totalRisk = 0f;

    // Call this after every player decision
    public void UpdateProfile(float decisionTime, bool isCorrect, int riskLevel)
    {
        totalDecisions++;
        totalDecisionTime += decisionTime;

        // Average decision time (speed)
        speedScore = totalDecisionTime / totalDecisions;

        // Accuracy
        if (isCorrect)
        {
            correctDecisions++;
        }

        successRate = (float)correctDecisions / totalDecisions;

        // Risk calculation
        if (riskLevel == 3)
        {
            totalRisk += 1f;
        }
        else
        {
            totalRisk += 0.3f;
        }

        riskScore = totalRisk / totalDecisions;
    }

    // -------- GETTERS --------

    public float GetSpeed()
    {
        return speedScore;
    }

    public float GetRisk()
    {
        return riskScore;
    }

    public float GetAccuracy()
    {
        return successRate;
    }

    // Optional: Reset profile for new session
    public void ResetProfile()
    {
        totalDecisionTime = 0f;
        totalDecisions = 0;
        correctDecisions = 0;
        totalRisk = 0f;

        speedScore = 0f;
        riskScore = 0f;
        successRate = 0f;
    }

    void Awake()
{
    if (FindObjectsOfType<PlayerProfile>().Length > 1)
    {
        Destroy(gameObject);
        return;
    }

    DontDestroyOnLoad(gameObject);
}
}