using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class byzArrowProjectile : MonoBehaviour {

    float velocity = 10;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bird")
        {
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