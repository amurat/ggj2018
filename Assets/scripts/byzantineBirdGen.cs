using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class byzantineBirdGen : MonoBehaviour {
//in Byzantine General scene, limits are upper Y = 2.5, lower Y = 0, left tower X = -11.4, rightmost X = 11.4
    public GameObject birdFromLeft;
	public GameObject birdFromRight;
	
	public float topSide = 2.5f;		//upper Y limit = 2.5
    public float bottomSide = 0;	//lower Y limit = 0
    public float leftSide = -12;		//left tower X = -11.4
    public float rightSide = 12;	//right tower X = 11.4
	
    int counter;
    int scoreKeeper;

    // Use this for initialization
    void Start()
    {
		StartCoroutine(genBirds());
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	
	IEnumerator genBirds()
    {
        int objSoFar = 0;
        int randomLimit = Random.Range(10, 20); // make parameter of class tunable


        Debug.Log(randomLimit);
        while (objSoFar < randomLimit)
        {
            int randomizeBird = Random.Range(1, 10); //randomize coin flip whether a bird is generated on left or right
            
			if (randomizeBird < 6) { //half the time, generate a bird from the left
				Vector3 leftGenPoint = new Vector3(Random.Range(leftSide-5, leftSide), Random.Range (bottomSide, topSide), 0f);
				Instantiate(birdFromLeft, leftGenPoint, Quaternion.identity);
				objSoFar++;
			}
			else if (randomizeBird >= 5){ //half the time, generate a bird from the right
				Vector3 rightGenPoint = new Vector3(Random.Range(rightSide, rightSide+5), Random.Range (bottomSide, topSide), 0f);
				Instantiate(birdFromRight, rightGenPoint, Quaternion.identity);
				objSoFar++;				
			}
			else
			{
				Debug.Log ("randomizeBird value out of bounds. Current value = "+randomizeBird);
			}
			
        }
       yield return new WaitForSeconds(0.9f);

    }
}
