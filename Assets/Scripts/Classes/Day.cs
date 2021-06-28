using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        int taskIndex = 1;
        string singleTaskJSON;
        foreach(Task task in tasks)
        {
            singleTaskJSON = JasonManager.ExtractData(tasksJSON, "task" + taskIndex.ToString());
            Debug.Log(singleTaskJSON);
            tasks[taskIndex - 1] = new Task(taskIndex, JasonManager.ExtractData(singleTaskJSON, "options"), dayTitle);
            taskIndex++;
        }
    }
}
