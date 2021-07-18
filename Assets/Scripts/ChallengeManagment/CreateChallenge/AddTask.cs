using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BME;

public class AddTask : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject addRow; //Add row Prefab
    GameObject tasksField; //Field of all Toggles
    CreateChallenge createChallengeScript;//Main script of Scene

    public void Start()
    {
        tasksField = GameObject.Find("TaskToggle");
        createChallengeScript = GameObject.Find("Canvas").GetComponent<CreateChallenge>();
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        createChallengeScript.totalTasks++;
        AddNewTask(createChallengeScript.totalTasks);
        List<string> newTaskOptions = new List<string>();
        newTaskOptions.Add("Add a new Task");
        int currentDay = createChallengeScript.SplitStringInt(Visuals.selectedToggle.name);
        Day day = createChallengeScript.challengeOptions.daysArr[currentDay - 1];
        day.tasks.Add(new Task(createChallengeScript.totalTasks, day.dayTitle, false, newTaskOptions));
    }
    /// <summary>
    /// Creates a new task button
    /// </summary>
    /// <param name="taskNum"></param>
    public void AddNewTask(int taskNum)
    {
        Transform lastRow = tasksField.transform.GetChild(tasksField.transform.childCount - 1);
        GameObject newTaskButton;
        if (lastRow.childCount < 5)
        {
            newTaskButton = Instantiate(gameObject, lastRow);
        }
        else
        {
            GameObject newRow = AddNewRow();
            newTaskButton = Instantiate(gameObject, newRow.transform);
        }
        newTaskButton.name = "Task " + taskNum;
        newTaskButton.GetComponentInChildren<Text>().text = "Task " + taskNum;
        newTaskButton.GetComponent<AddTask>().enabled = false;
        newTaskButton.AddComponent<TasksManager>();
    }
    /// <summary>
    /// adds a new row if the last row has more then 5 elements
    /// </summary>
    /// <returns></returns>
    public GameObject AddNewRow()
    {
        GameObject newRow = Instantiate(addRow, tasksField.transform);
        newRow.name = "Row" + (tasksField.transform.childCount - 1);
        newRow.GetComponent<HorizontalLayoutGroup>().padding.left = 0;
        Destroy(newRow.transform.Find("Add").gameObject);
        return newRow;
    }
}
