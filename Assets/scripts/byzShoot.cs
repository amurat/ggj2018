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

	public float projectileCapacity = 5;
	public float projectileRechargeRate = 2;

	public float currentCapacity;
	
	// Use this for initialization
	void Start () {
		currentCapacity = projectileCapacity;
	}
	
	// Update is called once per frame
	void Update () 
    {
		// update ammo capacity
		currentCapacity += projectileRechargeRate * Time.deltaTime;
		currentCapacity = Mathf.Min(currentCapacity, projectileCapacity);

		if (currentCapacity < 1)
		{
			return;
		}

		bool fire = false;
	    if (Input.GetKeyDown(shootKeyA))
	    {
	        GameObject shot = GameObject.Instantiate(projectileA, transform.position, transform.rotation);
			Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
			fire = true;
	    }
		else if (Input.GetKeyDown(shootKeyB))
		{
			GameObject shot = GameObject.Instantiate(projectileB, transform.position, transform.rotation);
			Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
			fire = true;
		} 
		else if (Input.GetKeyDown(shootKeyC))
		{
			GameObject shot = GameObject.Instantiate(projectileC, transform.position, transform.rotation);
			Physics.IgnoreCollision(shot.GetComponent<Collider>(), GetComponent<Collider>());
			fire = true;
		}
		if (fire && fireProjectileAudioClip) {
                AudioSource.PlayClipAtPoint(fireProjectileAudioClip, transform.position);
        }
		if (fire) {
			currentCapacity -= 1;
		}

	}
}