using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class PostJson : MonoBehaviour
{
    public string data;
    string jsonLocation = "/LOGIN.json";

    public void Start()
    {
        Dictionary<string, string> key = new Dictionary<string, string>();
        //key.Add("username", "tami");
        //key.Add("phone", "972547932000");
        //JasonManager.CreateJson(key, "signIn", "/LOGIN.json");
        StartCoroutine(PostData());
        }

        IEnumerator PostData()
        {
            #region Define URL and JSON info file creation
            string url = "http://193.46.199.76:8087/api";
            #endregion
            #region UnityWebRequest POST request definitions
            UnityWebRequest req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST); //class handles the request with url and request kind, in this case kHttpVerbPOST==POST
            req.uploadHandler = new UploadHandlerFile(jsonLocation);// define the json File to Upload to server
            req.downloadHandler = new DownloadHandlerBuffer(); // expected response of server, its a buffer to adjust to any response and not be empty
            req.SetRequestHeader("Content-Type", "application/json"); //Definition of Headers, cann add more like Dictionary, USED to tell server what to expect in request
            #endregion

            yield return req.SendWebRequest();
            if (req.isNetworkError)
                print("Error: " + req.error);
            if (req.isDone)
            {

                data = req.downloadHandler.text;

                //string data = JasonManager.ExtractData(data, "username");
                //print(data);

                //data = JasonManager.ExtractData(data, "phone");
                //print(data);

            }

        }

        //public string ExtractData(string json, string key)
        //{
        //    string word = json.Substring(json.IndexOf(key));
        //    word = word.Replace(key, "");
        //    word = word.Replace(":", "");
        //    word = word.Replace("\"", "");
        //    word = word.Replace("}", "");

        //    string input = word;
        //    int index = input.IndexOf(",");
        //    if (index > 0)
        //    {
        //        word = input.Substring(0, index);
        //    }            
        //        return word;
        //}

        IEnumerator GetData()
        {
            #region Define URL and JSON info file creation
            string url = "http://193.46.199.76:8087/api";
            #endregion
            #region UnityWebRequest POST request definitions
            UnityWebRequest get= new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET); //class handles the request with url and request kind, in this case kHttpVerbPOST==POST
            get.uploadHandler = new UploadHandlerFile(jsonLocation);// define the json File to Upload to server
            get.downloadHandler = new DownloadHandlerBuffer(); // expected response of server, its a buffer to adjust to any response and not be empty
            get.SetRequestHeader("Content-Type", "application/json"); //Definition of Headers, cann add more like Dictionary, USED to tell server what to expect in request
            #endregion

            yield return get.SendWebRequest();
            if (get.isNetworkError)
                print("Error: " + get.error);
            if (get.isDone)
            print(get.downloadHandler.text);

            

    }



    /// <summary>
    /// Creates A json file containing the data within ROOT Class and sends it to url server as POST request
    /// </summary>
    /// <returns>Server Response</returns>
    //IEnumerator PostData()
    //{
    //    #region Define URL and JSON info file creation
    //    string url = "http://193.46.199.76:8087/api";
    //    Root root = new Root();
    //    string JSON = JsonUtility.ToJson(root); //Convertion of ROOT class to JSON data formation
    //    Debug.Log(JSON);
    //    var directory = Application.dataPath + "/LOGIN.json"; //Change to Resources folder for use of resources functions       
    //    File.WriteAllText(directory, JSON);
    //    #endregion
    //    #region UnityWebRequest POST request definitions
    //    UnityWebRequest req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST); //class handles the request with url and request kind, in this case kHttpVerbPOST==POST
    //    Debug.Log(directory);
    //    req.uploadHandler = new UploadHandlerFile(directory);// define the json File to Upload to server
    //    req.downloadHandler = new DownloadHandlerBuffer(); // expected response of server, its a buffer to adjust to any response and not be empty
    //    req.SetRequestHeader("Content-Type", "application/json"); //Definition of Headers, cann add more like Dictionary, USED to tell server what to expect in request
    //    #endregion

    //    yield return req.SendWebRequest();
    //    if (req.isNetworkError)
    //        print("Error: " + req.error);
    //    if (req.isDone)
    //        print(req.downloadHandler.text);
    //}

    //IEnumerator PostData()
    //{
    //    #region Define URL and JSON info file creation
    //    string url = "http://193.46.199.76:8087/api";
    //    CheckUser root = new CheckUser();
    //    string JSON = JsonUtility.ToJson(root); //Convertion of ROOT class to JSON data formation
    //    Debug.Log(JSON);
    //    var directory = Application.dataPath + "/LOGIN.json"; //Change to Resources folder for use of resources functions       
    //    File.WriteAllText(directory, JSON);
    //    #endregion
    //    #region UnityWebRequest POST request definitions
    //    UnityWebRequest req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST); //class handles the request with url and request kind, in this case kHttpVerbPOST==POST
    //    Debug.Log(directory);
    //    req.uploadHandler = new UploadHandlerFile(directory);// define the json File to Upload to server
    //    req.downloadHandler = new DownloadHandlerBuffer(); // expected response of server, its a buffer to adjust to any response and not be empty
    //    req.SetRequestHeader("Content-Type", "application/json"); //Definition of Headers, cann add more like Dictionary, USED to tell server what to expect in request
    //    #endregion

    //    yield return req.SendWebRequest();
    //    if (req.isNetworkError)
    //        print("Error: " + req.error);
    //    if (req.isDone)
    //        print(req.downloadHandler.text);
    //}


}




