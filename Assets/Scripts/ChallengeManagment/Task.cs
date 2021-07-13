using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Task 
{
    public int taskLevel;
    public List <string> taskOptions;
    public string taskTitle;
    public bool isTaskCompleted;

     public Task(int taskLevel, string taskTheme, bool isTaskCompleted, List<string> taskOptions)
    {
        this.taskLevel = taskLevel;
        this.isTaskCompleted = isTaskCompleted;
        this.taskTitle = taskTheme;
        this.taskOptions = taskOptions;
    }
    public Task GetNextTask(Day activeDay, Task activeTask)
    {
        try
        {
            return activeDay.tasks[activeTask.taskLevel];
        }
        catch
        {
            return null; 
        }
    }
}
