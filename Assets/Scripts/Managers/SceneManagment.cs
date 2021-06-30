using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagment 
{
    public static string info;
    public static IEnumerator LoadScene(string NextScene, int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(NextScene);
    }
    public static IEnumerator LoadScene(string NextScene, int waitTime, string info)
    {
        SceneManagment.info = info;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(NextScene);
    }
}
