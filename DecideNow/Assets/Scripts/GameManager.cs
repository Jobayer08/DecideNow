using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scenarioText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private int score = 0;
    private float timeLeft = 10f;

    [Header("Scenario System")]
public List<Scenario> scenarios;

private Scenario currentScenario;

     [Header("Option Texts")]
public TextMeshProUGUI optionAText;
public TextMeshProUGUI optionBText;
public TextMeshProUGUI optionCText;



    void Start()
    {
        LoadScenario();
        UpdateScore();
    }

    void Update()
    {
        HandleTimer();
    }

    void LoadScenario()
{
    currentScenario = scenarios[Random.Range(0, scenarios.Count)];

    scenarioText.text = currentScenario.scenarioText;

    optionAText.text = currentScenario.optionA;
    optionBText.text = currentScenario.optionB;
    optionCText.text = currentScenario.optionC;
}



    void HandleTimer()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timeLeft);

        if (timeLeft <= 0)
        {
            GameOver();
        }
    }

    public void PlayerDecision(int choice)
{
    if (choice == currentScenario.correctOption)
    {
        score += 10;
    }
    else
    {
        score -= 5;
    }

    UpdateScore();
    ResetTimer();
    LoadScenario();
}


    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void ResetTimer()
    {
        timeLeft = 10f;
    }

    void GameOver()
    {
        scenarioText.text = "Time's up!\nGame Over.";
        Time.timeScale = 0f;
    }
}
