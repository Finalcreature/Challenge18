using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BME;
using System.IO;
using TMPro;
using System;

public class CreateChallenge : MonoBehaviour
{
    GameObject darkMode; //Dark Panel for edit day title
    [SerializeField] GameObject changeDayTitleInput; 
    TMP_Text dayTitle;
    string currentToggleText; //Current day text
    public int totalDays;
    string ChallengeOptionsJSON;
    public Dictionary<Toggle, string> dayTitles; //Days + Titles
    public Toggle currentToggle; //current toggle selected
    GameObject daysField; // Game Object Contains all toggles
    // Start is called before the first frame update
    void Start()
    {
        daysField = GameObject.Find("DaysToggle");
        currentToggle = Visuals.selectedToggle.GetComponent<Toggle>();
        currentToggleText = currentToggle.GetComponentInChildren<Text>().text;
        ChallengeOptionsJSON = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/Challenge_Options.json");
        dayTitles = new Dictionary<Toggle, string>();
        FillDictionary(dayTitles);
        totalDays = 18;
        darkMode = GameObject.Find("DarkTheme");
        if (darkMode != null) darkMode.SetActive(false);
        dayTitle = GameObject.Find("Day Title").GetComponent<TMP_Text>();
        StartCoroutine(SetUpDayTitle());
    }
    /// <summary>
    /// Fills up Dictionary with Toggles and titles
    /// </summary>
    /// <param name="temp"></param>
    private void FillDictionary(Dictionary<Toggle, string> temp)
    {
        Toggle[] toggles = FindObjectsOfType<Toggle>();
        for (int i = 1; i < toggles.Length; i++)
        {
            foreach (Toggle t in toggles)
            {
                if (SplitStringInt(t.GetComponentInChildren<Text>().text) == i)
                {
                    string currentIndex = t.GetComponentInChildren<Text>().text;
                    currentIndex = currentIndex.Replace(" ", "");
                    temp.Add(t, JasonManager.ExtractData(JasonManager.ExtractData(ChallengeOptionsJSON, currentIndex.ToLower()), "title"));
                }
            }
        }
        temp.Add(GameObject.Find("Add").GetComponent<Toggle>(), "Empty");
    }
    /// <summary>
    /// Sets up the correct title of the selected toggle
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetUpDayTitle()
    {
        yield return new WaitUntil(() => Visuals.selectedToggle != null);
        currentToggle = Visuals.selectedToggle.GetComponent<Toggle>();
        dayTitle.text = "";
        currentToggleText = currentToggle.GetComponentInChildren<Text>().text;
        dayTitle.text = currentToggleText + " - " + dayTitles[currentToggle];

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Activates Edit Day Title Panel
    /// </summary>
    public void EditDayTitle()
    {
        darkMode.SetActive(true);
        changeDayTitleInput.SetActive(true);
    }
    /// <summary>
    /// Saves the user input of a new title
    /// </summary>
    public void SaveNewTitle()
    {
        string newTitle = changeDayTitleInput.transform.GetChild(2).gameObject.GetComponent<Text>().text;
        if (!newTitle.Equals(""))
        {
            dayTitles[Visuals.selectedToggle.GetComponent<Toggle>()] = " - " + newTitle;
            StartCoroutine(SetUpDayTitle());
        }
    }
    /// <summary>
    /// Delets the current toggle selected
    /// </summary>
    public void DeleteDay()
    {
        currentToggle = Visuals.selectedToggle.GetComponent<Toggle>();
        int destroyedIndex = SplitStringInt(currentToggle.name);
        dayTitles.Remove(currentToggle);
        GameObject destroyedObjRow = currentToggle.transform.parent.gameObject;
        Destroy(currentToggle.gameObject);
        Visuals.selectedToggle = null;
        currentToggle = null;
        if (destroyedIndex == dayTitles.Count) //If the toggle is the last toggle in the list
        {
            currentToggle = GameObject.Find("Day " + (destroyedIndex - 1).ToString()).GetComponent<Toggle>();
        }
        else // else, changes all the next toggles text and name to the correct order
        {
            currentToggle = GameObject.Find("Day " + (destroyedIndex + 1).ToString()).GetComponent<Toggle>();
            foreach (Toggle t in FindObjectsOfType<Toggle>()) //runs on all toggles in scene
            {
                int dayIndex = SplitStringInt(t.name);
                if (dayIndex > destroyedIndex) 
                {
                    t.GetComponentInChildren<Text>().text = "Day " + (dayIndex - 1).ToString();
                    t.transform.name = "Day " + (dayIndex - 1).ToString();
                }
            }
        }
        currentToggle.isOn = true;
        totalDays--;
        Visuals.SelectToggle(currentToggle); // colors the correct toggle
        StartCoroutine(SetUpDayTitle()); // changes the title
        FixRows(destroyedObjRow); // fix the rows
    }
    /// <summary>
    /// Removes all letters from a string leaving only numbers
    /// </summary>
    /// <param name="str">string</param>
    /// <returns>Only the numbers from the string attached</returns>
    public int SplitStringInt(string str)
    {
        Char[] chars = str.ToCharArray();
        int index = 0;
        foreach (char c in chars)
        {
            if (Char.IsLetter(c))
                str = str.Replace(c.ToString(), "");
            else if (c == ' ')
                str = str.Replace(c.ToString(), "");
            index++;
        }
        if (str.Length == 0)
            return -1;
        return int.Parse(str);
    }
    /// <summary>
    /// Moves Toggles between rows when a specific toggle is deleted
    /// </summary>
    /// <param name="row">Row of the Current Toggle selected</param>
    public void FixRows(GameObject row)
    {
        int rowNum = SplitStringInt(row.name);
        if (rowNum == daysField.transform.childCount - 1)
            return;
        for (int i = rowNum + 1; i < daysField.transform.childCount; i++)
        {
            GameObject nextRow = GameObject.Find("Row" + i);
            nextRow.transform.GetChild(0).transform.SetParent(GameObject.Find("Row" + (i - 1)).transform);
            if (nextRow.transform.childCount == 0)
                Destroy(nextRow);
        }
    }
}
