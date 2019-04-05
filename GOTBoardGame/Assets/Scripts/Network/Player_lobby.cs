using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_lobby : NetworkLobbyPlayer
{
    public int connID = -1;
    private FactionHouse _factionChoice = FactionHouse.None;
    public FactionHouse factionChoice
    {
        get
        {
            return _factionChoice;
        }
        private set
        {
            _factionChoice = value;
        }
    }
    // Use this for initialization
    void Start () {
        transform.SetParent(Lobby_BaseClass.singleton.LobbyPlayerBranch.transform);
    }


}
