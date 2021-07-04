using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;

public class Side_Menu : MonoBehaviour
{
    #region Singleton
    private static Side_Menu _instance;

    public static Side_Menu Instance
    {
        get { return _instance; }
    }
    #endregion
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject contactMenu;
    [SerializeField] private List<AudioFileObject> soundLibrary;
    
        
    private void Awake()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        #endregion
        menuCanvas.SetActive(false);  
        soundLibrary = AudioManager.instance.GetSoundLibrary(); // the sound librart known to AudioManager
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// Func to switch the side menu display on screen on/off
    /// </summary>
    public void DisplayMenu()
    {
        if (menuCanvas.activeInHierarchy)
            menuCanvas.SetActive(false);
        else
        {
            menuCanvas.SetActive(true);
            if (!mainButtons.activeInHierarchy)
            {
                mainButtons.SetActive(true);
                contactMenu.SetActive(false);
            }
            else
                contactMenu.SetActive(false);
        }     
    }

    /// <summary>
    /// Control over Contact Us button in side menu, switches displays
    /// </summary>
    public void ContactUs()
    {
        if (mainButtons.activeInHierarchy)
        {
            mainButtons.SetActive(false);
            contactMenu.SetActive(true);
        }
        else
        {
            contactMenu.SetActive(false);
            mainButtons.SetActive(true);
        }
    }

    /// <summary>
    /// Open in a browser challange18 Site
    /// </summary>
    public void LinkToSite()
    {
        Application.OpenURL("https://ting.global/");
    }

    public void Logout()
    {
        //sign out with the global library
    }

    /// <summary>
    /// SideMenu func to send to app scenes
    /// </summary>
    /// <param name="SceneName">string Name of scene wanted</param>
    public void SendToScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void Play_SideMenu(string soundName)
    {
        if(Enum.IsDefined(typeof(Sounds), soundName)) // if the sounds enum contains the said sound
        {
            for(int i = 0; i < soundLibrary.Count; i++)
            {
                if (soundLibrary[i].name == soundName)
                    AudioManager.instance.PlaySoundInternal(i);
            }
        }
    }
}
