using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net.Mail;

public class RegistrationEngine : MonoBehaviour
{
    [SerializeField] GameObject userNameTextBox;
    [SerializeField] GameObject phoneTextBox;
    [SerializeField] GameObject fullNameTextBox;
    [SerializeField] GameObject emailTextBox;
    [SerializeField] GameObject languageSelection;
    [SerializeField] Text errorText;

    Dictionary<string, string> registerDetails;
    string jsonLocation;

    public bool isUsernameValid;
    public bool isPhoneValid;
    // Start is called before the first frame update
    void Start()
    {
        jsonLocation = Application.dataPath + "/Resources/JsonFiles/register.json";
        registerDetails = new Dictionary<string, string>();
        errorText.gameObject.SetActive(false);
        userNameTextBox.transform.GetChild(2).gameObject.SetActive(false);
        phoneTextBox.transform.GetChild(2).gameObject.SetActive(false);
        isPhoneValid = false;
        isUsernameValid = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Check if Username is available within Server
    /// </summary>
    /// <param name="userName">String (Max ~ 20 Chars) </param>
    public void CheckUserName(string userName)
    {
        if(userName.Length == 0)
        {
            Debug.Log("Error");
            isUsernameValid = false;
            userNameTextBox.GetComponent<Image>().color = new Color32(153, 103, 103, 255);
        }
        else
        {
            Dictionary<string, string> usernameCheck = new Dictionary<string, string>();
            usernameCheck.Add("checkUsername", userName);
            JasonManager.CreateJson(usernameCheck, Application.dataPath + "/Resources/JsonFiles/checkUsername.json");
            StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/checkUsername.json"));
            CheckField(userNameTextBox);
        }
    }
    /// <summary>
    /// Check if PhoneNumber meets specified Requirements
    /// </summary>
    /// <param name="phone">String (Only Numbers, Min 8 Chars and Max 20)</param>
    /// <returns>True For Meeting the Requirements</returns>
    public void CheckPhone(string phone)
    {
        if (phone.Length == 0)
        {
            Debug.Log("Error");
            isPhoneValid = false;
            phoneTextBox.GetComponent<Image>().color = new Color32(153, 103, 103, 255);
        }
        else
        {
            Dictionary<string, string> phoneCheck = new Dictionary<string, string>();
            phoneCheck.Add("checkPhone", phone);
            JasonManager.CreateJson(phoneCheck, Application.dataPath + "/Resources/JsonFiles/checkPhone.json");
            StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/checkPhone.json"));
            CheckField(phoneTextBox);
        }
    }
    /// <summary>
    /// Check if Email meets specified Requirements
    /// </summary>
    /// <param name="emailAdress">String (Email Format) </param>
    /// <returns>True For Requirements Met</returns>
    public bool IsValidEmail(string emailToCheck)
    {
        if (emailToCheck.Length > 0)
        {
            try
            {
                MailAddress mail = new MailAddress(emailToCheck);
                return true;
            }
            catch (Exception e)
            {
                emailTextBox.GetComponent<Image>().color = new Color32(153, 103, 103, 255);
                return false;
            }
        }
        else
            return true;
    }
    /// <summary>
    /// Register New User into the Server
    /// </summary>
    public void Register()
    {
        if (isPhoneValid && isUsernameValid && IsValidEmail(emailTextBox.transform.GetChild(2).GetComponent<Text>().text))
        {
            registerDetails.Clear();
            registerDetails.Add("username", userNameTextBox.transform.GetChild(2).gameObject.GetComponent<Text>().text);
            registerDetails.Add("phone", phoneTextBox.transform.GetChild(2).gameObject.GetComponent<Text>().text);
            registerDetails.Add("fullname", fullNameTextBox.transform.GetChild(2).gameObject.GetComponent<Text>().text);
            registerDetails.Add("email", emailTextBox.transform.GetChild(2).gameObject.GetComponent<Text>().text);
            registerDetails.Add("language", languageSelection.GetComponentInChildren<Text>().text);
            JasonManager.CreateJson(registerDetails, "register", jsonLocation);
            StartCoroutine(JasonManager.PostData(jsonLocation));
            if(JasonManager.data.Contains(""))
            {
                //Move to Dashboard
            }
        }
        else
        {
            errorText.gameObject.SetActive(true);
        }
    }
    private void CheckField(GameObject field)
    {
        if (JasonManager.data.Contains("Oops")) //UserName/Phone Exists in Server
        {
            field.transform.GetChild(3).gameObject.SetActive(true);
            field.GetComponent<Image>().color = new Color32(153, 103, 103, 255); //Mark Filed Incorrect
            if (field.transform.name == "UserName")
                isUsernameValid = false;
            else
                isPhoneValid = false;
        }
        else if (JasonManager.data.Contains("Great")) // Mark Field Ok
        {
            field.transform.GetChild(3).gameObject.SetActive(false);
            field.GetComponent<Image>().color = Color.white;
            if (field.transform.name == "UserName")
                isUsernameValid = true;
            else
                isPhoneValid = true;
        }
    }
}
