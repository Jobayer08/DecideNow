using UnityEngine;

[System.Serializable]
public class Scenario
{
    [Header("Scenario")]
    [TextArea(3, 5)]
    public string scenarioText;

    [Header("Options")]
    public string optionA;
    public string optionB;
    public string optionC;

    [Header("Answer & Difficulty")]
    [Range(1, 3)]
    public int correctOption;

    [Range(1, 3)]
    public int riskLevel;

    [Header("Explanation (Shown after decision)")]
    [TextArea(2, 4)]
    public string explanation;

    public string animationTrigger;

    

public ScenarioType scenarioType;


}
