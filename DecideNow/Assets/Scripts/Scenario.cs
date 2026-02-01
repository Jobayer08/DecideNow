[System.Serializable]
public class Scenario
{
    public string scenarioText;

    public string optionA;
    public string optionB;
    public string optionC;

    public int correctOption;
    public int riskLevel;

    [TextArea]
    public string explanation;
}
