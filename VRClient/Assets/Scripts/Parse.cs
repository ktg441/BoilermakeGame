using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parse : MonoBehaviour
{

    public string newMessage = "";

    private int numCorrect = 0;

    public Text display;

    public Text staticText;
    public Text timer;

    // Start is called before the first frame update
    void Start()
    {
        display.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
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
        string[] split = msg.Split(':');
        string[] correct = split[0].Split(',');
        string incorrect = split[1];
        display.color = new Color(float.Parse(correct[0]), float.Parse(correct[1]), float.Parse(correct[2]));
        if(string.Compare(incorrect, "0,255,255") == 0)
        {
            display.text = "Blue";
        }
        else if (string.Compare(incorrect, "255,165,0") == 0)
        {
            display.text = "Orange";
        }
        else if (string.Compare(incorrect, "0,255,0") == 0)
        {
            display.text = "Green";
        }
        else if (string.Compare(incorrect, "255,0,0") == 0)
        {
            display.text = "Red";
        }
    }

    public void incrementWinCount()
    {
        numCorrect++;
        if (numCorrect == 3)
        {
            GetComponent<AudioSource>().Play();
            timer.text = "";
            staticText.text = "Congratulations!\nYou have upgraded your computer to Linux!";
        }
    }

    public void resetWinCount()
    {
        numCorrect = 0;
    }
}
