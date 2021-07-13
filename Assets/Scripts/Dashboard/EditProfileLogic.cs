using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using TMPro;
using BME;

public class EditProfileLogic : MonoBehaviour
{
    [SerializeField] TMP_InputField _username, _fullname, _email;
    string language;
    private string data;
    public UserRoot root;
    private string _userId;
    private DashboardTest dashboard;
  
    void Start()
    {
        data = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/UserDetails.json");
        root = JasonManager.GetData(data);
        _username.text = root.User.Username;
        _fullname.text = root.User.FullName;
        _email.text = root.User.Email;
        _userId = root.User.Id;
        dashboard = FindObjectOfType<DashboardTest>(); 
        
    }
    
    public void Change()
    {     
        Dictionary<string, object> keys = new Dictionary<string, object>();
        keys.Add("userID", _userId);
        Dictionary<string, string> edit = new Dictionary<string, string>();
        edit.Add("email", _email.text);
        edit.Add("fullName", _fullname.text);
        edit.Add("language", language);
        JasonManager.CreateJson(edit, "editProfile", keys ,  Application.dataPath + "/Resources/JsonFiles/NewName.json");
        StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/NewName.json", root.AccessToken));
        StartCoroutine("UpdateServerDetails");
       
    }

    private IEnumerator UpdateServerDetails()
    {
        yield return new WaitUntil(() => JasonManager.data != null);
        Dictionary<string, string> signIn = new Dictionary<string, string>()
        {
            {"username",root.User.Username },
            {"phone", root.User.Phone}
        };
        JasonManager.CreateJson(signIn, "signIn", Application.dataPath + "/Resources/JsonFiles/UpdateDetails.json");
        StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/UpdateDetails.json"));
        StartCoroutine("UpdateUserDetails");
    }

    private IEnumerator UpdateUserDetails()
    {
        yield return new WaitUntil(() => JasonManager.data != null);

        File.WriteAllText(Application.dataPath + "/Resources/JsonFiles" + "/UserDetails.json", JasonManager.data);
        data = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles" + "/UserDetails.json");
        root = JasonManager.GetData(data);
        dashboard.SetTexts(root);
        dashboard.ShowPanel("off");
    }

    /// <summary>
    ///     Choose the language the challenge will be in
    /// </summary>
    /// <param name="index"></param> 
    public void ChangeOption(int index) //TODO the language is null in the user details - check with Yinon why is that and set it later
    {
        switch (index)
        {
            case 0: { language = "English"; }
                break;
            case 1: { language = "Hebrew"; }
                break;
        }
    }
}
