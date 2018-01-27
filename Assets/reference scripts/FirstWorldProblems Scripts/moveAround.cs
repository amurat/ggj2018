using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class moveAround : MonoBehaviour {

	public GameObject player;
    public GameObject slower;
    int slowCounter = 0; 
    int origVelocity = 1;
    float velocity = 1;

    //DIFFERENT CONTROL SCHEMES should be separated, using #if UNITY_STANDALONE,UNITY_WEBPLAYER,UNITY_ANDROID,etc
    //for now, everything is running at once
    //Touchscreen code 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		movePlayer ();
        if (slowCounter >= 8)
        {
            SceneManager.LoadScene(2); //touch 5 or more cold tiles, you lose
        }
        
	}

	void movePlayer(){

        //BASIC KEYBOARD PC CONTROLS 
		if ((Input.GetKey (KeyCode.LeftArrow)) || (Input.GetKey (KeyCode.A)) ){	
			transform.position += new Vector3(-(velocity), 0f, 0f) * 10*Time.deltaTime;
		}
        if ((Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.D)) ){
			transform.position += new Vector3(velocity, 0f, 0f) * 10*Time.deltaTime;
		}
        if ((Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.S)) ){
			transform.position += new Vector3(0,-velocity,0) * 10*Time.deltaTime;
		}
        if ((Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.W)) ){	
			transform.position += new Vector3(0f, velocity, 0f) * 10*Time.deltaTime;
		}
        //PC MOUSE-BASED & TOUCHSCREEN CONTRLS
        Vector3 targetPoint;

        if (Input.GetMouseButton(0))
        {
            //PC moves towards touch/mouse point if not on it, otherwise operates on drag mechanics
            targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPoint.z = transform.position.z;



           // targetPoint.z = player.transform.position.z;


            transform.position = Vector3.MoveTowards(transform.position, targetPoint, 100f);
        }
        //TOUCHSCREEN CONTROLS 

        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            targetPoint = Camera.main.ScreenToWorldPoint(myTouch.position);
            targetPoint.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, targetPoint, 100f);
        }

        /* NO response, not sure why 
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].position.x > transform.position.x)
            {
                transform.position += new Vector3(velocity, 0f, 0f) * 10 * Time.deltaTime;
            }
            if (Input.touches[0].position.x < transform.position.x)
            {
                transform.position += new Vector3(-(velocity), 0f, 0f) * 10 * Time.deltaTime;
            }
            if (Input.touches[0].position.y > transform.position.y)
            {
                transform.position += new Vector3(0f, velocity, 0f) * 10 * Time.deltaTime;
            }
            if (Input.touches[0].position.y < transform.position.y)
            {
                transform.position += new Vector3(0f, -(velocity), 0f) * 10 * Time.deltaTime;
            }
        }
         
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            transform.position = Input.GetTouch(0).position;

            if (Input.GetTouch(0).position.x != player.transform.position.x)
            {
                if (Input.GetTouch(0).position.y != player.transform.position.y)
                {

                }

            }  
        }*/
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "winning")
        {
            SceneManager.LoadScene(1);
        }
        else if (other.gameObject.tag == "bad")
        {
            slowCounter++;
            velocity = velocity / 2;
            Debug.Log("I've touched " + slowCounter + " cold tiles");
        }
    }

    void OnTriggerExit(Collider other)
    {
        velocity = origVelocity;
    }
}
