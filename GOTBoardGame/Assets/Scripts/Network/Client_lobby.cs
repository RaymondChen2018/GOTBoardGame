using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Client_lobby : MonoBehaviour
{
    public CanvasGroup mainMenu;
    public InputField input;
    public CanvasGroup connectingScreen;
    public CanvasGroup errorScreen;
    public CanvasGroup lobbyMenu;

    [SerializeField] private Lobby_BaseClass lobbyManager;

    public float time_mark = 0;

    void Start()
    {
        //lobbyManager.showLobbyGUI = false;
    }
    void Update()
    {
        if (lobbyManager.state == Lobby_BaseClass.STATE.Connecting && (Time.time - time_mark) > 4)//Time out
        {

            lobbyManager.state = Lobby_BaseClass.STATE.ErrorMsg;
            time_mark = Time.time;

            connectingScreen.gameObject.SetActive(false);
            errorScreen.gameObject.SetActive(true);
            lobbyManager.StopClient();
        }

        //Debug.Log("server on: " + NetworkServer.active);
    }
    public void connectServer()
    {
        lobbyManager.networkAddress = input.text;
        if(lobbyManager.networkAddress == "")//Invalid address
        {
            errorScreen.gameObject.SetActive(true);

            lobbyManager.state = Lobby_BaseClass.STATE.ErrorMsg;
            time_mark = Time.time;
        }
        else
        {
            //Hide main menu
            mainMenu.gameObject.SetActive(false);
            connectingScreen.gameObject.SetActive(true);

            lobbyManager.state = Lobby_BaseClass.STATE.Connecting;
            time_mark = Time.time;

            lobbyManager.StartClient();
        }
    }
    
    public void disconnectServer()
    {
        lobbyManager.state = Lobby_BaseClass.STATE.Disconnecting;
        lobbyManager.StopClient();
        lobbyMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        lobbyManager.state = Lobby_BaseClass.STATE.ErrorMsg;
    }

    public void clickReady()
    {
    }

    public void returnMenu()
    {
        lobbyManager.state = Lobby_BaseClass.STATE.Offline;
        time_mark = Time.time;
        mainMenu.gameObject.SetActive(true);
    }
}
