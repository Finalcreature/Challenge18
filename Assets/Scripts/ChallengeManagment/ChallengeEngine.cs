using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ChallengeEngine : MonoBehaviour
{
    [SerializeField] Challenge challenge;
    string JSON;
    private bool isNewChallenge;
    // Start is called before the first frame update
    void Start()
    {
        isNewChallenge = true; // Delete late, Just for test
        JSON = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/Challenge_Options.json"); // Delete late, Just for test
        //CheckIfNewChallenge();
        challenge = new Challenge(JSON, isNewChallenge);
        JasonManager.CreateJson(challenge, Application.dataPath + "/Resources/JsonFiles/CurrentChallenge.json");
        string newChallengeJSON = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/CurrentChallenge.json");
        Debug.Log(newChallengeJSON);
    }
    /// <summary>
    /// Checks if the user asked to join a new challenge or to open an existing one from server
    /// </summary>
    private void CheckIfNewChallenge()
    {
        if (JasonManager.ExtractData(SceneManagment.info, "newchallenge").Equals("true"))//Check if the user wants to join a new challenge
        {
            JSON = File.ReadAllText(Application.dataPath + "/Resources/JsonFiles/Challenge_Options.json");
            isNewChallenge = true;
        }
        else // if the user wants to continue an existing challenge, SceneManagment.info will get the JSON of the challenge from the server
        {
            //JSON = SceneManagment.info;
            isNewChallenge = false;
        } 
        SceneManagment.info = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
