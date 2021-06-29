using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Day 
{
    public int dayNum;
    public string dayTitle;
    public Task[] tasks = new Task[3];
    public Day(int dayNum, string dayTitle, string tasksJSON)
    {
        this.dayNum = dayNum;
        this.dayTitle = dayTitle;
        FillTasks(dayTitle, tasksJSON);
    }
    /// <summary>
    /// Fills up 3 tasks in a specific day from the task pool
    /// </summary>
    /// <param name="dayTitle">Title of the specific day</param>
    /// <param name="tasksJSON">string of all the tasks from a specific day</param>
    private void FillTasks(string dayTitle, string tasksJSON)
    {
        int taskIndex = 1;
        foreach (Task task in tasks) 
        {
            string taskOptionsJSON;
            bool isTaskCompleted = false;
            //Currently not working as there is no full challenge json coming from server
            //if (JasonManager.ExtractData(tasksJSON, "iscompleted").Equals("true"))
            //    isTaskCompleted = true;
            List<string> taskOptions = new List<string>();
            taskOptionsJSON = JasonManager.ExtractData(tasksJSON, "task" + taskIndex.ToString());
            taskOptionsJSON = JasonManager.ExtractData(taskOptionsJSON, "options");
            taskOptions = SplitTaskOptions(taskOptionsJSON);
            if (taskOptions.Count >= 1)
            {
                string randomTask = taskOptions[UnityEngine.Random.Range(0, taskOptions.Count - 1)]; // randomly selects a task from the list
                tasks[taskIndex - 1] = new Task(taskIndex, randomTask, dayTitle, isTaskCompleted);
            }
            else
                tasks[taskIndex - 1] = new Task(taskIndex, "EMPTY", dayTitle, true); // if no tasks in the pool, fills up "EMPTY"
            taskIndex++;
        }
    }

    /// <summary>
    /// Splits the string attached into a list of strings
    /// </summary>
    /// <param name="JSON">list in a string form </param>
    /// <returns>the string in a list form</returns>
    private List<string> SplitTaskOptions(string JSON)
    {
        List<string> temp = new List<string>();
        char[] JSONArr = JSON.ToCharArray();
        int index = 0;
        int startIndex = 0;
        int count = 0;
        foreach (char c in JSONArr)
        {
            if(c.Equals('"'))
            {
                if(count == 0)
                {
                    startIndex = index;
                    count++;
                }
                else
                {
                    temp.Add(JSON.Substring(startIndex, index - startIndex)); // add each string divided as a cell in the list
                    count = 0;
                }
            }
            index++;
        }
        return temp;
    }
    public Day GetNextDay(Challenge activeChallenge, Day activeDay)
    {
        try
        {
            return activeChallenge.daysArr[activeDay.dayNum];
        }
        catch
        {
            return null;
        }
    }
}
