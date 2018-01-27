using UnityEngine;
using System.Collections;

public class genEnemies : MonoBehaviour {

    public GameObject dolans;
    public GameObject trolls;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       int coinFlip = Random.Range(0, 2);
       StartCoroutine(genemy(coinFlip));
	}

    IEnumerator genemy(int result)
    {
        yield return new WaitForSeconds(2f);

        print(result);
        float limitTop = 10f;			//gets the Y position of the ceiling
        float limitBottom = 0f;	//gets the Y position of the floor
        float limitRight = Camera.main.transform.position.x+20f;		//gets the X position of the right wall
        
        Vector3 genPoint = new Vector3(Random.Range(Camera.main.transform.position.x, limitRight), Random.Range(limitBottom, limitTop), 0f);
        if (result > 0)
        {
            Instantiate(dolans, genPoint, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(1f);
        }
        else
        {
            Instantiate(trolls, genPoint, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(1f);
        }
    }
}
