using UnityEngine;
using System.Collections;

public class generateObjectsCustom : MonoBehaviour {

	public GameObject platform;		//the basic platforms that will be procedurally generated each time the level is loaded
	public Transform theExit;		//level ending, location is procedurally decided
	public GameObject topSide;		//all these are assigned in inspector, values are then referenced to generate procedural generation constraints
	public GameObject bottomSide;	//Setting up these walls is currently static, but may end up being procedurally generated in a later update
	public GameObject leftSide;
	public GameObject rightSide;

	// Use this for initialization
	void Start () {
		StartCoroutine (genObstacles());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator genObstacles(){
		float limitTop = topSide.transform.position.y;			//gets the Y position of the ceiling
		float limitBottom = bottomSide.transform.position.y;	//gets the Y position of the floor
		float limitLeft = leftSide.transform.position.x;		//gets the X position of the left wall
		float limitRight = rightSide.transform.position.x;		//gets the X position of the right wall
		int objSoFar = 0;
		int randomLimit = Random.Range(10, 30);

		Debug.Log(randomLimit);
		while (objSoFar < randomLimit){
			float randomRotation = Random.Range(0, 90);
            if(randomRotation > 45){
                randomRotation = 90;
            }
            else if (randomRotation < 45)
            {
                randomRotation = 0;
            }
            Vector3 proceduralGenPoint = new Vector3(Random.Range(limitLeft + 15, limitRight - 15), Random.Range(limitBottom + 10, limitTop - 15), 0f);
            Instantiate(platform, proceduralGenPoint, Quaternion.Euler(0, 0, randomRotation));
			objSoFar++;
		}
		Vector3 proceduralExitPoint = new Vector3(Random.Range (limitLeft+15, limitRight-15), Random.Range (limitTop-20, limitTop-15), 0f);
		Instantiate (theExit, proceduralExitPoint, Quaternion.Euler (0,0,0));
		yield return new WaitForSeconds(0.1f);

	}
}
