using UnityEngine;
using System.Collections;

public class mouseMoveLR : MonoBehaviour {

    public GameObject baby;
    private Vector3 mousePos;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.A)))
        {
            transform.position += new Vector3(-1f, 0f, 0f) * 10 * Time.deltaTime;
        }
        if ((Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.D)))
        {
            transform.position += new Vector3(1f, 0f, 0f) * 10 * Time.deltaTime;
        }
        transform.position = new Vector3(Input.mousePosition.x-Screen.width/2, 0f, 0f)*Time.deltaTime;
    }
}



