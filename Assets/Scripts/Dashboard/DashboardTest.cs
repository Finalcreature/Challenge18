using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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
    string temp;
    [SerializeField] Text _usernameT, _phoneNumberT, _fullNameT, _emailT, _challengeLangT; //Texts == string variables from registration page
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
    private void SetTexts()
    {

        _usernameT.text = JasonManager.ExtractData(data, "username");
        _phoneNumberT.text = JasonManager.ExtractData(data, "phone");


        // _fullNameT.text = JasonManager.ExtractData(data, "fullname");
        //_emailT.text = JasonManager.ExtractData(data, "email");
        //_challengeLangT.text = JasonManager.ExtractData(data, "username");

    }

    IEnumerator GetData()
    {
        yield return new WaitUntil(() => JasonManager.data != "");
        data = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/UserDetails.json");
        SetTexts();
    }

    public void ShowPanel(string _panelName) //TODO connect to button
    {
        switch (_panelName)
        {
            case ("Profile"):
                { _editProfileP.SetActive(true); }
                break;
            case ("Join"):
                { _joinChallengeP.SetActive(true);
                    SelectToggle();
                }
                break;
            default:
                break;
        }
        //Based on the given parameter, the chosen panel will be active
        //Use switch method
    }



    // Start is called before the first frame update
    void Start()
    {
        _editProfileP.SetActive(false);
        _joinChallengeP.SetActive(false);
        StartCoroutine(GetData());

        //SetTexts(); //TODO get the data from the server
    }

    // Update is called once per frame
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
    /// Summery:
    ///     Choose the language the challenge will be
    /// </summary>
    /// <param name="index"></param>
    public void ChangeOption(int index)
    {
        //TODO Change the language
    }

    /// <summary>
    /// Summery: Add the challenge to the poll based on the selected topic
    /// </summary>
    public void JoinChallenge()
    {
        //TODO  add the challenge to poll
        _joinChallengeP.SetActive(false);
    }

    public void SelectToggle()
    {
        ColorBlock cb = new ColorBlock();
        foreach (Toggle t in FindObjectsOfType<Toggle>())
        {
            if (t.isOn)
            {
                t.targetGraphic.color = t.colors.selectedColor;
            }
            else
            {
                t.targetGraphic.color = cb.normalColor;
            }
        }

    }
}
