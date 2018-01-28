﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class byzantineBirdGen : MonoBehaviour {
//in Byzantine General scene, limits are upper Y = 2.5, lower Y = 0, left tower X = -11.4, rightmost X = 11.4
    public GameObject birdA;
	public GameObject birdB;
	public GameObject birdC;
	GameObject birdOfChoice;

	public GameObject conMeterLeft;
	public GameObject conMeterRight;

	public float topSide = 2.5f;		//upper Y limit = 2.5
    public float bottomSide = 0;	//lower Y limit = 0
    public float leftSide = -12;		//left tower X = -11.4
    public float rightSide = 12;	//right tower X = 11.4
	
    int counter;
    int scoreKeeper;
	public int curLivingBirds = 0;

	public int maxBirdsSpawnedAtOnce; 

	//score stuff
	int leftConfidenceMeterValue;
	int rightConfidenceMeterValue;
	int defaultConfidenceBoostAmount = 1;
	public int maxConfidenceMeterAmount = 20;
	int totalBirdsDestroyed;

	// lose condition countdown
	public float defeatCountdownDuration = 10;
	private bool defeatCountdownMode = false;
	private float defeatCountdownDeadline;

	private bool gameOver = false;
	
    // Use this for initialization
    void Start()
    {
		StartCoroutine(genBirds());
	}

    // Update is called once per frame
    void Update()
    {
		if (gameOver) {
			return;
		}
		//if there aren't enough birds, spawn a new one each frame until there are
		if (curLivingBirds < maxBirdsSpawnedAtOnce) {
			spawnBird ();
		}
		handleVictoryDefeatCountdown();
    }
	
	private void handleVictoryDefeatCountdown()
	{
		bool leftMeterMaxed = (leftConfidenceMeterValue >= maxConfidenceMeterAmount);
		bool rightMeterMaxed = 	(rightConfidenceMeterValue >= maxConfidenceMeterAmount);
		bool bothMeteresMaxed = leftMeterMaxed && rightMeterMaxed;

		if (!defeatCountdownMode) {
			if (leftMeterMaxed || rightMeterMaxed) 
			{
				Debug.Log ("entering defeat countdown");
				defeatCountdownMode = true;
				float currentTime = Time.time;
				defeatCountdownDeadline = currentTime + defeatCountdownDuration;
			}	
		} 
		
		if (defeatCountdownMode) {
			float currentTime = Time.time;
			float remaining = Mathf.Max(defeatCountdownDeadline - currentTime, 0);
			//Debug.Log ("defeat countdown : " + remaining);
			if (remaining > 0) {
				if (bothMeteresMaxed) 
				{
					OnPlayerDefeat();
					gameOver = true;
				} else {
					OnCountdownMeterExpiring(remaining);					
				}
			} else {
				OnPlayerVictory();
				gameOver = true;
			}
		}
	}

	private void OnCountdownMeterExpiring(float remaining) 
	{
		// handle meter flashing updates here
	}

	private void OnPlayerVictory() 
	{
		// handle victory condition
		Debug.Log ("OnPlayerVictory");
	}

	private void OnPlayerDefeat()
	{
		// handle defeat condition
		Debug.Log ("OnPlayerDefeat");
	}

	IEnumerator genBirds()
    {
		while (curLivingBirds < maxBirdsSpawnedAtOnce)
        {
			spawnBird ();
        }
       yield return new WaitForSeconds(0.9f);
    }

	//spawn birds outside of generation method so we can spawn them from other thems
	public void spawnBird()
	{
		int randomizeBirdSpot = Random.Range(1, 10); //randomize coin flip whether a bird is generated on left or right

		int randomBirdChoice = Random.Range(0,4); //randomly generate 1 of the 3 possible birds each time

		if (randomBirdChoice == 1){
			birdOfChoice = birdA;
		}
		else if (randomBirdChoice == 2){
			birdOfChoice = birdB;
		}
		else if (randomBirdChoice == 3){
			birdOfChoice = birdC;
		}


		if (randomizeBirdSpot < 5) { //half the time, generate a bird from the left

			Vector3 leftGenPoint = new Vector3(Random.Range(leftSide-5, leftSide), Random.Range (bottomSide, topSide), 0f);
			birdOfChoice.GetComponent<SpriteRenderer>().flipX = false;
			Instantiate(birdOfChoice, leftGenPoint, Quaternion.identity);
			curLivingBirds++;
		}
		else if (randomizeBirdSpot >= 5){ //half the time, generate a bird from the right

			Vector3 rightGenPoint = new Vector3(Random.Range(rightSide, rightSide+5), Random.Range (bottomSide, topSide), 0f);
			birdOfChoice.GetComponent<SpriteRenderer>().flipX = true;
			Instantiate(birdOfChoice, rightGenPoint, Quaternion.identity);
			curLivingBirds++;				
		}
		else
		{
			Debug.Log ("randomizeBirdSpot out of bounds, showing X value of "+randomizeBirdSpot);
		}
	}

	//kills a bird off. if it was destroyed by an arrow, handle some scoring
	public void DestroyBird(birdDestroyMethod method){
		curLivingBirds--;
		if (method == birdDestroyMethod.DESTROYED_BY_ARROW) {
			//scoring logic for destroyed by arrow
			totalBirdsDestroyed++;
		} else if (method == birdDestroyMethod.REACHED_RIGHT) {
			rightConfidenceMeterValue += defaultConfidenceBoostAmount;
			conMeterRight.transform.position.x--;
		} else if (method == birdDestroyMethod.REACHED_LEFT) {
			leftConfidenceMeterValue += defaultConfidenceBoostAmount;
			conMeterLeft.transform.position.x++;
		} else {
			Debug.Log ("WHAT KILLED THE BIRD!?");
		}
		Debug.Log ("Total birds destroyed: " + totalBirdsDestroyed + ", right meter: " + rightConfidenceMeterValue + ", left meter: " + leftConfidenceMeterValue);
	}
}

public enum birdDestroyMethod{
	DESTROYED_BY_ARROW = 0,
	REACHED_LEFT = 1,
	REACHED_RIGHT = 2
}

