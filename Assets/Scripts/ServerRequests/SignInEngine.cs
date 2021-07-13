using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using BME;

public class SignInEngine : MonoBehaviour
{
    public bool isUsernameValid;
    [SerializeField] Text userNameTextBox;
    [SerializeField] Text phoneTextBox;
    Dictionary<string, string> signInDetails;
    string jsonLocation;
    bool keepSignedIn;
    // Start is called before the first frame update
    void Start()
    {
        keepSignedIn = false;
        isUsernameValid = false;
        jsonLocation = Application.dataPath + "/Resources/JsonFiles";
        signInDetails = new Dictionary<string, string>();
        if (File.Exists(jsonLocation + "/SignIn.json")) //TODO if user logs out, need to delete signin json file.
        {
            keepSignedIn = true;
            string signInJSON = File.ReadAllText(jsonLocation + "/SignIn.json");
            SignIn(JasonManager.ExtractData(signInJSON, "username"), JasonManager.ExtractData(signInJSON, "phone"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Send Post Request to server to Sign In User
    /// </summary>
    public void SignIn()
    {
        signInDetails.Clear();//Clear Dictionary to prevent adding same Key names
        signInDetails.Add("checkUsername", userNameTextBox.text);
        JasonManager.CreateJson(signInDetails, jsonLocation + "/checkUsername.json");
        StartCoroutine(JasonManager.PostData(jsonLocation + "/checkUsername.json"));
        StartCoroutine(CheckSignIn(userNameTextBox.transform.parent.gameObject));
        StartCoroutine(TryLogIn(userNameTextBox.text, phoneTextBox.text));
    }
    private void SignIn(string username, string phone)
    {
        signInDetails.Clear();//Clear Dictionary to prevent adding same Key names
        signInDetails.Add("checkUsername", username);
        JasonManager.CreateJson(signInDetails, jsonLocation + "/checkUsername.json");
        StartCoroutine(JasonManager.PostData(jsonLocation + "/checkUsername.json"));
        StartCoroutine(CheckSignIn(userNameTextBox.transform.parent.gameObject));
        StartCoroutine(TryLogIn(username, phone));
    }
    IEnumerator TryLogIn(string username, string phone)
    {
        yield return new WaitForSeconds(0.5f);
        if (isUsernameValid)
        {
            signInDetails.Clear();//Clear Dictionary to prevent adding same Key names
            signInDetails.Add("username", username);
            signInDetails.Add("phone", phone); //972547932000
            JasonManager.CreateJson(signInDetails, "signIn", jsonLocation + "/signIn.json");
            StartCoroutine(JasonManager.PostData(jsonLocation + "/signIn.json"));
            StartCoroutine(CheckSignIn(phoneTextBox.transform.parent.gameObject));
        }
    }
    private IEnumerator CheckSignIn(GameObject field)
    {
        yield return new WaitUntil(() => JasonManager.data != null);
        if (JasonManager.data.Contains("Oops")) //UserName Exists in Server
        {
            isUsernameValid = true;
            field.GetComponent<Image>().color = Color.white;
        }
        else if (JasonManager.data.Contains("Great")) //UserName Doesnt Exists
        {
            isUsernameValid = false;
            field.GetComponent<Image>().color = new Color32(153, 103, 103, 255);
        }
        else if (JasonManager.data.Contains("Bad")) //Password Incorrect
        {
            field.GetComponent<Image>().color = new Color32(153, 103, 103, 255);
        }
        else
        {
            File.WriteAllText(jsonLocation + "/UserDetails.json", JasonManager.data);
            //Login Approved
            if (!keepSignedIn)
            {
                File.Delete(jsonLocation + "/signIn.json");
            }
                
            StartCoroutine(SceneManagment.LoadScene("Dashboard", 0));           //Send to Dashboard
        }
    }
    public void MoveToRegistration()
    {
        StartCoroutine(SceneManagment.LoadScene("FillDetails", 0));
    }
    public void IsKeepSignedInOn()
    {
        Toggle toggle = GameObject.Find("KeepSignedIn").GetComponent<Toggle>();
        if (toggle.isOn)
            keepSignedIn = true;
        else
            keepSignedIn = false;
    }
}
