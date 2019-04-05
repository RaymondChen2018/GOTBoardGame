using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class Dedicated_lobby : MonoBehaviour
{
    [SerializeField] private GameObject network_Watcher = null;
    public int numberConnected = 0;
    [SerializeField] private Lobby_BaseClass lobbyManager;

    // Use this for initialization
    void Start()
    {
        startServer();
    }

    public void startServer()
    {
        lobbyManager.state = Lobby_BaseClass.STATE.Online;
        lobbyManager.StartServer();
        network_Watcher = Instantiate(network_Watcher);
        NetworkServer.Spawn(network_Watcher);
    }

    

    /// <summary>
    /// Evaluate the connection and determines whether the connection should stay
    /// </summary>
    /// <param name="conn"></param>
    /// <returns></returns>
    public bool validConnection(NetworkConnection conn)
    {
        bool valid = true;
        if (numberConnected >= lobbyManager.maxPlayers)
        {
            valid = false;
        }
        else if(Server_Proxy.singleton.state == Server_Proxy.STATE.Map)
        {
            valid = false;
        }
        return valid;
    }
}

