# Unity-KCP-CLIENT-FEC
wrap a kcp_with_fec lib to Unity

# Link
lib: https://github.com/xtaci/libkcp  
server: https://github.com/xtaci/kcp-go

# useage 
```C#
using System;
using System.Collections;
using System.Collections.Generic;
using TomKCPGameLib;
using UnityEngine;

public class test : MonoBehaviour {

    // Use this for initialization
        IntPtr sess;
	void Start () {
	//sess = DLL.KCPDial("ip", 10000, 0, 0); disable fec
        sess = DLL.KCPDial("ip", 10000, 2, 1);
        if(!sess.Equals(0))
        {
            DLL.KCPSetNoDelay(sess, 1, 5, 2, 1);
            DLL.KCPSetStreamMode(sess, false);
            DLL.KCPSetDSCP(sess, 46);
            DLL.KCPSetMtu(sess, 1400);
        }
    }
	
	// Update is called once per frame
	void Update () {
        DLL.KCPUpdate(sess);

        var bufRecv = new byte[1024];
        DLL.Read(sess, bufRecv, 1024);

        //update game logic

        DLL.Write(sess, bufRecv, 1024/*datelen*/);
    }
	
	 void Destroy()
    {
        DLL.KCPReset(sess);
    }
}

```