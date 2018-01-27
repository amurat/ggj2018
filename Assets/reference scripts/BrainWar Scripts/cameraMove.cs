using UnityEngine;
using System.Collections;

public class cameraMoveCustom : MonoBehaviour {

	public Transform mainPlayer;
	public float delayTime;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (mainPlayer){
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint (mainPlayer.position);
			Vector3 delta = mainPlayer.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, delayTime);
		}
	}
}
