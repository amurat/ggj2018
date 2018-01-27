using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class levelLoader : MonoBehaviour {
    /*LEVEL LOADER SCRIPT
     * keeps track of which game mode the player is in, player score, and anything else that needs to transition between every scene
     * as such, it is NOT destroyed at the end of each scene
    */

    //flags to set the different modes. Static to ensure values remain the same between scenes
    static bool storyMode;
    static bool endlessMode;
    static bool galleryMode;
 
    //arrays of minigames for Story mode, based on phase of day
    string[] awakeScenes = { "StopTheClock" };
    string[] morningScenes = { "AvoidColdTile" };
    string[] afternoonScenes = { "WeaveThruTraffic"};
    string[] eveningScenes = { "RainDropDodge" };
    string[] nightScenes = { "BrushTeeth" };
    string sceneToRun;
    int phaseCount;  //goes from 0-4, stands for which of the 5 phases of the day Story mode is in
    static int prevIndex;  //saves index of previous game for Endless mode so repeats shouldn't happen

    public GameObject pauseWall;    //transparent wall to convey the game is paused
    public AudioSource soundSource;
    public Text wealthTrack;

    static int lossCount = 0;  //if lossCount becomes greater than 3, you lose the game
    int prevScore;
    static float scoreKeeper;
    static float wealthTracker;

    float timer;

    // Use this for initialization
	void Start () {
        //Camera.main.aspect = 16 / 9;    //maintain screen aspect ratio at 16:9
    }
	
	// Update is called once per frame
	void Update () {
        immortalScript();   //keeps the object with all this code attached existing between scenes


        //Debugs to check persistency of flags, time, score, etc
        for (int i = 0; i < 1; i++)
        {

            /*  Debug.Log("Current level: " + SceneManager.GetActiveScene().name + ", Index #: " + SceneManager.GetActiveScene().buildIndex);
              Debug.Log("Story mode is " + storyMode);
              Debug.Log("Endless mode is " + endlessMode);
              Debug.Log("Gallery mode is " + galleryMode);
              Debug.Log("Time on level: " + timer);
              Debug.Log("Total losses: " + lossCount);
              Debug.Log("Score is at " + scoreKeeper);
              Debug.Log("Wealth @: " + wealthTracker);
              Debug.Log("Wealth @: " + wealthTracker);

             Debug.Log("Time Scale @ " + Time.timeScale);*/
            //    Debug.Log(randomizer);
        }
        //Screen orientation code. This game should only ever run in landscape view
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        //Screen.orientation = ScreenOrientation.AutoRotation;  NOT SURE IF THIS ENABLES AUTOROTATION TO ALL DIRECTIONS OR NOT

        //basic controls for resetting a level or exiting a game
        resetLevel();
        exitGame();
        homeScreen();

        timer = timer + Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z))//pause button(s) code
        {
            pauseToggle();
        }
        if (Time.timeScale == 0)//if the game is paused, tapping/clicking once should disable pause
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)||(Input.GetKeyDown(KeyCode.Space)))
            {
                pauseToggle();
            }
        }

        if (timer > 10)
        {
            timer = 0;
        }
        if (timer > 3)  //ONLY APPLIES ON THE WIN AND LOSE SCREENS
        {
            if (SceneManager.GetActiveScene().name == "WinScreen")
            {
                this.GetComponent<TextMesh>().text = "Score: " + (int)scoreKeeper + " feelgoods";  //score displayed ONLY on Win/Lose screens

                if (storyMode == true)
                {
                    loadStoryLevel();
                }
                else if (endlessMode == true)
                {
                    loadEndlessLevel();
                }
                if (galleryMode == true)
                {
                    loadGallery();
                }
            }

            if (SceneManager.GetActiveScene().name == "LoseScreen")
            {
                this.GetComponent<TextMesh>().text = "Score: " + (int)scoreKeeper + " feelgoods";

                lossCount++;
                if (lossCount >= 3)
                {
                    wealthTracker += (scoreKeeper / 100);
                    soundSource.Stop();
                    SceneManager.LoadScene(0); //return to title screen after 3 losses
                }
                else if (galleryMode == true){

                        loadGallery();
                    
                }
                else
                {
                    if (storyMode == true)
                    {
                        loadStoryLevel();
                    }
                    else if (endlessMode == true)
                    {
                        loadEndlessLevel();
                    }
                }
                

            }
        }

        if (SceneManager.GetActiveScene().name == "StartScreen")
        {
           // endlessMode = false;
          //  storyMode = false;
         //   galleryMode = false;
            lossCount = 0;
            scoreKeeper = 0;
            wealthTrack.text = "" + (int)wealthTracker; //wealth displayed ONLY on start and store screens

        }
        else if (SceneManager.GetActiveScene().name == "StoreScreen")
        {
            wealthTrack.text = "" + (int)wealthTracker; //wealth displayed ONLY on start and store screens
        }
	}

    public void loadEndlessLevel()  //ENDLESS MODE LEVEL LOADING LOGIC
    {        
        //Randomizer generates a random number to decide each following game in ENDLESS mode
        endlessMode = true;
        int randomizer = Random.Range(5, 9);

        if (prevIndex == randomizer)
        {
            randomizer++;
        }
        Debug.Log("Endless Mode randomizer = " + randomizer);

        prevIndex = randomizer;
        SceneManager.LoadScene(randomizer);
    }

    public void loadStoryLevel()
    {
        //Debug.Log(tellTime);
        storyMode = true;

        if (phaseCount < 1)
        {
            sceneToRun = awakeScenes[Random.Range(0,awakeScenes.Length-1)];
        }
        else if (phaseCount == 1)
        {
            sceneToRun = morningScenes[Random.Range(0, awakeScenes.Length - 1)];
        }
        else if (phaseCount == 2)
        {
            sceneToRun = afternoonScenes[Random.Range(0, awakeScenes.Length - 1)];
        }
        else if (phaseCount == 3)
        {
            sceneToRun = eveningScenes[Random.Range(0, awakeScenes.Length - 1)];
        }
        else if (phaseCount == 4)
        {
            sceneToRun = nightScenes[Random.Range(0, awakeScenes.Length - 1)];
        }

        SceneManager.LoadScene(sceneToRun);

        phaseCount++;  
        if (phaseCount > 4)
        {
            phaseCount = 0;
        }
    }

    public void loadGallery()
    {
        galleryMode = true;
        SceneManager.LoadScene("GalleryMode");
    }

    void immortalScript()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void resetLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void homeScreen()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("StartScreen");
        }
    }

    public void pauseToggle()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            pauseWall.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z+5f);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseWall.transform.position = new Vector3(500f, 500f, 500f);
        }
    }

    public void exitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().name == "WinScreen")
        {
                scoreKeeper = scoreKeeper + (100 * (10 - timer));
        }
        else if (SceneManager.GetActiveScene().name == "LoseScreen")
        {
      
        }
        else if (SceneManager.GetActiveScene().name == "StartScreen")
        {

        }
        else if (SceneManager.GetActiveScene().name == "StoreScreen") { 
        }
        else // for all actual minigames, pause the game on load to give player time to react
        {
            pauseToggle();
        }
        timer = 0;
    }

    public void loadByGameName(string gameName)
    {
        SceneManager.LoadScene(gameName);
    }
}