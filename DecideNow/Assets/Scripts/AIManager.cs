using UnityEngine;

public class AIManager : MonoBehaviour
{
    [Header("Player Behavior Stats")]
    public float averageDecisionTime;
    public int totalDecisions;
    public int correctDecisions;

    public int highRiskChoices;
    public int lowRiskChoices;

    private float totalDecisionTime;

    // Call when player makes a decision
    public void RecordDecision(float decisionTime, bool isCorrect, int riskLevel)
    {
        totalDecisions++;
        totalDecisionTime += decisionTime;
        averageDecisionTime = totalDecisionTime / totalDecisions;

        if (isCorrect)
            correctDecisions++;

        if (riskLevel == 3)
            highRiskChoices++;
        else
            lowRiskChoices++;
    }

    // Difficulty logic
    public float GetDifficultyMultiplier()
    {
        float successRate = (totalDecisions == 0) ? 0 :
            (float)correctDecisions / totalDecisions;

        if (successRate > 0.7f && averageDecisionTime < 4f)
            return 1.3f; // harder

        if (successRate < 0.4f)
            return 0.8f; // easier

        return 1f; // normal
    }
}
