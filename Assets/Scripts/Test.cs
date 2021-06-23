//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;
//using System.IO;
//using System.Text;
//using UnityEngine.UI;

//public class Test : MonoBehaviour
//{
//    [SerializeField] WWWForm form;

 

//    public void SendJson(string json)
//    {
//        //StartCoroutine(PostData_Coroutine(json));
//        POST();
//    }

//    //IEnumerator PostData_Coroutine(string json)
//    //{
//    //    //string url = "http://193.46.199.76:8087";
//    //    form = new WWWForm();
//    //    form.AddField("username", "tami");
//    //    form.AddField("phone", "972547932000");
//    //    Dictionary<string, string> headers = form.headers;
//    //    string JSON = "{ 'signIn':{ 'username':'tami','phone':'972547932000'}";
//    //    Hashtable postHeader = new Hashtable();
//    //    postHeader.Add("Content-Type", "application/json");
//    //    var bodyRaw = Encoding.UTF8.GetBytes(JSON);
        
//        //json = JsonUtility.ToJson(signin.headers);

////        WWW www = new WWW(url, bodyRaw, postHeader);
        
//        //    yield return request.SendWebRequest();
//        //    if (request.isNetworkError || request.isHttpError)
//        //        Debug.Log(request.error);
//        //    else
//        //        Debug.Log(request.downloadHandler.text);
//        //}
//        //using (UnityWebRequest request = UnityWebRequest.Post(url, null, bodyRaw)) 
//        //{
//        //    yield return request.SendWebRequest();
//        //    if (request.isNetworkError || request.isHttpError)
//        //        Debug.Log(request.error);
//        //    else
//        //        Debug.Log(request.downloadHandler.text);
//        //}


//   // }
//    public WWW POST()
//    {
//        WWW www;
//        string url = "http://193.46.199.76:8087";
//        Hashtable postHeader = new Hashtable();
//        postHeader.Add("Content-Type", "application/json");

//        // convert json string to byte
//        string JSON = "{ 'signIn':{ 'username':'tami','phone':'972547932000'}";
        
//        var formData = System.Text.Encoding.UTF8.GetBytes(JSON);
//        form.AddField("username", "Nadav");
//        Dictionary<string, string> headers = form.headers;
        
//        www = new WWW(url, formData, postHeader);
//        StartCoroutine(WaitForRequest(www));
//        return www;
//    }

//    IEnumerator WaitForRequest(WWW data)
//    {
//        yield return data; // Wait until the download is done
//        if (data.error != null)
//        {
//            Debug.Log("There was an error sending request: " + data.error);
//        }
//        else
//        {
//            Debug.Log("WWW Request: " + data.text);
//        }
//    }

//    [SerializeField] TextAsset json;
//    //void Start()
//    //{
//    //    StartCoroutine(PostData());
//    //}

//    //IEnumerator PostData()
//    //{
//    //    string url = "http://193.46.199.76:8087";
//    //    yield return null;
//    //   // string newJson = JsonUtility.ToJson(json.bytes);
//    //    print(json.text);
//    //    byte[] bodyRaw = Encoding.UTF8.GetBytes(json.text);
//    //    Dictionary<string, string> headers = form.headers;
//    //    using (UnityWebRequest request = UnityWebRequest.Post(url,null ,bodyRaw))
//    //    {
//    //        yield return request.SendWebRequest();
//    //        if (request.isNetworkError || request.isHttpError)
//    //            Debug.Log(request.error);
//    //        else
//    //            Debug.Log(request.downloadHandler.text);
//    //    }


//    //    //string url = "http://193.46.199.76:8087";
//    //    //form = new WWWForm();
//    //    //form.AddField("username", "tami");
//    //    //form.AddField("phone", "972547932000");
//    //    //Dictionary<string, string> headers = form.headers;
//    //    //byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
//    //    // json = JsonUtility.ToJson(signin.headers);


//    //    //using (UnityWebRequest request = UnityWebRequest.Post(url, headers))
//    //    //{
//    //    //    yield return request.SendWebRequest();
//    //    //    if (request.isNetworkError || request.isHttpError)
//    //    //        Debug.Log(request.error);
//    //    //    else
//    //    //        Debug.Log(request.downloadHandler.text);
//    //    //}


//    //}
//}

