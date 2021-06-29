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

    public void DisplayMenu()
    {
        if(menuCanvas.activeInHierarchy)
            menuCanvas.SetActive(false);
        else
            menuCanvas.SetActive(true);
        AudioManager.instance.Play("Button1");
    }
}
