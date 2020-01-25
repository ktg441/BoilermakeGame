using UnityEngine;

public class StartGame : MonoBehaviour
{

    private AudioSource flick = null;
    private GameObject lamp = null;

    // Start is called before the first frame update
    void Start()
    {
        flick = GetComponent<AudioSource>();
        flick.time = flick.clip.length * .25f;
        lamp = GameObject.Find("Lamp");
        Invoke("TurnOn", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnOn()
    {
        lamp.GetComponent<Light>().intensity = 5;
        flick.Play();
    }
}
