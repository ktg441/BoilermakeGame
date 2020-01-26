using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }

    public float pressLength;
    public bool pressed;
    public ButtonEvent downEvent;

    Vector3 startPos;
    Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // If our distance is greater than what we specified as a press
        // set it to our max distance and register a press if we haven't already
        float distance = Mathf.Abs(transform.position.x - startPos.x);
        if (distance >= pressLength)
        {
            //Debug.Log("Actively pressing");
            // Prevent the button from going past the pressLength
            transform.position = new Vector3(transform.position.x, startPos.y - pressLength, transform.position.z);
            if (!pressed)
            {
                pressed = true;
                // If we have an event, invoke it
                PushEvent();
            }
        }
        else
        {
            // If we aren't all the way down, reset our press
            pressed = false;
        }
        // Prevent button from springing back up past its original position
        if (transform.position.x > startPos.x)
        {
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }
    }

    public void PushEvent()
    {
        Debug.Log("Button pressed");
        GetComponent<AudioSource>().Play();
        GameObject.Find("OVRPlayerController").GetComponent<InitialServerConnect>().SendMessage();
        Debug.Log("Message sent from button");
    }
}