using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scenarioText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI feedbackText;

    [Header("Scenario System")]
    public List<Scenario> scenarios;
    private List<Scenario> remainingScenarios;
    private Scenario currentScenario;

    [Header("Option Texts")]
    public TextMeshProUGUI optionAText;
    public TextMeshProUGUI optionBText;
    public TextMeshProUGUI optionCText;

    [Header("UI Panels")]
    public GameObject optionPanel;

    [Header("Timer Settings")]
    public float maxTime = 19f;
    private float currentTime;
    private bool canChoose = true;

    [Header("Feedback Settings")]
    public float feedbackDuration = 5f;

    [Header("AI & Profile")]
    public AIManager aiManager;
    public PlayerProfile playerProfile;

    // ================= GAME DATA =================
    private float decisionStartTime;
    private int score = 0;

    public int maxScenarios = 10;
    private int playedScenarios = 0;

    public static int finalScore;

    [Header("Animation Controller")]
    public ScenarioAnimatorController scenarioAnimator;

    // ================= UNITY =================

    void Start()
    {
        remainingScenarios = new List<Scenario>(scenarios);
        UpdateScore();
        LoadScenario();
    }

    void Update()
    {
        if (canChoose)
            UpdateTimer();
    }

    // ================= SCENARIO =================

    void LoadScenario()
    {
        if (playedScenarios >= maxScenarios || remainingScenarios.Count == 0)
        {
            finalScore = score;
            SceneManager.LoadScene("ResultScene");
            return;
        }

        canChoose = true;
        optionPanel.SetActive(true);
        feedbackText.text = "";

        int index = Random.Range(0, remainingScenarios.Count);
        currentScenario = remainingScenarios[index];
        remainingScenarios.RemoveAt(index);

        playedScenarios++;

        scenarioText.text = currentScenario.scenarioText;
        optionAText.text = currentScenario.optionA;
        optionBText.text = currentScenario.optionB;
        optionCText.text = currentScenario.optionC;

        ApplyTimeByRisk();
        ResetTimer();
        decisionStartTime = currentTime;

        PlayScenarioAnimation();
    }

    void PlayScenarioAnimation()
    {
        if (scenarioAnimator == null || currentScenario == null)
            return;

        switch (currentScenario.scenarioType)
        {
            case ScenarioType.Fire:
                scenarioAnimator.PlayFire();
                break;

            case ScenarioType.Accident:
                scenarioAnimator.PlayAccident();
                break;

            case ScenarioType.Surgery:
                scenarioAnimator.PlaySurgery();
                break;

            case ScenarioType.CyberAttack:
                scenarioAnimator.PlayCyberAttack();
                break;

            default:
                scenarioAnimator.PlayIdle();
                break;
        }
    }

    void ApplyTimeByRisk()
    {
        switch (currentScenario.riskLevel)
        {
            case 3: maxTime = 15f; break; // Hard
            case 2: maxTime = 17f; break; // Medium
            default: maxTime = 19f; break; // Easy
        }

        ApplyAIDifficulty();
    }

    // ================= TIMER =================

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(currentTime);

        if (currentTime <= 0f)
            TimeUp();
    }

    void ResetTimer()
    {
        currentTime = maxTime;
    }

    void TimeUp()
    {
        if (!canChoose) return;

        canChoose = false;
        optionPanel.SetActive(false);

        score -= 5;
        UpdateScore();

        feedbackText.text = "⏱ Time's Up!\n" + currentScenario.explanation;
        feedbackText.color = Color.yellow;

        StartCoroutine(LoadNextScenario(feedbackDuration));
    }

    // ================= PLAYER DECISION =================

    public void PlayerDecision(int choice)
    {
        if (!canChoose) return;

        canChoose = false;
        optionPanel.SetActive(false);

        float decisionTime = decisionStartTime - currentTime;
        bool isCorrect = choice == currentScenario.correctOption;

        score += isCorrect ? 10 : -5;
        UpdateScore();

        ShowFeedback(isCorrect);

        if (aiManager != null)
            aiManager.RecordDecision(decisionTime, isCorrect, currentScenario.riskLevel);

        if (playerProfile != null)
            playerProfile.UpdateProfile(decisionTime, isCorrect, currentScenario.riskLevel);

        StartCoroutine(LoadNextScenario(feedbackDuration));
    }

    // ================= FEEDBACK =================

    void ShowFeedback(bool success)
    {
        feedbackText.text =
            (success ? "✔ Correct Decision!\n" : "✖ Wrong Decision!\n") +
            currentScenario.explanation;

        feedbackText.color = success ? Color.green : Color.red;
    }

    // ================= AI =================

    void ApplyAIDifficulty()
    {
        if (aiManager == null) return;

        float multiplier = aiManager.GetDifficultyMultiplier();
        maxTime = Mathf.Clamp(maxTime / multiplier, 8f, 25f);
    }

    // ================= SCORE =================

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        scoreText.color = score >= 0 ? Color.white : Color.red;
    }

    // ================= COROUTINE =================

    IEnumerator LoadNextScenario(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScenario();
    }
}
