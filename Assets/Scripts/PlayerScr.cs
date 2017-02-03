using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerScr : MonoBehaviour {

    public GameObject number;

    public void AddClick()
    {
        number.GetComponent<Text>().text = (int.Parse(number.GetComponent<Text>().text) + 1).ToString();
    }

    public void SubClick()
    {
        number.GetComponent<Text>().text = (int.Parse(number.GetComponent<Text>().text) - 1).ToString();
    }
}
