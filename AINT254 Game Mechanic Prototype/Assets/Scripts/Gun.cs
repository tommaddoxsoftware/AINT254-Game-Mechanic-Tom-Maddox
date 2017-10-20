using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField] private GameObject m_bullet;
    private Transform m_transform;

	// Use this for initialization
	void Start () {
        m_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Instantiate(m_bullet, m_transform.position + m_transform.forward * 3, m_transform.rotation);
        }
	}
}
