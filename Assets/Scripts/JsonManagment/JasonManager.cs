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
    public static IEnumerator PostData(string directory)
    {
        data = null;
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
        }
    }
}



