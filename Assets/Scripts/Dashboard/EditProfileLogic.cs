using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using BME;

public class EditProfileLogic : MonoBehaviour
{
    [SerializeField] TMP_InputField _username;
    private string email;
    private string accessToken;
    private string Json;
    // Start is called before the first frame update
    void Start()
    {
        _username.text = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/UserDetails.json");
        _username.text = JasonManager.ExtractData(_username.text, "username");
    }
    
    //Note: Currently connected to the input field - need to find a way to save inputfields info to strings
    public void Change()
    {
        string json = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/UserDetails.json");
        accessToken = JasonManager.ExtractData(json, "access_token");
        Dictionary<string, string> bigKey = new Dictionary<string, string>();
        bigKey.Add("userID", "972549603373@c.co");
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        keyValuePairs.Add("email", email);
        JasonManager.CreateJson(keyValuePairs , "editProfile", bigKey, Application.dataPath + "/Resources/JsonFiles/NewName.json");
        print(File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/NewName.json"));
        StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/NewName.json"));
        
        //Json = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/NewName.json");
        //  Invoke("SetNewDetails", 3);
    }

    //public void SetNewDetails()
    //{
    //    JasonManager.CreateJson(Json, "editProfile", Application.dataPath + "/Resources/JsonFiles/NewName.json");
    //    print(Application.dataPath + "/Resources/JsonFiles/NewName.json");
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewData(string value)
    {
        email = value;
    }
}
