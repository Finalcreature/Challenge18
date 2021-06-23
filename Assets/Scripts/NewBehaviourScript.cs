using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class NewBehaviourScript : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, string>> roaming;
    Dictionary<string, string> yossi;

    private void Awake()
    {
        roaming = new Dictionary<string, Dictionary<string, string>>();
    }
    private void Start()
    {
        roaming.Add("signIn", new Dictionary<string, string> {
            { "username", "tami" },
            { "phone", "972547932000" }
        });

        string json = JsonUtility.ToJson(roaming);
        Debug.Log(roaming);
        WWWForm wwwform = new WWWForm();
        var headers = wwwform.headers;
        headers["content-type"] = "application/json";
        wwwform.AddField("username", "tami");
        wwwform.AddField("phone", "972547932000");
        UnityWebRequest.Post("http://193.46.199.76:8087/api", wwwform);
    }
}
