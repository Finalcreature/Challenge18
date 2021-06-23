using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

[System.Serializable]
public class SignIn
{
    public string username = "tami";
    public string phone = "972547932000";
}
[System.Serializable]
public class Root
{
    public SignIn signIn = new SignIn();
}
public class PostRequest : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(PostData());
    }
    /// <summary>
    /// Creates A json file containing the data within ROOT Class and sends it to url server as POST request
    /// </summary>
    /// <returns>Server Response</returns>
    IEnumerator PostData()
    {
        #region Define URL and JSON info file creation
        string url = "http://193.46.199.76:8087/api";      
        Root root = new Root();
        string JSON = JsonUtility.ToJson(root); //Convertion of ROOT class to JSON data formation
        Debug.Log(JSON);
        var directory = Application.dataPath + "/Files/LOGIN.json"; //Change to Resources folder for use of resources functions       
        File.WriteAllText(directory, JSON);
        #endregion
        #region UnityWebRequest POST request definitions
        UnityWebRequest req = new UnityWebRequest(url,UnityWebRequest.kHttpVerbPOST); //class handles the request with url and request kind, in this case kHttpVerbPOST==POST
        Debug.Log(directory);
        req.uploadHandler = new UploadHandlerFile(directory);// define the json File to Upload to server
        req.downloadHandler = new DownloadHandlerBuffer(); // expected response of server, its a buffer to adjust to any response and not be empty
        req.SetRequestHeader("Content-Type", "application/json"); //Definition of Headers, cann add more like Dictionary, USED to tell server what to expect in request
        #endregion

        yield return req.SendWebRequest();
        if (req.isNetworkError)
            print("Error: " + req.error);
        if (req.isDone)
            print(req.downloadHandler.text);
    }
}