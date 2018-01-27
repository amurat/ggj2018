using UnityEngine;
using System.Collections;

public class movePlayers : MonoBehaviour {

    public GameObject p1;
    public GameObject p2;
    public GameObject grumpyBack;
    public GameObject dolans;
    public GameObject trolls;
    public Transform p1pos;
    public Transform p2pos;

    public float waitTime = 0;
    float p1Speed = 2;
    float p2Speed = 2;
    private Vector3 velocity = Vector3.zero;

    private Vector3 point;
    private Vector3 destination;
    private Vector3 delta;

    public Animation runningDoge;
    public Animation runningNyan;

    private Animator animatic;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //CONTROLS FOR PLAYER 1 (WASD)

            if (Input.GetKey(KeyCode.A))    //P1 left
            {
                p1.transform.position += new Vector3(0-p1Speed, 0f, 0f) * 10 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))    //P1 right
            {
                p1.transform.position += new Vector3(p1Speed, 0f, 0f) * 10 * Time.deltaTime;
                //Animate Doge runing
            }
            if (Input.GetKey(KeyCode.S))    //P1 down
            {
                p1.transform.position += new Vector3(0, p1Speed/-5, 0f) * 10 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))    //P1 up
            {
                p1.transform.position += new Vector3(0f, p1Speed, 0f) * 10 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Alpha0))   //Disable P1's controls 
            {
                p1.transform.position += new Vector3(0-p1Speed, 0f, 0f) * 10 * Time.deltaTime;
            }
            

        //CONTROLS FOR PLAYER 2 (UpLeftDownRight)

            if (Input.GetKey(KeyCode.LeftArrow))    //P2 left
            {
                p2.transform.position += new Vector3(0-p2Speed, 0f, 0f) * 10 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))   //P2 right
            {
                p2.transform.position += new Vector3(p2Speed, 0f, 0f) * 10 * Time.deltaTime;
                //Animate Nyan running

            }
            if (Input.GetKey(KeyCode.DownArrow))   //P2 down
            {
                p2.transform.position += new Vector3(0, p2Speed / -5, 0f) * 10 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow))      //P2 right
            {
                p2.transform.position += new Vector3(0f, p2Speed, 0f) * 10 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Alpha1))       //Disable P2's controls
            {
                p2.transform.position += new Vector3(0-p2Speed, 0f, 0f) * 10 * Time.deltaTime;
            }

            //Camera position should automatically follow the leading player
            if (p1pos.position.x > p2pos.position.x)
            {
                point = GetComponent<Camera>().WorldToViewportPoint(p1pos.position);
                delta = p1pos.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, waitTime);
            }
            else if (p1pos.position.x < p2pos.position.x)
            {
                point = GetComponent<Camera>().WorldToViewportPoint(p2pos.position);
                delta = p2pos.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, waitTime);
            }

          
        //Reset the level
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player1")
        {
            Debug.Log("P1 has entered");
            p1Speed = p1Speed / 2;
        }
        else if (other.gameObject.name == "player2"){
            Debug.Log("P2 has entered");
            p2Speed = p2Speed / 2;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "player1")
        {
            Debug.Log("P1 has exited");
            p1Speed *= 2;
        }
        else if (other.gameObject.name == "player2")
        {
            Debug.Log("P2 has exited");
            p2Speed *= 2;
        }
    }
}