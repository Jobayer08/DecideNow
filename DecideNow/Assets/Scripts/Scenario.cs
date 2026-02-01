[System.Serializable]
public class Scenario
{
    public string scenarioText;

    public string optionA;
    public string optionB;
    public string optionC;

    public int correctOption;   // 1 = A, 2 = B, 3 = C

    public int riskLevel;       // 1 = Low, 2 = Medium, 3 = High
}
