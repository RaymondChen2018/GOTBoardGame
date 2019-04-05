using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server_Proxy : NetworkBehaviour {
    //State
    public enum STATE
    {
        Lobby, LoadingMap, Map
    }
    public static Server_Proxy singleton = null;
    public STATE state = STATE.Lobby;
    
    //Map
    [SyncVar] public string map_choice = "Map1";

    

    //
    Dedicated_lobby dedicated = null;
    // Use this for initialization
    public Server_Proxy() {
        singleton = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        dedicated = FindObjectOfType<Dedicated_lobby>();
    }

    // Update is called once per frame
    void Update () {
		if(isServer && state == STATE.Map && dedicated.numberConnected == 0)
        {
            state = STATE.Lobby;
            Lobby_BaseClass.singleton.ServerReturnToLobby();
        }
	}

}
