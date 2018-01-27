using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class moveLeftRightOnly : MonoBehaviour {

	public GameObject player;
    public int velocity;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		movePlayer ();
       // moveWithTouch();
	}

	void movePlayer(){
        if ((Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.A)) /*|| (Input.GetTouch(0).position.x < player.transform.position.x)*/){	
			transform.position += new Vector3(-1f, 0f, 0f) * 10*Time.deltaTime;
		}
		if ((Input.GetKey (KeyCode.RightArrow)) || (Input.GetKey (KeyCode.D))/*|| (Input.GetTouch(0).position.x > player.transform.position.x)*/){
			transform.position += new Vector3(1f, 0f, 0f) * 10*Time.deltaTime;
		}
        Vector3 targetPoint;


        if (Input.GetMouseButton(0))
        {
            targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPoint.y = transform.position.y;
            targetPoint.z = player.transform.position.z;
               
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, 100f);

            if (Input.mousePosition.x > transform.position.x)
            {
                transform.position += new Vector3(velocity, 0f, 0f) * 10 * Time.deltaTime;
            }
            if (Input.mousePosition.x < transform.position.x)
            {
                transform.position += new Vector3(-(velocity), 0f, 0f) * 10 * Time.deltaTime;
            }
        }
	}
    
    void moveWithTouch()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if(touch.position.x > player.transform.position.x){
                    if (touch.phase != TouchPhase.Ended)
                    {
                        transform.position += new Vector3(velocity, 0f, 0f) * 10 * Time.deltaTime;
                    }
                    else
                    {
                    }   

                }
                if(touch.position.x < player.transform.position.x){
                    if (touch.phase != TouchPhase.Ended)
                    {
                        transform.position += new Vector3(-(velocity), 0f, 0f) * 10 * Time.deltaTime;
                    }
                    else
                    {
                    }   
                }
            }
        }
    }
}
