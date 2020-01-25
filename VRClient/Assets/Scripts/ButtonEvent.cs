using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{

    public void OnPush()
    {
        Debug.Log("Being pushed");
        GameObject.Find("Lamp").GetComponent<Light>().intensity = 100;
        GameObject.Find("ControlButton").GetComponent<AudioSource>().Play();
    }

}
