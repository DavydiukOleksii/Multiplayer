using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameHelper : NetworkBehaviour {

    #region Data
    public InputField Input;
    public Text TextBlock;

    public Text Id;

    string connectToIP = "127.0.0.1";
    int connectPort = 25001;

    public GameObject connM;
    public GameObject disConnM;


    private PlayerHelper _currentPlayer;
    public PlayerHelper CurrentPlayer
    {
        get
        {
            return _currentPlayer;
        }
        set
        {
            _currentPlayer = value;
        }
    }
    #endregion

    // Use this for initialization
    void Start() { }

    #region Methods
    public void Send()
    {
        CurrentPlayer.CmdSend(Input.text);
    }

    public void StartHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
        connM.SetActive(false);
        disConnM.SetActive(true);
    }

    public void SetPort()
    {
        NetworkManager.singleton.networkPort = connectPort;
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
        connM.SetActive(false);
        disConnM.SetActive(true);
    }

    public void SetIPAddress()
    {
        NetworkManager.singleton.networkAddress = connectToIP;
    }

    public void Disconnect()
    {
        NetworkManager.singleton.StopHost();
        connM.SetActive(true);
        disConnM.SetActive(false);
    }

    public void NetStatus()
    {
        switch (Network.peerType)
        {
            case NetworkPeerType.Server:
                {
                    Debug.Log("Server");
                    break;
                }
            case NetworkPeerType.Disconnected:
                {
                    Debug.Log("Disconnected");
                    break;
                }
            case NetworkPeerType.Client:
                {
                    Debug.Log("Client");
                    break;
                }
            case NetworkPeerType.Connecting:
                {
                    Debug.Log("Connecting");
                    break;
                }
        }
    }

    #endregion
}
