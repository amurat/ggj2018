using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class byzArrowProjectile : MonoBehaviour {

    public float velocity = 10;
    
    public AudioClip projectileHitAudioClip;


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "bird")
        {
            if (projectileHitAudioClip) {
                AudioSource.PlayClipAtPoint(projectileHitAudioClip, transform.position);
				FindObjectOfType<byzantineBirdGen> ().DestroyBird (birdDestroyMethod.DESTROYED_BY_ARROW);
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