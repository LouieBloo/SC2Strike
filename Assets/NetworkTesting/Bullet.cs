using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 150);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gm = collision.gameObject;

        Health h = gm.GetComponent<Health>();
        if(h != null)
        {
            h.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
