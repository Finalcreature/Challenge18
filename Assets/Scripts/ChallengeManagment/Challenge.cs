using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using BME;
[System.Serializable]
public class Challenge 

{
    public string challengeID;
    public string challengeTemplate;
    public Day[] daysArr = new Day[18];
    private string challengePoolJSON;

    public Challenge(string JSON) 
    {
        challengePoolJSON = JSON;
        //Currently not working as there is no full challenge json coming from server
        //challengeTemplate = JasonManager.ExtractData(challengePoolJSON, "template"); 
        //challengeID = JasonManager.ExtractData(challengePoolJSON, "id");
        int dayIndex = 0;
        foreach (Day day in daysArr)
        {
            daysArr[dayIndex] = FillDay(dayIndex);
            dayIndex++;
        }
    }

    private Day FillDay(int dayIndex)
    {
        string currentDayJSON = JasonManager.ExtractData(challengePoolJSON, "day" + (dayIndex + 1).ToString());
        return new Day(dayIndex + 1, JasonManager.ExtractData(currentDayJSON, "title"), JasonManager.ExtractData(currentDayJSON, "tasks"));
    }
}
