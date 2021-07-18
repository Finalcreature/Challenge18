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
    public List<Day> daysArr = new List<Day>();
    private string challengePoolJSON;

    public Challenge(string JSON) 
    {
        challengePoolJSON = JSON;
        //Currently not working as there is no full challenge json coming from server
        //challengeTemplate = JasonManager.ExtractData(challengePoolJSON, "template"); 
        //challengeID = JasonManager.ExtractData(challengePoolJSON, "id");
        for(int i = 1; i <=18; i ++)
        {
            daysArr.Add(new Day(i, challengePoolJSON));
        }
    }
}
