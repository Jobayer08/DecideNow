using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scenarioText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("Scenario System")]
    public List<Scenario> scenarios;
    private Scenario currentScenario;

    [Header("Option Texts")]
    public TextMeshProUGUI optionAText;
    public TextMeshProUGUI optionBText;
    public TextMeshProUGUI optionCText;

    [Header("Timer Settings")]
    public float maxTime = 10f;
    private float currentTime;

    [Header("AI")]
    public AIManager aiManager;
    private float decisionStartTime;

    private int score = 0;

[Header("Player Profile")]
public PlayerProfile playerProfile;

[Header("Feedback UI")]
public TextMeshProUGUI feedbackText;



    void Start()
    {
        LoadScenario();
        UpdateScore();
    }

    void Update()
    {
        UpdateTimer();
    }

    void LoadScenario()
    {
         feedbackText.text = "";
        currentScenario = scenarios[Random.Range(0, scenarios.Count)];

        scenarioText.text = currentScenario.scenarioText;
        optionAText.text = currentScenario.optionA;
        optionBText.text = currentScenario.optionB;
        optionCText.text = currentScenario.optionC;

        // Base time from risk
        if (currentScenario.riskLevel == 3)
            maxTime = 6f;
        else if (currentScenario.riskLevel == 2)
            maxTime = 8f;
        else
            maxTime = 10f;

        // AI adjustment
        ApplyAIDifficulty();

        ResetTimer();
        decisionStartTime = currentTime;
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(currentTime);

        if (currentTime <= 0f)
            TimeUp();
    }

    void TimeUp()
    {
        score -= 5;
        UpdateScore();
        LoadScenario();
    }

    void ResetTimer()
    {
        currentTime = maxTime;
    }

    public void PlayerDecision(int choice)
{
    if (currentTime <= 0f)
        return;

    float decisionTime = decisionStartTime - currentTime;
    bool isCorrect = choice == currentScenario.correctOption;

    if (isCorrect)
    {
        score += 10;
        ShowFeedback(true);
    }
    else
    {
        score -= 5;
        ShowFeedback(false);
    }

    aiManager.RecordDecision(
        decisionTime,
        isCorrect,
        currentScenario.riskLevel
    );

    playerProfile.UpdateProfile(
        decisionTime,
        isCorrect,
        currentScenario.riskLevel
    );

    UpdateScore();
    Invoke(nameof(LoadScenario), 1.5f);
}


void ShowFeedback(bool success)
{
    if (success)
    {
        feedbackText.text = "✔ Correct Decision!\n" + currentScenario.explanation;
        feedbackText.color = Color.green;
    }
    else
    {
        feedbackText.text = "✖ Wrong Decision!\n" + currentScenario.explanation;
        feedbackText.color = Color.red;
    }

    CancelInvoke();
}

    void ApplyAIDifficulty()
    {
        float multiplier = aiManager.GetDifficultyMultiplier();
        maxTime = Mathf.Clamp(maxTime / multiplier, 4f, 12f);
    }

    void UpdateScore()
{
    scoreText.text = "Score: " + score;

    if (score >= 0)
        scoreText.color = Color.white;
    else
        scoreText.color = Color.red;
}

}
