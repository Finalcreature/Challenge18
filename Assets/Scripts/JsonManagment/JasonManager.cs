using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public static class JasonManager 
{
    public static string data;

    /// <summary>
    /// Serealize Dictionary and Creates Json with RootKey attached
    /// </summary>
    /// <param name="keyValuePairs">Dictionary</param>
    /// <param name="rootKey">Root Key</param>
    /// <param name="directory">File Location</param>
    public static void CreateJson(Dictionary<string, string> keyValuePairs, string rootKey, string directory)
    {
        string JSON = JsonConvert.SerializeObject(keyValuePairs); //Serealize Dictionary
        var jsonObj = JObject.Parse(JSON); 
        var newObj = new JObject
        {
            [rootKey] = jsonObj, //Add Root Key to Excisting Dictionary
        };
        string newJsonString = newObj.ToString();
        Debug.Log(newJsonString);
        File.WriteAllText(directory, newJsonString);//Create Json File
    }
    /// <summary>
    /// Serealize Dictionary and Creates Json
    /// </summary>
    /// <param name="keyValuePairs">Dictionary</param>
    /// <param name="directory">File Location</param>
    public static void CreateJson(Dictionary<string, string> keyValuePairs, string directory)
    {
        string JSON = JsonConvert.SerializeObject(keyValuePairs); //Serealize Dictionary
        File.WriteAllText(directory, JSON);//Create Json File
    }
    /// <summary>
    /// Extracts a specific Value from a Json String 
    /// </summary>
    /// <param name="json">Json String</param>
    /// <param name="key">Key for a specific Value</param>
    /// <returns>string Value</returns>
    public static string ExtractData(string json, string key)
    {
        string word = json.Substring(json.IndexOf(key));
        word = word.Replace(key, "");
        word = word.Replace(":", "");
        word = word.Replace("\"", "");
        word = word.Replace("}", "");

        string input = word;
        int index = input.IndexOf(",");
        if (index > 0)
        {
            word = input.Substring(0, index);
        }
        return word;
    }
    /// <summary>
    /// Post Json from a specific library to server
    /// </summary>
    /// <param name="directory">Json file Directory</param>
    /// <param name="field">Current Input Field Being Checked</param>
    /// <param name="key">Register/SignIn</param>
    /// <returns></returns>
    public static IEnumerator PostData(string directory, GameObject field, string key)
    {
        #region Define URL and JSON info file creation
        string url = "http://193.46.199.76:8087/api";
        #endregion
        #region UnityWebRequest POST request definitions
        UnityWebRequest req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST); //class handles the request with url and request kind, in this case kHttpVerbPOST==POST
        req.uploadHandler = new UploadHandlerFile(directory);// define the json File to Upload to server
        req.downloadHandler = new DownloadHandlerBuffer(); // expected response of server, its a buffer to adjust to any response and not be empty
        req.SetRequestHeader("Content-Type", "application/json"); //Definition of Headers, cann add more like Dictionary, USED to tell server what to expect in request
        #endregion

        yield return req.SendWebRequest();
        if (req.isNetworkError)
            Debug.Log("Error: " + req.error);
        if (req.isDone)
        {
            Debug.Log(req.downloadHandler.text);
            data = req.downloadHandler.text;
            switch (key)
            {
                case "signIn":
                    {
                        CheckSignIn(field, req);
                        break;
                    }
                case "register":
                    {
                        CheckRegistration(field, req);
                        break;
                    }
            }
        }
    }
    private static void CheckRegistration(GameObject field, UnityWebRequest req)
    {
        if (req.downloadHandler.text.Contains("Oops")) //UserName/Phone Exists in Server
        {
            field.transform.GetChild(3).gameObject.SetActive(true);
            field.GetComponent<Image>().color = new Color32(153, 103, 103, 255); //Mark Filed Incorrect
            if (field.transform.name == "UserName")
                field.GetComponentInParent<RegistrationEngine>().isUsernameValid = false;
            else
                field.GetComponentInParent<RegistrationEngine>().isPhoneValid = false;
        }
        else if (req.downloadHandler.text.Contains("Great")) // Mark Field Ok
        {
            field.transform.GetChild(3).gameObject.SetActive(false);
            field.GetComponent<Image>().color = Color.white;
            if (field.transform.name == "UserName")
                field.GetComponentInParent<RegistrationEngine>().isUsernameValid = true;
            else
                field.GetComponentInParent<RegistrationEngine>().isPhoneValid = true;
        }
        else
        { } // Send To Dashboard
    }

    private static void CheckSignIn(GameObject field, UnityWebRequest req)
    {
        if (req.downloadHandler.text.Contains("Oops")) //UserName Exists in Server
        {
            field.GetComponentInParent<SignInEngine>().isUsernameValid = true;
            field.GetComponent<Image>().color = Color.white;
        }
        else if (req.downloadHandler.text.Contains("Great")) //UserName Doesnt Exists
        {
            field.GetComponentInParent<SignInEngine>().isUsernameValid = false;
            field.GetComponent<Image>().color = new Color32(153, 103, 103, 255);
        }
        else if (req.downloadHandler.text.Contains("Bad")) //Password Incorrect
        {
            field.GetComponent<Image>().color = new Color32(153, 103, 103, 255);
        }
        else
        {
            //Login Approved
            SceneManager.LoadScene(2);                                               //Send to Dashboard
        }
    }
}



