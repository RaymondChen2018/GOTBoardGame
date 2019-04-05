using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DebugStat : NetworkBehaviour {
    static DebugStat singleton;

    // Use this for initialization
    void Start () {
        singleton = this;
    }
	
	// Update is called once per frame
	void Update () {

    }

}
