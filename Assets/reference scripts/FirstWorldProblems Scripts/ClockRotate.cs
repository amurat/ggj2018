using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClockRotate : MonoBehaviour {

    public Rigidbody minuteHand;
    public float forceAmt = 10f;
    bool win = false;
    float timer = 10;
    float timeDelay;
    public GameObject timerGUI;
    public GameObject gameInstGUI;
    public GameObject scoreGUI;

	// Use this for initialization
	void Start () {
   //     timerGUI.transform.position = new Vector3(Screen.width, Screen.height-40, 0); //Timer positioned in top right

	}
	
	// Update is called once per frame
	void Update () {

        timer = timer-Time.deltaTime;
        int countDown = (int)timer;
        Text textTime = timerGUI.GetComponent<Text>();
        textTime.text = countDown.ToString();

        if (Time.timeScale == 0)    //if the game is paused
        {
            timeDelay = timer;  //save the time at which it was paused
        }
        if (Time.timeScale > 0)     //if it's unpaused
        {
            if (timeDelay > timer + 1)  //wait a second
            {
                stopClock();    //allow touching to stop clock rotation
            }
        }
	}

    void stopClock() {

            if ((Input.GetMouseButtonDown(0)) || (Input.GetKeyDown("space")))
            {
                minuteHand.freezeRotation = true;               //fully stops the minute hand

                if (win == true)
                {
                    SceneManager.LoadScene(1);
                }
                else if (timer < 1)
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(2);
                }
            }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "winning"))
        {
            win = true;
            //Debug.Log("Am I winning?" + win);

        }
    }

    void OnTriggerExit(Collider other)
    {
        win = false;
        //Debug.Log("Am I winning?" + win);
    }
}
