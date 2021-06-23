using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SignInEngine : MonoBehaviour
{
    public bool isUsernameValid;
    [SerializeField] Text userNameTextBox;
    [SerializeField] Text phoneTextBox;
    Dictionary<string, string> signInDetails;
    string jsonLocation;
    // Start is called before the first frame update
    void Start()
    {
        isUsernameValid = false;
        jsonLocation = Application.dataPath + "/JsonFiles/signIn.json";
        signInDetails = new Dictionary<string, string>();
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
        signInDetails.Add("checkUsername", userNameTextBox.GetComponent<Text>().text);
        JasonManager.CreateJson(signInDetails, Application.dataPath + "/JsonFiles/checkUsername.json");
        StartCoroutine(JasonManager.PostData(Application.dataPath + "/JsonFiles/checkUsername.json", userNameTextBox.transform.parent.gameObject, "signIn"));
        StartCoroutine(TryLogIn());
    }
    IEnumerator TryLogIn()
    {
        yield return new WaitForSeconds(0.5f);
        if (isUsernameValid)
        {
            signInDetails.Clear();//Clear Dictionary to prevent adding same Key names
            signInDetails.Add("username", userNameTextBox.GetComponent<Text>().text);
            signInDetails.Add("phone", phoneTextBox.GetComponent<Text>().text); //972547932000
            JasonManager.CreateJson(signInDetails, "signIn", jsonLocation);
            StartCoroutine(JasonManager.PostData(jsonLocation, phoneTextBox.transform.parent.gameObject, "signIn"));
        }
    }
    public void MoveToRegistration()
    {
        SceneManager.LoadScene(1);
    }
}
