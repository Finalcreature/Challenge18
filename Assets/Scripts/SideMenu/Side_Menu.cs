using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// Func to switch the side menu display on screen on/off
    /// </summary>
    public void DisplayMenu()
    {
        if(menuCanvas.activeInHierarchy)
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
        AudioManager.instance.Play("Button1");
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
            menuCanvas.SetActive(true);
        }
    }

    /// <summary>
    /// Open in a browser challange18 Site
    /// </summary>
    public void LinkToSite()
    {
        Application.OpenURL("https://ting.global/");
    }
}
