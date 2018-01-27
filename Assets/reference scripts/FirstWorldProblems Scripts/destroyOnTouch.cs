using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class destroyOnTouch : MonoBehaviour {

    public GameObject fallingThingy;
    int dropHitCount = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (dropHitCount >= 75)
        {
            SceneManager.LoadScene(2);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destroy")
        {
            Destroy(other.gameObject);
            dropHitCount++;
        }
       // Debug.Log("contact made");
       // Debug.Log("Hit by " + dropHitCount + " raindrops.");
    }
}
