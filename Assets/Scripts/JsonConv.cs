using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Networking;

public class JsonConv : MonoBehaviour
{
    IEnumerator PostData()
    {
        string url = "http://193.46.199.76:8087/api";
        #region
        //Root root = new Root();
        //string JSON = JsonUtility.ToJson(root);
        //Debug.Log(JSON);
        //UnityWebRequest req = UnityWebRequest.Post(url, JSON);
        #endregion
        #region postinfo
        PostInfo postinfo = new PostInfo();
        postinfo.Add("username", "tami");
        postinfo.Add("phone", "972547932000");
        #endregion
        var directory = Application.dataPath + "/Files/LOGIN.json";
        #region
        //WWWForm wwwform = new WWWForm();
        //var headers = wwwform.headers;
        //headers["content-type"] = "application/json";
        //wwwform.AddField("username", "tami");
        //wwwform.AddField("phone", "972547932000");
        #endregion
        UnityWebRequest req = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        Debug.Log(directory);
        #region
        //UploadHandlerRaw uploadHandler = new UploadHandlerRaw(postinfo.toJsonBytes());
        #endregion
        req.uploadHandler = new UploadHandlerFile(directory);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();
        if (req.isNetworkError)
            print("Error: " + req.error);
        if (req.isDone)
            print(req.downloadHandler.text);
    }
    [System.Serializable]
    private class PostInfo : Dictionary<string, string>
    {
        public byte[] toJsonBytes()
        {
            return Encoding.ASCII.GetBytes(toJsonString());
        }
        public string toJsonString()
        {
            string result = "{";
            foreach (string key in this.Keys)
            {
                string value;
                TryGetValue(key, out value);
                result += "\"" + key + "\":\"" + value + "\",";
            }
            result = result.Substring(0, result.Length - 1) + "}";
            return result;
        }
    }
}


