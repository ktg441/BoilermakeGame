using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class Parse : MonoBehaviour
{

    public GameObject ButtonA;
    public GameObject ButtonB;
    public GameObject ButtonC;
    public GameObject ButtonD;

    private GameObject mainCam;

    private string[] options = { "Red Button", "Green Button", "Blue Button", "Yellow Button" };

    public string newMessage = "";

    private void Start()
    {
        mainCam = GameObject.Find("Main Camera");
    }

    private void FixedUpdate()
    {
        if (string.Compare(newMessage, "") != 0)
        {
            Debug.Log("New message is " + newMessage + " \nLength: " + newMessage.Length.ToString());
            string toParse = newMessage;
            ParseData(toParse);
            newMessage = "";
        }
    }

    public void ParseData(string msg)
    {
        Debug.Log("TRYING TO PARSE " + msg);
        string[] split = msg.Split(' ');
        int counter = 0;
        foreach (string e in split)
        {
            //Debug.Log("Current e: " + e);
            string[] determine = e.Split(':');
            //Debug.Log(determine[0]);
            //Debug.Log(determine[1]);
            string action = determine[1];
            string[] hex = determine[0].Split(',');
            switch (counter)
            {
                case 0:
                    ButtonA.GetComponent<Image>().color = new Color(float.Parse(hex[0]), float.Parse(hex[1]), float.Parse(hex[2]));
                    ButtonA.transform.Find("Text").GetComponent<Text>().text = options[System.Int32.Parse(action.Substring(action.Length-1))];
                    break;
                case 1:
                    ButtonB.GetComponent<Image>().color = new Color(float.Parse(hex[0]), float.Parse(hex[1]), float.Parse(hex[2]));
                    ButtonB.transform.Find("Text").GetComponent<Text>().text = options[System.Int32.Parse(action.Substring(action.Length - 1))];
                    break;
                case 2:
                    ButtonC.GetComponent<Image>().color = new Color(float.Parse(hex[0]), float.Parse(hex[1]), float.Parse(hex[2]));
                    ButtonC.transform.Find("Text").GetComponent<Text>().text = options[System.Int32.Parse(action.Substring(action.Length - 1))];
                    break;
                case 3:
                    ButtonD.GetComponent<Image>().color = new Color(float.Parse(hex[0]), float.Parse(hex[1]), float.Parse(hex[2]));
                    ButtonD.transform.Find("Text").GetComponent<Text>().text = options[System.Int32.Parse(action.Substring(action.Length - 1))];
                    break;
            }

            counter++;
        }
    }
}
