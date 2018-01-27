using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class droppingStuff : MonoBehaviour
{

    //in RainDropDodge, limits are upper Y = 7, lower Y = -7, leftmost X = -10, rightmost X = 10
    public GameObject fallingThings;
    public GameObject timerGUI;
    public GameObject gameInstGUI;
    public GameObject scoreGUI;

    float timer = 10;
    int counter;
    int scoreKeeper;

    // Use this for initialization
    void Start()
    {
       // gameInstGUI.transform.position = new Vector3(75, Screen.height - 25, 0); //Instructions positioned in top left
      //  timerGUI.transform.position = new Vector3(Screen.width - 5, Screen.height - 50, 0); //Timer positioned in top right
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 genPoint = new Vector3(Random.Range(-10f, 10f), Random.Range(7f, 10f), 0f);
        if (Time.timeScale > 0) //IF time is paused, DO NOT INSTANTIATE RAIN
        {
            Instantiate(fallingThings, genPoint, Quaternion.Euler(0, 0, 0));
        }

        timer = timer - Time.deltaTime;
        int countDown = (int)timer;
        Text textTime = timerGUI.GetComponent<Text>();
        textTime.text = countDown.ToString();


        //players score is always displayed in the bottom center of the screen
        scoreGUI.transform.position = new Vector3(Screen.width / 2, 30, 0);
        Text scoreShow = scoreGUI.GetComponent<Text>();
        scoreShow.text = scoreKeeper.ToString();


        if (timer < 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
