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
        
        Dictionary<string, object> keys = new Dictionary<string, object>();
        keys.Add("userID", "972547932000@c.us");
        Dictionary<string, string> edit = new Dictionary<string, string>();
        edit.Add("email", "kfaaf@da.com");
        edit.Add("fullName", "fwawfjk");
        JasonManager.CreateJson(edit, "editProfile", keys ,  Application.dataPath + "/Resources/JsonFiles/NewName.json");
        StartCoroutine(JasonManager.PostData(Application.dataPath + "/Resources/JsonFiles/NewName.json", JasonManager.ExtractData(File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/UserDetails.json"), "access_token")));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
