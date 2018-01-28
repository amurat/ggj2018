using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class byzArrowProjectileC : MonoBehaviour {

	public float velocity = 10;

	public AudioClip projectileHitAudioClip;

    void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "birdC")
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
