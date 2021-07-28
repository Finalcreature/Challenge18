using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTask : MonoBehaviour
{
    [SerializeField] GameObject taskOption;
    [SerializeField] GameObject addNewOption;

    CreateChallenge createChallenge;

    GameObject taskTitle;
    GameObject applyButton;
    GameObject content;
    GameObject addOption;
    List<GameObject> taskOptions;

    Task currentTask;
    // Start is called before the first frame update
    void Start()
    {
        taskTitle = transform.Find("Task Number").gameObject;
        content = transform.Find("ScrollTaskPanel/ViewPort/Content").gameObject;
        applyButton = content.transform.Find("Button").gameObject;
        addOption = content.transform.Find("AddOption").gameObject;
        createChallenge = GameObject.Find("Canvas").GetComponent<CreateChallenge>();
        taskOptions = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ApplyTask()
    {
        ClearTaskOptions();
        createChallenge.taskPanel.SetActive(false);
    }
    public void ManageTask(int taskNum)
    {
        createChallenge.taskPanel.SetActive(true);
        taskTitle.GetComponent<Text>().text = "Task " + taskNum;
        currentTask = createChallenge.challengeOptions.daysArr[createChallenge.currentDayIndex - 1].tasks[taskNum - 1];
        foreach(string option in currentTask.taskOptions)
        {
            if (option.Equals("Add a new Task"))
                break;
            GameObject temp = Instantiate(taskOption, content.transform);
            taskOptions.Add(temp);
            temp.GetComponentInChildren<Text>().text = option;
            temp.GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();
        }
        if(addOption == null) addOption = content.transform.Find("AddOption(Clone)").gameObject;
        addOption.transform.SetAsLastSibling();
        applyButton.transform.SetAsLastSibling();
    }
    public void ClearTaskOptions()
    {
        foreach(GameObject option in taskOptions)
        {
            Destroy(option);
        }    
    }
    public void AddNewInputField(GameObject editedField)
    {
        taskOptions.Add(editedField);
        GameObject temp = Instantiate(addNewOption, content.transform);
        temp.transform.Find("InputField").GetComponent<InputField>().onEndEdit.AddListener(delegate { AddNewInputField(temp); });
        temp.GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();
        applyButton.transform.SetAsLastSibling();
    }
}
