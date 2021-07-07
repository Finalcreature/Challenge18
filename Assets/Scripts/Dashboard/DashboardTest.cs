using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using BME;

public class DashboardTest : MonoBehaviour
{
    #region Rules
    /*
        Writting Gamal
        private/public
        const Capital letters
        private _
        bool is

        Suffix:
        T - Text
        P - Panel
        Btn - Button
    */
    #endregion

    #region Design

    /*
     
    - Show Account Details on the top
    - Show My Challenges on the bottom
    - Humburger menu on the top right side
    */

    #endregion

    #region Variables

    string data;
    UserRoot root;
    string temp;
    [SerializeField] TextMeshProUGUI _usernameT, _phoneNumberT, _fullNameT, _emailT, _challengeLangT; //Texts == string variables from registration page
    //Maybe replace by [] and use for-loop

    ///*[SerializeField]???*/ Button _editProfileBtn, _joinChallengeBtn, _sideMenuBtn;

    [SerializeField] GameObject _editProfileP, _joinChallengeP;


    #endregion

    /// <summary>
    ///Take all Text variables and assign the correct value from Ido's strings
    ///e.g _usernameT.text = username;
    ///
    ///if email is empty ===> _emailT.text == "Not filled yet"
    /// </summary>
    public void SetTexts(UserRoot root)
    {
        this.root = root;
        _usernameT.text = root.User.Username;
        _phoneNumberT.text = root.User.Phone;
        _fullNameT.text = root.User.FullName;
        _emailT.text = root.User.Email;
       // _challengeLangT.text = JasonManager.ExtractData(data, "username");

    }

 
        
    

    //public void SetNewData(string newData) //TODO change string to a class
    //{
    //    //TODO get the chagne request code
    //    _editProfileP.SetActive(false);
    //}


    /// <summary>
    /// <para>Based on the given parameter, the chosen panel will be active </para>
    ///<para> Will turn off panels by default </para>
    /// </summary>
    /// <param name="_panelName"></param>
    public void ShowPanel(string _panelName) 
    {
        switch (_panelName)
        {
            case ("Profile"):
                { _editProfileP.SetActive(true); }
                break;
            case ("Join"):
                { _joinChallengeP.SetActive(true);
                    Visuals.SelectToggle();
                }
                break;
            default:
                {
                    _joinChallengeP.SetActive(false);
                    _editProfileP.SetActive(false);
                }
                break;
        }
    }

    void Start()
    {
        _editProfileP.SetActive(false);
        _joinChallengeP.SetActive(false);
        data = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/UserDetails.json");
        root = JasonManager.GetData(data);
        SetTexts(root);
        Dictionary<string, string> templates = new Dictionary<string, string>();
        templates.Add("userID", root.User.Id);
        templates.Add("getTemplateNames", "");
        JasonManager.CreateJson(templates, Application.dataPath + "/Resources/JsonFiles/Templates.json");
        StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/Templates.json", root.AccessToken));
    }

   
    void Update()
    {
     
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                _editProfileP.SetActive(false);
                _joinChallengeP.SetActive(false);

                return;
            }
        }
    }



    /// <summary>
    /// Summery: Add the challenge to the poll based on the selected topic
    /// </summary>
    public void JoinChallenge()
    {
        //TODO send a post request and send it in the coroutine
        //StartCoroutine(SceneManagment.LoadScene("CurrentChallenge", 0, File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/Challenge_Options.json")));
        Dictionary<string, string> challengeRequest = new Dictionary<string, string>();
        challengeRequest.Add("userID", root.User.Id);
        challengeRequest.Add("userRequestChallenge", "New Challenge");
        JasonManager.CreateJson(challengeRequest, Application.dataPath + "/Resources/JsonFiles/ChallengeRequest.json");
        StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/ChallengeRequest.json", root.AccessToken));
        _joinChallengeP.SetActive(false);

    }

    public void ShowToggleSelection()
    {
        Visuals.SelectToggle();
    }
}
