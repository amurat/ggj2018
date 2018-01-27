using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class collideBad : MonoBehaviour {
    public GameObject timerGUI;
    public GameObject gameInstGUI;
    public GameObject scoreGUI;
    float timer = 10;

	// Use this for initialization
	void Start () {
        //gameInstGUI.transform.position = new Vector3(100, Screen.height - 25, 0); //Instructions positioned in top left
        //timerGUI.transform.position = new Vector3(Screen.width - 30, Screen.height - 50, 0); //Timer positioned in top right

	}
	
	// Update is called once per frame
	void Update () {
        timer = timer - Time.deltaTime;
        int countDown = (int)timer;
        Text textTime = timerGUI.GetComponent<Text>();
        textTime.text = countDown.ToString();


	}

    void OnCollisionEnter()
    {
        SceneManager.LoadScene(2);  //if a car collides with any solid object, you lose
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered a trigger");
        if (other.gameObject.tag == "winning")
        {
            SceneManager.LoadScene(1);      //if the car enters the win zone, you win
        }
    }
}
