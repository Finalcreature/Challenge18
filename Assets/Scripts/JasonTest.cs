//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using UnityEngine.UI;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;



//public class JasonTest : MonoBehaviour
//{
//    //Info myClass;
//    public SignIn root;

//    private void Start()
//    {
//        root = new SignIn();
//        root.signIn.Add("username", "tami");
//        root.signIn.Add("phone", "972547932000");
//        string json = JsonConvert.SerializeObject(root);
//        var jsonObj = JObject.Parse(json);

        

//        Dictionary<string, string> dic = new Dictionary<string, string>();
//        dic.Add("username", "name");
//        dic.Add("phone", "123455");
//        string exJson = JsonConvert.SerializeObject(dic);
        
//        JObject newObjct = new JObject()
//        {
//            //["signIn"] = jsonObj
//            ["start"] = exJson
//        };

       
        
//        //string json = JsonConvert.SerializeObject(root);
//        //var serializer = JsonConvert.SerializeObject(j);
       
//        Debug.Log("My test: " + newObjct);

        
//        //
        
       

//            //signin = new Info[1] {myClass};

//         //serializer = JsonConvert.SerializeObject(serializer);
//        // File.WriteAllText(Application.dataPath + "/jsonText.json", jsonString);
//        // print(jsonString);
//        //FindObjectOfType<Test>().SendJson(jsonString);

//    }
//    public class SignIn
//    {
//        public Dictionary<string, string> signIn = new Dictionary<string, string>();
//    }

//    public class SignIn1
//    {
//        public Dictionary<string, string> login = new Dictionary<string, string>();
//    }

//    public class SignIn2
//    {
//        public Dictionary<string, string> register = new Dictionary<string, string>();
//    }

//     public class SignIn3
//    {
//        public Dictionary<string, string> checkName = new Dictionary<string, string>();
//    }

//    //public signIn ChangeRoot(signIn origin, string newName)
//    //{
//    //    origin = new signIn();

//    //}

//    //public class signIn
//    //{


//    //}




//    //public string username;
//    //public string phone;


//    //public void writeFile()
//    //{
//    //    string jsonString = JsonUtility.ToJson(myClass);

//    //}
//}





