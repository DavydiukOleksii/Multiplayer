using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScr : MonoBehaviour
{
    public static GameObject Instance { get; set; }

    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;

    void Start () {
        Instance = this.gameObject;

        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
	}

    public void ConnectButton()
    {
        mainMenu.SetActive(false);
        connectMenu.SetActive(true);
    }

    public void HostButton()
    {
        mainMenu.SetActive(false);
        serverMenu.SetActive(true);
    }

    public void ConnectToServerButton()
    {

    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
    }
}
