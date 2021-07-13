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
    int currentDayIndex;
    public int totalDays;
    public int totalTasks;
    string ChallengeOptionsJSON;
    //public Dictionary<Toggle, string> dayTitles; //Days + Titles
    public Toggle currentToggle; //current toggle selected
    GameObject daysField; // Game Object Contains all toggles
    public Challenge challengeOptions;// the complete challenge edited by the user
    [SerializeField] GameObject TaskToggle;//Task Buttons Field
    bool isTasksCleared;//bool for waiting tasks to clear before setting new ones

    // Start is called before the first frame update
    void Start()
    {
        daysField = GameObject.Find("DaysToggle");
        currentToggle = Visuals.selectedToggle.GetComponent<Toggle>();
        currentToggleText = currentToggle.GetComponentInChildren<Text>().text;
        currentDayIndex = SplitStringInt(currentToggleText);
        ChallengeOptionsJSON = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/Challenge_Options.json");
        challengeOptions = new Challenge(ChallengeOptionsJSON);
        //dayTitles = new Dictionary<Toggle, string>();
        //FillDictionary(dayTitles);
        totalDays = 18;
        darkMode = GameObject.Find("DarkTheme");
        if (darkMode != null) darkMode.SetActive(false);
        dayTitle = GameObject.Find("Day Title").GetComponent<TMP_Text>();
        StartCoroutine(SetUpDayTitle());
    }
    ///// <summary>
    ///// Fills up Dictionary with Toggles and titles
    ///// </summary>
    ///// <param name="temp"></param>
    //private void FillDictionary(Dictionary<Toggle, string> temp)
    //{
    //    Toggle[] toggles = FindObjectsOfType<Toggle>();
    //    for (int i = 1; i < toggles.Length; i++)
    //    {
    //        foreach (Toggle t in toggles)
    //        {
    //            if (SplitStringInt(t.GetComponentInChildren<Text>().text) == i)
    //            {
    //                string currentIndex = t.GetComponentInChildren<Text>().text;
    //                currentIndex = currentIndex.Replace(" ", "");
    //                temp.Add(t, JasonManager.ExtractData(JasonManager.ExtractData(ChallengeOptionsJSON, currentIndex.ToLower()), "title"));
    //            }
    //        }
    //    }
    //    temp.Add(GameObject.Find("Add").GetComponent<Toggle>(), "(Edit Day Title)");
    //}
    /// <summary>
    /// Sets up the correct title of the selected toggle
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetUpDayTitle()
    {
        yield return new WaitUntil(() => Visuals.selectedToggle != null);
        dayTitle.text = "";
        currentToggle = Visuals.selectedToggle.GetComponent<Toggle>();
        currentToggleText = currentToggle.GetComponentInChildren<Text>().text;
        currentDayIndex = SplitStringInt(currentToggleText);
        dayTitle.text = currentToggleText + " - " + challengeOptions.daysArr[currentDayIndex - 1].dayTitle;
        SetUpTasks(challengeOptions.daysArr[currentDayIndex - 1]);
    }

    /// <summary>
    /// Activates Edit Day Title Panel
    /// </summary>
    public void EditDayTitle()
    {
        darkMode.SetActive(true);
        changeDayTitleInput.SetActive(true);
        if (challengeOptions.daysArr[currentDayIndex - 1].dayTitle.Equals("(Edit Day Title)"))
            changeDayTitleInput.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "Enter Title Here";
        else
            changeDayTitleInput.GetComponent<InputField>().placeholder.GetComponent<Text>().text = challengeOptions.daysArr[currentDayIndex - 1].dayTitle;

    }
    /// <summary>
    /// Saves the user input of a new title
    /// </summary>
    public void SaveNewTitle()
    {
        string newTitle = changeDayTitleInput.transform.GetChild(2).gameObject.GetComponent<Text>().text;
        changeDayTitleInput.GetComponent<InputField>().text = "";
        if (!newTitle.Equals(""))
        {
            challengeOptions.daysArr[currentDayIndex - 1].dayTitle = newTitle;
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
        challengeOptions.daysArr.RemoveAt(destroyedIndex - 1);
        //dayTitles.Remove(currentToggle);
        GameObject destroyedObjRow = currentToggle.transform.parent.gameObject;
        Destroy(currentToggle.gameObject);
        Visuals.selectedToggle = null;
        currentToggle = null;
        if (destroyedIndex == challengeOptions.daysArr.Count + 1) //If the toggle is the last toggle in the list
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
    private void FixRows(GameObject row)
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

    private void SetUpTasks(Day day)
    {
        isTasksCleared = false;
        ClearTasks();
        StartCoroutine(SetNewTasks(day));
    }
    /// <summary>
    /// Clear existing task buttons and rows when switching between different days
    /// </summary>
    private void ClearTasks()
    {
        for (int i = 2; i < TaskToggle.transform.childCount; i++)
        {
            Destroy(TaskToggle.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < TaskToggle.transform.GetChild(1).childCount; i++)
        {
            Destroy(TaskToggle.transform.GetChild(1).GetChild(i).gameObject);
        }
        isTasksCleared = true;
    }
    /// <summary>
    /// Sets up new buttons for tasks after swiching to a different day toggle
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    private IEnumerator SetNewTasks(Day day)
    {
        yield return new WaitUntil(() => isTasksCleared);
        totalTasks = 0;
        for (int i = 0; i < day.tasks.Count; i++)
        {
            totalTasks++;
            TaskToggle.GetComponentInChildren<AddTask>().AddNewTask(totalTasks);
        }
    }
}
