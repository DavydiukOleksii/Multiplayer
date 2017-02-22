using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHelper : NetworkBehaviour {

    GameHelper _gameHelper;
 

	// Use this for initialization
	void Start () {
        _gameHelper = GameObject.FindObjectOfType<GameHelper>();
        

        if (isLocalPlayer)
        {
            _gameHelper.CurrentPlayer = this;
        }
    }   

    // Update is called once per frame
    void Update()
    {
       
    }

    [Command]
    public void CmdSend(string mess)
    {
        Debug.Log("Send = " + mess);
        RpcSend(netId +": " + mess);
    }

    [ClientRpc]
    public void RpcSend(string message)
    {
        if(!Network.isServer)
        _gameHelper.TextBlock.text += System.Environment.NewLine + message;
    }
}
