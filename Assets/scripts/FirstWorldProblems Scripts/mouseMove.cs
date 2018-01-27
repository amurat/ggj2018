using UnityEngine;
using System.Collections;

public class mouseMove : MonoBehaviour {

    private Vector3 mousePos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0f) * Time.deltaTime;
	}
}
