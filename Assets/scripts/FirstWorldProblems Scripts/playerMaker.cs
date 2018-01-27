using UnityEngine;
using System.Collections;

public class playerMaker : MonoBehaviour {

    public GameObject player;
    public GameObject hat;
    public GameObject eyes;
    public GameObject pants;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void cosmeticToggle(GameObject cosmetic)
    {
        if (cosmetic.GetComponent<SpriteRenderer>().enabled == false)
        {
            cosmetic.GetComponent<SpriteRenderer>().enabled = true;
        }
        else {
            cosmetic.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
