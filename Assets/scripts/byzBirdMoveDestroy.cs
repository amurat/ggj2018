using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class byzBirdMoveDestroy : MonoBehaviour {

	//rules for when the Byzantine Birds move or are destroyed
	public float minSpeed = 5;
	public float maxSpeed = 9;

	float startPosition;
	float randomizeBirdSpeed; 


	// Use this for initialization
	void Start () {
		startPosition = transform.position.x;		//discern whether birds started on left or right
		randomizeBirdSpeed = Random.Range(minSpeed,maxSpeed);	//each instance of a bird should move at it's own speed
	}
	
	// Update is called once per frame
	void Update () {
		if (startPosition < -10) {
			transform.Translate(Vector2.right*randomizeBirdSpeed*Time.deltaTime);
			if (transform.position.x > 20){
				Destroy(this.gameObject);
				FindObjectOfType<byzantineBirdGen> ().DestroyBird(false);//tell the generator that you killed a bird
			}
		}
		else if (startPosition > 10) {
			transform.Translate(Vector2.left*randomizeBirdSpeed*Time.deltaTime);
			if (transform.position.x < -20){
				Destroy (this.gameObject);
				FindObjectOfType<byzantineBirdGen> ().DestroyBird(false);//tell the generator that you killed a bird
			}
		}
	}
}
