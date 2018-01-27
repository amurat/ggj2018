using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VR;

//put this on an OBJECT that is to be looked at 
public class RaycastGaze : MonoBehaviour {

    public GameObject player;
    bool amIBeingLookedAt = false;
    float timer;
    Vector3 posStart = new Vector3(0f, 3.581f, 0f); //player position at start screen
    Vector3 posSceneA = new Vector3(-14.344f, 4.77f, 50.135f); //player position in childhood bed
    Vector3 posSceneB = new Vector3(4.2f, 2f, -79.5f); //player position in classroom as student
    Vector3 posSceneC_A = new Vector3(-370.8f, 2f, -20.5f); //player position at stadium


	// Use this for initialization
	void Start () {
        player.transform.position = posStart;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //constructing a Ray before we fire a Raycast
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit rayHitInfo = new RaycastHit();    //setting up a blank variable to know where we hit 
        Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red);

        Debug.Log(timer);
        if (Physics.Raycast(ray, out rayHitInfo, 50f) && rayHitInfo.collider == GetComponent<Collider>())
        {
            Debug.Log("I see a: "+rayHitInfo.collider.gameObject.name);
            OnLooking();
            if (amIBeingLookedAt == false)
            {
                OnStartLook();
                amIBeingLookedAt = true;
            }
        }
        else
        {
            OnNotLooking();
            if (amIBeingLookedAt == true)
            {
                OnStopLook();
                amIBeingLookedAt = false;
            }
        }


        if (rayHitInfo.collider.gameObject.name == "startObj")
        {
            if (timer > 1)
            {
                player.transform.position = posSceneA;
                timer = 0;
            }
        }
        if (rayHitInfo.collider.gameObject.name == "howObj")
        {
            if (timer > 1)
            {
                rayHitInfo.collider.gameObject.GetComponent<TextMesh>().text = "Roy is a choose your own adventure game, \n controlled solely by what you look at. \n Look for things that <color=lime>glow green</color>";
            }
        }
        if (player.transform.position == posSceneA) //the first scene (Roy in bed) has no interactions. Ends after 15 seconds
        {
            timer = timer + Time.deltaTime;
            if (timer > 15)
            {
                player.transform.position = posSceneB;
            }
        }
        if (player.transform.position == posSceneB) //the second scene (Roy at school) has different paths based on where the player is looking
        {
            timer = timer + Time.deltaTime;

            if (rayHitInfo.collider.gameObject.name == "lookHereFootball")
            {
                if (timer > 23)
                {
                    player.transform.position = posSceneC_A;
                }
            }
        }
   //     if (player.transform.position == posSceneC_A) //the original third scene (Roy playing football) has Roy running along the X axis automatically
   //     { 
            if (player.transform.position.x < -337.39f)
            {
                player.transform.position = new Vector3(player.transform.position.x + 0.1f, player.transform.position.y, player.transform.position.z);
            }
            else
            {
                //stops player movement along x axis once they reach the goal 
            }
   //     }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(0);
        }

	}

    void OnStartLook()  //happens the first instant that an object is being looked at 
    {
        //Debug.Log("User STARTED looking");
    }

    void OnStopLook()    //happen the instant the object is no longer being looked at 
    {
        //Debug.Log("User STOPPED looking");

    }

    void OnLooking()    //should happen every Update, as long as the thing is being looked at by a raycast
    {
        timer = timer + Time.deltaTime;
    }

    void OnNotLooking() //should happen every Update while thing is NOT being looked at by raycast
    {
       
    }
}

