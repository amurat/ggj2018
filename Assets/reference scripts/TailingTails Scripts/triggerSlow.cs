using UnityEngine;
using System.Collections;

public class triggerSlow : MonoBehaviour {

    public GameObject slowArea;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider slowArea)
    {
        if (gameObject.name == "player1")
        {
            Debug.Log("P1 has entered");
        }
        else if (gameObject.name == "player2")
        {
            Debug.Log("P2 has entered");
        }
    }
    void OnTriggerExit(Collider slowArea)
    {
        if (gameObject.name == "player1")
        {
            Debug.Log("P1 has exited");
        }
        else if (gameObject.name == "player2")
        {
            Debug.Log("P2 has exited");
        }
    }
}
