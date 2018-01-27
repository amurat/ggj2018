using UnityEngine;
using System.Collections;

public class orbiter : MonoBehaviour {
    public GameObject world;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.RotateAround(world.transform.position, Vector3.left, 20 * Time.deltaTime);
	}
}
