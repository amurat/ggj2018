using UnityEngine;
using System.Collections;

public class levelFinish : MonoBehaviour {

	public GameObject theExit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other){
		Debug.Log("Exit!");
		Application.LoadLevel (Application.loadedLevel);
	}

}
