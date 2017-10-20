using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody m_rigidbody;
    private float m_force = 1000;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(transform.forward * m_force);

        Destroy(gameObject, 5.0f);
	}
	
}
