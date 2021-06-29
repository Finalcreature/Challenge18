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

     public Task(int taskLevel, string taskDescription, string taskTheme)
    {
        this.taskLevel = taskLevel;
        this.taskDescription = taskDescription;
        this.isTaskCompleted = false;
        this.taskTitle = taskTheme;
    }
}
