using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class ChallengeEngine : MonoBehaviour
{
    Challenge challenge;
    string challengeJSON;
    [SerializeField] Task activeTask;
    [SerializeField] Day activeDay;
    string taskUrl;
    float challengeFillValue;
    float dayFillValue;

    [SerializeField] Button linkButton;
    [SerializeField] Text challengeName;
    [SerializeField] Text dayNum;
    [SerializeField] Text dayTheme;
    [SerializeField] Text taskLevel;
    [SerializeField] Text taskDescription;
    [SerializeField] Slider challengeProgressBar;
    [SerializeField] Slider dayProgressBar;
    // Start is called before the first frame update
    void Start()
    {
        taskUrl = "";
        linkButton.gameObject.SetActive(false);
        activeDay = null;
        activeTask = null;
        challengeJSON = SceneManagment.info;
        SceneManagment.info = null;
        challenge = new Challenge(challengeJSON);
        challengeName.text = challenge.challengeTemplate;
        challengeProgressBar.value = 0;
        challengeFillValue = 1f / challenge.daysArr.Length;
        //Fill up completed tasks for Checking:
        //int tasksCompleted = Random.Range(1, 20);
        //foreach(Day d in challenge.daysArr) 
        //{
        //    foreach(Task t in d.tasks)
        //    {
        //        if(tasksCompleted > 0)
        //        {
        //            t.isTaskCompleted = true;
        //            tasksCompleted--;
        //        }
        //    } 
        //}
        // end
        foreach(Day day in challenge.daysArr)
        {
            dayProgressBar.value = 0;
            dayFillValue = 1f / day.tasks.Length;
            foreach (Task task in day.tasks)
            {
                if (!task.isTaskCompleted)
                {
                    activeTask = task;
                    activeDay = day;
                    UpdateText();
                    break;
                }
                dayProgressBar.value += dayFillValue;
            }
            if (activeDay != null)
                break;
            challengeProgressBar.value += challengeFillValue;
        }
    }
    /// <summary>
    /// Checks if the user asked to join a new challenge or to open an existing one from server
    /// </summary>


    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToLink()
    {
        Application.OpenURL(taskUrl);
        UpdateActiveTask();

    }
    public void UpdateActiveTask ()
    {
        activeTask.isTaskCompleted = true;
        activeTask = activeTask.GetNextTask(activeDay, activeTask);
        if (activeTask == null)
        {
            activeDay = activeDay.GetNextDay(challenge, activeDay);
            activeTask = activeDay.tasks[0];
            dayProgressBar.value = 0;
            challengeProgressBar.value += challengeFillValue;
        }
        else
        {
            dayProgressBar.value += dayFillValue;
        }
        UpdateText();
    }

    private void UpdateText()
    {
        dayNum.text = "Day: " + activeDay.dayNum.ToString();
        dayTheme.text = activeDay.dayTitle;
        taskLevel.text = "Task: " + activeTask.taskLevel.ToString();
        taskDescription.text = activeTask.taskDescription;
        if (activeTask.taskDescription.Contains("http"))
        {
            int startIndex = activeTask.taskDescription.IndexOf("http");
            string temp = activeTask.taskDescription.Substring(startIndex);
            int length = temp.IndexOf(" ");
            taskUrl = temp.Substring(0, length);
            linkButton.gameObject.SetActive(true);
        }
        else
            linkButton.gameObject.SetActive(false);
    }
}
//Method that checks if the user wants to create a Single Random Challenge
//private void CheckIfNewChallenge()
//{
//    if (JasonManager.ExtractData(SceneManagment.info, "newchallenge").Equals("true"))//Check if the user wants to join a new challenge
//    {
//        JSON = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/Challenge_Options.json");
//    }
//    else // if the user wants to continue an existing challenge, SceneManagment.info will get the JSON of the challenge from the server
//    {
//        //JSON = SceneManagment.info;
//    } 
//    SceneManagment.info = null;
//}

//Code to add on DashBoard to create a Single Random Challenge:

//Dictionary<string, string> newChallengeInfo = new Dictionary<string, string>();
//newChallengeInfo.Add("newchallenge", "true");
//newChallengeInfo.Add("challenge template", selectedTemplate);
//JasonManager.CreateJson(newChallengeInfo, Application.dataPath + "/Resources/JsonFiles/CurrentChallenge.json");
//string JSON = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/CurrentChallenge.json");
//SceneManagment.LoadScene("ChallengeEngine", 0, JSON);
