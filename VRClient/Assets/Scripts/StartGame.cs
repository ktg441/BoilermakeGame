using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    private AudioSource flick = null;
    private GameObject lamp = null;

    public Text staticText = null;
    public Text timer = null;

    public float gameTime = 300f;
    private float min;
    private float sec;

    private bool startTimer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game started");
        startTimer = false;
        min = 0f;
        sec = 0f;
        staticText.enabled = false;
        timer.enabled = false;
        flick = GetComponent<AudioSource>();
        flick.time = flick.clip.length * .25f;
        lamp = GameObject.Find("Lamp");
        lamp.GetComponent<Light>().intensity = 0;
        Invoke("TurnOn", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            gameTime -= Time.deltaTime;
            min = Mathf.Floor(gameTime / 60);
            sec = gameTime % 60;
            timer.text = string.Format("{0:0}:{1:00}", min, sec);

            if (gameTime == 0f)
            {
                startTimer = false;
                //win condition
            }
        }
        
    }

    void TurnOn()
    {
        Debug.Log("Light turned on");
        lamp.GetComponent<Light>().intensity = 5;
        flick.Play();
        staticText.enabled = true;
        timer.enabled = true;
        startTimer = true;
    }
}
