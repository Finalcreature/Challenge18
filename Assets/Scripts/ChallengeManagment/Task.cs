using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Task 
{
    public int taskLevel;
    public string taskDescription;
    public string taskTitle;
    public bool isTaskCompleted;

     public Task(int taskLevel, string taskDescription, string taskTheme, bool isTaskCompleted)
    {
        this.taskLevel = taskLevel;
        this.taskDescription = taskDescription;
        this.isTaskCompleted = isTaskCompleted;
        this.taskTitle = taskTheme;
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
