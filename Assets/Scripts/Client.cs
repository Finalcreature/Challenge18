using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using System.IO;


public class Client 
{
    public Dictionary<string, Dictionary<string, string>> roaming
    {
        get { return this._roaming ?? (this._roaming = new Dictionary<string, Dictionary<string, string>>()); }
        set { this._roaming = value; }
    }
    private Dictionary<string, Dictionary<string, string>> _roaming;

    
    public Client()
    {
        this.roaming.Add("signIn", new Dictionary<string, string> {
            { "username", "tami" },
            { "phone", "972547932000" },
        });
    }
}
