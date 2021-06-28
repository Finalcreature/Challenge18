using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ChallengeEngine : MonoBehaviour
{
    [SerializeField] Day[] daysArr;
    string challengeOptionsJSON;
    int dayIndex;
    // Start is called before the first frame update
    void Start()
    {
        daysArr = new Day[18];
        dayIndex = 0;
        challengeOptionsJSON = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/Challenge_Options.json");
        foreach (Day day in daysArr)
        {
            daysArr[dayIndex] = FillDay();
            dayIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Day FillDay()
    {
        string currentDayJSON = JasonManager.ExtractData(challengeOptionsJSON, "day" + (dayIndex + 1).ToString());
        return new Day(dayIndex, JasonManager.ExtractData(currentDayJSON, "title"), JasonManager.ExtractData(currentDayJSON, "tasks"));
    }
}
