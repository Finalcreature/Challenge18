using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class Challenge 

{
    public string challengeID;
    public Day[] daysArr = new Day[18];
    private string challengePoolJSON;

    public Challenge(string JSON, bool isNewChallenge) 
    {
        challengePoolJSON = JSON;
        if (isNewChallenge) //new challenge
        {
            //create/get challenge id from server
            int dayIndex = 0;
            foreach (Day day in daysArr)
            {
                daysArr[dayIndex] = FillDay(dayIndex);
                dayIndex++;
            }
        }
        else
        {
            //Get an Active Challenge from server 
        }
    }

    private Day FillDay(int dayIndex)
    {
        string currentDayJSON = JasonManager.ExtractData(challengePoolJSON, "day" + (dayIndex + 1).ToString());
        return new Day(dayIndex, JasonManager.ExtractData(currentDayJSON, "title"), JasonManager.ExtractData(currentDayJSON, "tasks"));
    }
}
