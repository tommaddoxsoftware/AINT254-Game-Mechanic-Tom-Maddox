using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour {

    [SerializeField] private GameObject aimPrefab; //The prefab that will create the aim line
    private GameObject aimObject; //Used to store prefabs in an array

    private Transform m_transform;
    private Vector3 m_direction; //Hold the direction player is aiming in
    [SerializeField] private float m_min_force; //Minimum force that can be applied
    [SerializeField] private float m_max_force; //Maximum force that can be applied
    [SerializeField] private float m_force = 1000f;

    private Vector3 m_mouseDownPos;
    private Vector3 m_mouseUpPos;

    [SerializeField] private GameObject aimContainer;

    private Rigidbody m_rigidbody; //Rigidbody of the player object

	// Use this for initialization
	void Start () {
      

        //Create Transform reference - Efficiency!
        m_transform = transform;


        //Get the rigidbody
        m_rigidbody = GetComponent<Rigidbody>();

        
            aimObject = Instantiate(aimPrefab);
            aimObject.SetActive(false);
            aimObject.transform.parent = aimContainer.transform;		
	}
	
	// Update is called once per frame
	void Update () {
        
     
     if(UnityEngine.Input.GetMouseButtonDown(0))
        {
            m_mouseDownPos = Input.mousePosition;    
            m_mouseDownPos.z = 0;
        }
    if (UnityEngine.Input.GetMouseButtonUp(0))
        {            
           //Ensure player cant move whilst car is still in motion
            if (m_rigidbody.velocity == new Vector3(0, 0, 0))
            {
                m_rigidbody.AddForce(transform.forward * m_force);
            }            

                aimObject.SetActive(false);

        }

    if(Input.GetMouseButton(0))
        {
            m_mouseUpPos = Input.mousePosition;
            m_mouseUpPos.z = 0;
            Vector3 charPosition = Camera.main.WorldToScreenPoint(m_transform.position);
            if (m_rigidbody.velocity == new Vector3(0, 0, 0))
            {
                float rotAngle = m_transform.rotation.x - (m_mouseDownPos.x - m_mouseUpPos.x);
                m_transform.rotation = Quaternion.Euler(0, rotAngle, 0);
                aimContainer.transform.rotation = m_transform.rotation;
                Aim();
            }

        }
    }

    //Some notes - Camera X is left/right,, Camera Y is up/down. Z not used.
    private void Aim()
        {    
            aimObject.transform.position = aimContainer.transform.position;
            aimObject.SetActive(true);
        }
    
}
