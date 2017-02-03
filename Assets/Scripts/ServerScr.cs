using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.IO;

public class ServerScr : MonoBehaviour
{
    public int port = 6232;

    private List<ServerClient> clients;
    private List<ServerClient> disconnectList;

    private TcpListener server;
    private bool serverStarted;

    //start server
    public void Init()
    {
        DontDestroyOnLoad(gameObject);

        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();

        try
        {
            //create tcplistener(our server), any ip can conect to this server by the port
            server = new TcpListener(IPAddress.Any, port);  
            server.Start();

            StartListening();
            serverStarted = true;
        }
        catch(Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    public void Update()
    {
        if (!serverStarted)
            return;

        foreach (ServerClient client in clients)
        {
            //Is the client still connected?
            if (!IsConnected(client.tcp))
            {
                client.tcp.Close();
                disconnectList.Add(client);
                continue;
            }
            else
            {
                NetworkStream s = client.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)
                    {
                        // 
                        OnIncomingData(client, data);
                    }
                }
            }
        }

        //remove disconnected client
        for (int i = 0; i < disconnectList.Count; i++)
        {
            //tell our player somebody disconnected

            clients.Remove(disconnectList[i]);
            disconnectList.RemoveAt(i);
        }
        
    }

    //find a clients which want to connect to server
    public void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server); 
    } 

    //create client for server add save it client
    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(ar));
        clients.Add(sc);

        StartListening();

        Debug.Log("Somebody connected/.");
    }

    //cheack connection between server and client
    private bool IsConnected(TcpClient client)
    {
        try
        {
            if (client != null && client.Client != null && client.Client.Connected)
            {
                if (client.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(client.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }
            }
            else
            {
                return false;
            }
        }
        catch { }
        return false;
    }

    //read from server
    private void OnIncomingData(ServerClient c, string d)
    {
        Debug.Log("Client: " + c.ClientName + ". Send a message: " + d + ".");
    }

    //send message from server
    private void Broadcast(string data, List<ServerClient> sc)
    {
        foreach (ServerClient s in sc)
        {
            try
            {
                StreamWriter writen = new StreamWriter(s.tcp.GetStream());
                writen.WriteLine(data);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}

public class ServerClient
{
    public string ClientName;
    public TcpClient tcp;

    public ServerClient(TcpClient tcp)
    {
        this.tcp = tcp;
    }
}
