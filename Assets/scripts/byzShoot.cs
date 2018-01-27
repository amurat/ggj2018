using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class byzShoot : MonoBehaviour
{
    public KeyCode shootKey = KeyCode.UpArrow;
    public GameObject projectile;

	public AudioClip fireProjectileAudioClip;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKeyDown(shootKey))
	    {
	        GameObject shot = GameObject.Instantiate(projectile, transform.position, transform.rotation);
			Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
			GetComponent<AudioSource>().PlayOneShot(fireProjectileAudioClip);
	    }
	}
}