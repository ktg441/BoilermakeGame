using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToServer : MonoBehaviour
{
    void Start()
    {
        GetComponent<InitialServerConnect>().SendMessage();
    }
}
