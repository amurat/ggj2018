using UnityEngine;
using System.Collections;

public class byzMoveMain : MonoBehaviour {
	public float velocity = 10;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//movement controls for left, right, and jumping
		if ((Input.GetKey (KeyCode.LeftArrow))){	
			transform.position += new Vector3(-1f, 0f, 0f) * velocity *Time.deltaTime;
		}
		if ((Input.GetKey (KeyCode.RightArrow))){
			transform.position += new Vector3(1f, 0f, 0f) * velocity *Time.deltaTime;
		}
		//reset the level
		if (Input.GetKeyDown (KeyCode.R)){		
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
