using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EditProfileLogic : MonoBehaviour
{
    [SerializeField] InputField _username;
    // Start is called before the first frame update
    void Start()
    {
        _username.text = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/UserDetails.json");
        _username.text = JasonManager.ExtractData(_username.text, "username");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
