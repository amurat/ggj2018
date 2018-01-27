using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class destroyThings : MonoBehaviour {
    public GameObject timerGUI;
    public GameObject gameInstGUI;
    public GameObject scoreGUI;

    int destroyCount = 0;
    float timer = 10;

	// Use this for initialization
	void Start () {
        //gameInstGUI.transform.position = new Vector3(100, Screen.height - 25, 0); //Instructions positioned in top left
       // timerGUI.transform.position = new Vector3(Screen.width - 30, Screen.height - 40, 0); //Timer positioned in top right
	}
	
	// Update is called once per frame
	void Update () {
        timer = timer - Time.deltaTime;
        int countDown = (int)timer;
        Text textTime = timerGUI.GetComponent<Text>();
        textTime.text = countDown.ToString();


        if (destroyCount >= 8)
        {
            SceneManager.LoadScene(1);
        }
        else if (timer < 1) {
            SceneManager.LoadScene(2);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destroy")
        {
            Destroy(other.gameObject);
            destroyCount++;
        }
        Debug.Log(destroyCount + " objects destroyed");
    }
}
