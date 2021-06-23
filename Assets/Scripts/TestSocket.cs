using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.Networking;

public class TestSocket : SocketIOComponent
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if(IsConnected)
        {
            print("Connected");
        }
        else
        {
            print("Not Connected");
        }

        
    }

    public override void Update()
    {
        base.Update();
        
    }
}

