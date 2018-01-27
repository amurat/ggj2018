using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tileGenerator : MonoBehaviour
{
    public GameObject timerGUI;
    public GameObject gameInstGUI;
    public GameObject scoreGUI;

    public GameObject goodTile;     //white tile that is safe
    public GameObject coldTile;     //blue tile that is cold
    public GameObject winTile;      //the end of the level

    float timer = 10;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(genTiles());
        //gameInstGUI.transform.position = new Vector3(100, Screen.height - 25, 0); //Instructions positioned in top left
       // timerGUI.transform.position = new Vector3(Screen.width - 30, Screen.height - 40, 0); //Timer positioned in top right

    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        int countDown = (int)timer;
        Text textTime = timerGUI.GetComponent<Text>();
        textTime.text = countDown.ToString();


        //players score is always displayed in the bottom center of the screen
        scoreGUI.transform.position = new Vector3(Screen.width / 2, 30, 0);
        Text scoreShow = scoreGUI.GetComponent<Text>();
      //  scoreShow.text = scoreKeeper.ToString();


        if (timer < 1)
        {
            SceneManager.LoadScene(2);
        }
    }

    IEnumerator genTiles()
    {
        float tileDimension =  winTile.GetComponent<Collider>().bounds.size.x;
  //      float sceneW = 21;
  //      float sceneH = 11;

        float totalTiles = 220;       //the camera view dimensions are roughly 20x10
        float counter = 0;
        float xPosTile = -10;        //left-side limit of camera view 
        float originalTileXPos = xPosTile; //saves the original left-side limit
        float yPosTile = 5;         //top-side limit of camera view 
        while (counter < totalTiles)   
        {
            counter++;
            int randomizer = Random.Range(0, 3);
           // Debug.Log(randomizer);
            Vector3 generationPoint = new Vector3(xPosTile, yPosTile, 0f);  
            if (randomizer < 2)     //50% of the time, make good tiles
            {
                Instantiate(goodTile, generationPoint, Quaternion.Euler(0, 0, 0));
            }
            else                    //The rest of the time, make a cold tile
            {
                Instantiate(coldTile, generationPoint, Quaternion.Euler(0, 0, 0));
            }
            xPosTile += tileDimension;          //Once a tile is generated, make the next one spawn to its right
            if (xPosTile > 11)   //When top row is filled (trying to generate an X past screen limit), start again one row lower from the far left 
            {
                yPosTile -= tileDimension;
                xPosTile = originalTileXPos;
            }
            if (counter == totalTiles)      //The very last tile is the tile you are trying to reach
            {
                Instantiate(winTile, generationPoint, Quaternion.Euler(0, 0, 0));
            }
        }
        yield return new WaitForSeconds(0.1f);
    }
}
