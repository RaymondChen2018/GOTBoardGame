using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lobby_BaseClass : NetworkLobbyManager
{
    public enum STATE
    {
        /// <summary>
        /// Local instance is not connected to any server.
        /// </summary>
        Offline,
        /// <summary>
        /// Local instance is connecting to a server but has not received a response yet.
        /// </summary>
        Connecting,
        /// <summary>
        /// Local instance is connected to a server and the player is ready in lobby; 
        /// Server proxy available.
        /// </summary>
        Online,
        /// <summary>
        /// Connection disconnected by the client (disconnect button; closes app etc.), as oppose to forced disconnection (eg. time-out; kicked by server etc.).
        /// </summary>
        Disconnecting,
        /// <summary>
        /// Local instance receiving error note regarding to failure to connect to server.
        /// </summary>
        ErrorMsg
    }
    public STATE state = STATE.Offline;
    public new static Lobby_BaseClass singleton = null;
    public GameObject LobbyPlayerBranch;

    void Start()
    {
        singleton = this;
    }
    //==============================Server=============================
    [SerializeField] private Dedicated_lobby dedicatedProxy = null;
    public override void OnServerConnect(NetworkConnection conn)
    {
        if (dedicatedProxy.validConnection(conn))
        {
            dedicatedProxy.numberConnected++;
            base.OnServerConnect(conn);
        }
        else
        {
            conn.Disconnect();
        }
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        dedicatedProxy.numberConnected--;
    }

    public override void OnLobbyServerPlayersReady()
    {
        base.OnLobbyServerPlayersReady();
        Server_Proxy.singleton.state = Server_Proxy.STATE.LoadingMap;
    }
    public override void OnLobbyServerSceneChanged(string sceneName)
    {
        base.OnLobbyServerSceneChanged(sceneName);
        if (sceneName == Server_Proxy.singleton.map_choice)
        {
            Server_Proxy.singleton.state = Server_Proxy.STATE.Map;
        }
    }
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        FactionHouse factionChoice = lobbyPlayer.GetComponent<Player_lobby>().factionChoice;
        switch (factionChoice)
        {
            case FactionHouse.Arryn:
                gamePlayer.AddComponent<Arryn>();
                break;
            case FactionHouse.Baratheon:
                gamePlayer.AddComponent<Baratheon>();
                break;
            case FactionHouse.Greyjoy:
                gamePlayer.AddComponent<Greyjoy>();
                break;
            case FactionHouse.Lannister:
                gamePlayer.AddComponent<Lannister>();
                break;
            case FactionHouse.Martell:
                gamePlayer.AddComponent<Martell>();
                break;
            case FactionHouse.Stark:
                gamePlayer.AddComponent<Stark>();
                break;
            case FactionHouse.Targaryen:
                gamePlayer.AddComponent<Targaryen>();
                break;
            case FactionHouse.Tyrell:
                gamePlayer.AddComponent<Tyrell>();
                break;
        }

        return true;
    }

    //==============================Client=============================
    [SerializeField] private Client_lobby clientProxy = null;
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        state = STATE.Online;
        clientProxy.time_mark = Time.time;

        clientProxy.connectingScreen.gameObject.SetActive(false);
        clientProxy.lobbyMenu.gameObject.SetActive(true);
    }

    /// <summary>
    /// When server disconnects the client via conn.disconnect() (not by the client's stopclient())
    /// </summary>
    /// <param name="conn"></param>
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        state = STATE.ErrorMsg;
        clientProxy.time_mark = Time.time;
        clientProxy.lobbyMenu.gameObject.SetActive(false);
        clientProxy.errorScreen.gameObject.SetActive(true);
    }

}

    

