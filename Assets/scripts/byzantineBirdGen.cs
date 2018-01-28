using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class byzantineBirdGen : MonoBehaviour {
    public GameObject birdA;
	public GameObject birdB;
	public GameObject birdC;
	GameObject birdOfChoice;

	public GameObject conMeterLeft;			
	public GameObject conMeterRight;	
	public GameObject scoreTracker;
	public GameObject messageTracker;

	//in Byzantine General scene, limits are upper Y = 2.5, lower Y = 0, left tower X = -11.4, rightmost X = 11.4
	public float topSide = 2.5f;		//upper Y limit = 2.5
    public float bottomSide = 0;	//lower Y limit = 0
    public float leftSide = -12;		//left tower X = -11.4
    public float rightSide = 12;	//right tower X = 11.4
	
    int counter;
	public int curLivingBirds = 0;
	public int maxBirdsSpawnedAtOnce; 

	//score stuff
	float leftConfidenceMeterValue;
	float rightConfidenceMeterValue;
	const float STARTING_CONFIDENCE = 20f;
	float defaultConfidenceBoostAmount = 1f;
	const float MAX_CONFIDENCE_METER_AMOUNT = 40f;
	float leftConfidenceMeterStartingXPosition = -22.09f;
	float leftConfidenceMeterEndingXPosition = -13.09f;
	float rightConfidenceMeterStartingXPosition = 22.21f;
	float rightConfidenceMeterEndingXPosition = 13.2f;
	float confidenceMeterLength = 9f;
	float RETREAT_DELAY_AMOUNT = -15f;
	float prevLeftMeterVal;
	float prevRightMeterVal;
	float confidenceDecrementVar = 1;

	int totalBirdsDestroyed;
	public int score;
	const int SCORE_FOR_DESTROYING_BIRD_NORMAL = 20;

	// lose condition countdown
	public float defeatCountdownDuration = 10;
	private bool defeatCountdownMode = false;
	private float defeatCountdownDeadline;

	private bool gameOver = false;

	private string endGameMessage;
	private int endGameBonusScore;
	private string winLoseMessage;
	
    // Use this for initialization
    void Start()
    {
		StartCoroutine(genBirds());

		for (int i = 0; i <= maxBirdsSpawnedAtOnce; i++) {			
			spawnBird ();
		}

		leftConfidenceMeterValue = STARTING_CONFIDENCE;
		rightConfidenceMeterValue = STARTING_CONFIDENCE;
		//set starting for conmeters
		updateConMeters();
		prevLeftMeterVal = leftConfidenceMeterValue;
		prevRightMeterVal = rightConfidenceMeterValue;

	}

	void updateConMeters(){
		float confidenceMeterModifier;
		confidenceMeterModifier = (leftConfidenceMeterValue * confidenceMeterLength) / MAX_CONFIDENCE_METER_AMOUNT;
		conMeterLeft.transform.localPosition= new Vector3(leftConfidenceMeterStartingXPosition + confidenceMeterModifier,  conMeterLeft.transform.localPosition.y, 1f);
		confidenceMeterModifier = (rightConfidenceMeterValue * confidenceMeterLength) / MAX_CONFIDENCE_METER_AMOUNT;
		conMeterRight.transform.localPosition= new Vector3(rightConfidenceMeterStartingXPosition - confidenceMeterModifier,  conMeterRight.transform.localPosition.y, 1f);
	}


    // Update is called once per frame
    void Update()    {

		//
		if (leftConfidenceMeterValue <= 0 && rightConfidenceMeterValue <= 0) {
			OnPlayerVictory (true);
		}

		//decerement them over time
		leftConfidenceMeterValue -= confidenceDecrementVar*Time.deltaTime;
		rightConfidenceMeterValue -= confidenceDecrementVar*Time.deltaTime;

		//update visual
		updateConMeters();

		//check if it crossed the threshold so there is a delay
		if (prevRightMeterVal >0 && rightConfidenceMeterValue <= 0) {
			rightConfidenceMeterValue -= RETREAT_DELAY_AMOUNT;
		}
		if (prevLeftMeterVal >0 && leftConfidenceMeterValue <= 0) {
			leftConfidenceMeterValue -= RETREAT_DELAY_AMOUNT;
		}

		//update prev values
		prevLeftMeterVal = leftConfidenceMeterValue;
		prevRightMeterVal = rightConfidenceMeterValue;



		if (gameOver) {
			return;
		}
		//if there aren't enough birds, spawn a new one each frame until there are
		if (curLivingBirds < maxBirdsSpawnedAtOnce) {
			spawnBird ();
		}
		handleVictoryDefeatCountdown();
    }
	
	void OnGUI()
	{
		if (endGameMessage != null &&endGameMessage.Length > 0) {
			string message = winLoseMessage + "\n";
			message += endGameMessage + "\n";
			if (messageTracker != null) {
				messageTracker.GetComponent<TextMesh>().text = endGameMessage; 
			}
		}
	}

	private void handleVictoryDefeatCountdown()
	{
		bool leftMeterMaxed = (leftConfidenceMeterValue >= MAX_CONFIDENCE_METER_AMOUNT);
		bool rightMeterMaxed = 	(rightConfidenceMeterValue >= MAX_CONFIDENCE_METER_AMOUNT);
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
				OnPlayerVictory(false);
				gameOver = true;
			}
		}
	}

	private void OnCountdownMeterExpiring(float remaining) 
	{
		// handle meter flashing updates here
	}

	private void OnPlayerVictory(bool didArmiesRetreat) //determines what trigged this win condition 
	{
		// handle victory condition
		winLoseMessage = "Player Victorious!";
		if (didArmiesRetreat) {
			endGameBonusScore = 1000;
			endGameMessage = "The battalions failed to\ncoordinate their attack.\nOne attacked and was easily\ndefeated. The other retreated.";
		} else {
			endGameBonusScore = 500;
			endGameMessage = "The battalions both lost\nconfidence and retreated.\nThe town is safe!";
		}
		Debug.Log ("OnPlayerVictory");
	}

	private void OnPlayerDefeat()
	{
		// handle defeat condition
		endGameBonusScore = 0;
		endGameMessage = "The armies successfully\ncoordinated their attack.\nThe town was destroyed.";
		winLoseMessage = "Player Defeated!";
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
			score += SCORE_FOR_DESTROYING_BIRD_NORMAL;
			scoreTracker.GetComponent<TextMesh>().text = "Score : "+"\n"+score; //update the score GUI
		} else if (method == birdDestroyMethod.REACHED_RIGHT) {
			rightConfidenceMeterValue += defaultConfidenceBoostAmount;
			if (rightConfidenceMeterValue <= MAX_CONFIDENCE_METER_AMOUNT){
				float confidenceMeterModifier;
				confidenceMeterModifier = (rightConfidenceMeterValue * confidenceMeterLength) / MAX_CONFIDENCE_METER_AMOUNT;
			 	conMeterRight.transform.localPosition= new Vector3(rightConfidenceMeterStartingXPosition - confidenceMeterModifier,  conMeterRight.transform.localPosition.y, 1f);
			}

			//Debug.Log ("Right Confidence @ "+rightConfidenceMeterValue);
		} else if (method == birdDestroyMethod.REACHED_LEFT) {
			leftConfidenceMeterValue += defaultConfidenceBoostAmount;
			if (leftConfidenceMeterValue <= MAX_CONFIDENCE_METER_AMOUNT){
				float confidenceMeterModifier;
				confidenceMeterModifier = (leftConfidenceMeterValue * confidenceMeterLength) / MAX_CONFIDENCE_METER_AMOUNT;
				conMeterLeft.transform.localPosition= new Vector3(leftConfidenceMeterStartingXPosition + confidenceMeterModifier,  conMeterLeft.transform.localPosition.y, 1f);
			}

			//Debug.Log ("Left Confidence @ "+leftConfidenceMeterValue); 
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

