using System;
using System.Collections;
using System.Collections.Generic;
using TomKCPGameLib;
using UnityEngine;

public class test : MonoBehaviour {

    // Use this for initialization
    IntPtr sess;
	void Start () {
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
