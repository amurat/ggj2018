using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class byzArrowProjectileB : MonoBehaviour {

	public float velocity = 10;

	public AudioClip projectileHitAudioClip;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "birdB")
		{
			if (projectileHitAudioClip) {
				AudioSource.PlayClipAtPoint(projectileHitAudioClip, transform.position);
			}
			GameObject.Destroy(collision.gameObject);
			GameObject.Destroy(gameObject);
		}
	}

	void Update () {
		transform.position += transform.up * velocity * Time.deltaTime;
	}

	void OnBecameInvisible () {
		Destroy(gameObject);
	}
}
