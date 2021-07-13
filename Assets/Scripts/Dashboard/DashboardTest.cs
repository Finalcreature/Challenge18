using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
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

    string _data;
    static string _category;
    UserRoot _root;
    [SerializeField] TextMeshProUGUI _usernameT, _phoneNumberT, _fullNameT, _emailT, _challengeLangT;
    [SerializeField] GameObject _editProfileP, _joinChallengeP;
    GameObject _challengeTemplate, _challengePanel;
    int _numOfChallenges;


    #endregion

    /// <summary>
    ///Take all Text variables and assign the correct value from Ido's strings
    ///e.g _usernameT.text = username;
    ///
    ///if email is empty ===> _emailT.text == "Not filled yet"
    /// </summary>
    public void SetTexts(UserRoot root)
    {
        this._root = root;
        _usernameT.text = root.User.Username;
        _phoneNumberT.text = root.User.Phone;
        _fullNameT.text = root.User.FullName;
        _emailT.text = root.User.Email;
    }

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
                    _category = "SDG International";
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
        _challengePanel = GameObject.Find("ChallengePanel");
        _challengeTemplate = Resources.Load<GameObject>("Prefabs/Challenge_Template");
        _editProfileP.SetActive(false);
        _joinChallengeP.SetActive(false);
        _data = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/UserDetails.json");
        _root = JasonManager.GetData(_data);
        SetTexts(_root); 
        StartCoroutine("GetTemplates");
    }

    /// <summary>
    /// Mid function to make sure the data for the table is correct
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetTemplates()
    {
        Dictionary<string, string> _templates = new Dictionary<string, string>();
        _templates.Add("userID", _root.User.Id);
        _templates.Add("getTemplateNames", "");
        JasonManager.CreateJson(_templates, Application.dataPath + "/Resources/JsonFiles/Templates.json");
        _numOfChallenges = 0;
        yield return new WaitUntil(() => JasonManager.data != null);
        StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/Templates.json", _root.AccessToken));
        StartCoroutine("SetTable");
    }

    /// <summary>
    /// Set the table in the dashboard according to the user's data
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetTable()
    {
        //Pereset as an array for easier for loop
        string[] _templateKeys = { "name", "language", "day", "numOfUsers", "score", "invite" };
       

        yield return new WaitUntil(() => JasonManager.data != null);
        _data = JasonManager.data;

        #region Convert data to JObject
        //This allows for easy access to number of keys in a token
        JObject _temp = JObject.Parse(_data);
        
        print(_temp.SelectToken("user.myChallenges"));

        if (_temp.SelectToken("user.myChallenges") != null)
        {
            _challengePanel.SetActive(true);
            if(_numOfChallenges == 0)
            {
                foreach (var key in _temp.SelectToken("user.myChallenges"))
                {
                    _numOfChallenges++;
                }
            }

            #endregion

            #region Instantiate and fill table cells


            for (int i = 1; i <= _numOfChallenges; i++)
            {
                GameObject myTemplate = Instantiate(_challengeTemplate, GameObject.Find("Table").transform.GetChild(1));
                string template = JasonManager.ExtractData(_data, "template" + i);
                for (int keyIndex = 0; keyIndex < _templateKeys.Length; keyIndex++)
                {
                    myTemplate.transform.GetChild(keyIndex).GetComponentInChildren<TextMeshProUGUI>().text = JasonManager.ExtractData(template, _templateKeys[keyIndex]);
                }
            }
        }
        else
        {
            _challengePanel.SetActive(false);
        }
    }
    
        #endregion
    

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
        challengeRequest.Add("userID", _root.User.Id);
        challengeRequest.Add("userRequestChallenge", _category);
        JasonManager.CreateJson(challengeRequest, Application.dataPath + "/Resources/JsonFiles/ChallengeRequest.json");
        StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/ChallengeRequest.json", _root.AccessToken));
        _joinChallengeP.SetActive(false);

        if(GameObject.Find("Table"))
        {
            foreach(Transform child in (GameObject.Find("Table").transform.GetChild(1)))
            {
                Destroy(child.gameObject);
            }
        }
        StartCoroutine("GetTemplates");
    }

    public void ShowToggleSelection()
    {
        Visuals.SelectToggle();
    }

    public void SetCategory(string category)
    {
        _category = category;
    }
}
