using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class byzShoot : MonoBehaviour
{
    public KeyCode shootKeyA = KeyCode.Alpha1;
	public KeyCode shootKeyB = KeyCode.Alpha2;
	public KeyCode shootKeyC = KeyCode.Alpha3;
    public GameObject projectileA;
	public GameObject projectileB;
	public GameObject projectileC;

	public AudioClip fireProjectileAudioClip;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetKeyDown(shootKeyA))
	    {
	        GameObject shot = GameObject.Instantiate(projectileA, transform.position, transform.rotation);
			Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
			GetComponent<AudioSource>().PlayOneShot(fireProjectileAudioClip);
	    }
		else if (Input.GetKeyDown(shootKeyB))
		{
			GameObject shot = GameObject.Instantiate(projectileB, transform.position, transform.rotation);
			Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
			GetComponent<AudioSource>().PlayOneShot(fireProjectileAudioClip);
		} 
		else if (Input.GetKeyDown(shootKeyC))
		{
			GameObject shot = GameObject.Instantiate(projectileC, transform.position, transform.rotation);
			Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
			GetComponent<AudioSource>().PlayOneShot(fireProjectileAudioClip);
		}
	}
}