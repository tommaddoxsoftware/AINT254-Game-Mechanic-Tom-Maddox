using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    private Rigidbody m_rigidbody;
    public float speed = 500;

	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        m_rigidbody.AddForce(transform.forward * speed * Input.GetAxis("Vertical") * Time.deltaTime);
        m_rigidbody.AddForce(transform.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime);
    }
}
