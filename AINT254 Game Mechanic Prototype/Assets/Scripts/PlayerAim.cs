using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour {

    [SerializeField] private GameObject aimPrefab; //The prefab that will create the aim line
    private GameObject[] aimObjects; //Used to store prefabs in an array

    private Transform m_transform;
    private Vector3 m_direction; //Hold the direction player is aiming in
    [SerializeField] private float m_min_force; //Minimum force that can be applied
    [SerializeField] private float m_max_force; //Maximum force that can be applied
    private float m_force = 8.0f;

    private Vector3 m_mouseDownPos;
    private Vector3 m_mouseUpPos;

    private Rigidbody m_rigidbody; //Rigidbody of the player object

	// Use this for initialization
	void Start () {

        //Create Transform reference - Efficiency!
        m_transform = transform;

        //Initialise array of aimObjects - 10 length
        aimObjects = new GameObject[10];

        //Get the rigidbody
        m_rigidbody = GetComponent<Rigidbody>();

        //Loop through and populate the array with prefabs
        for(int i=0; i<aimObjects.Length; i++)
        {
            //Instantiate the object, hide it then store it
            GameObject tempObj = Instantiate(aimPrefab);            
            tempObj.SetActive(false);
            aimObjects[i] = tempObj;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        
     
     if(UnityEngine.Input.GetMouseButtonDown(0))
        {
            m_mouseDownPos = Input.mousePosition;
            Debug.Log("The mouseDownPos is: " + m_mouseDownPos);
            m_mouseDownPos.z = 0;
        }
    if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            
            Debug.Log("The mouseUpPos is: " + m_mouseUpPos);
            m_rigidbody.velocity = m_direction * m_force;
            for(int i=0; i<aimObjects.Length; i++)
            {
                aimObjects[i].SetActive(false);
            }
        }

    if(Input.GetMouseButton(0))
        {
            m_mouseUpPos = Input.mousePosition;
            m_mouseUpPos.z = 0;
            Vector3 charPosition = Camera.main.WorldToScreenPoint(m_transform.position);
            m_direction = (charPosition - m_mouseDownPos).normalized;
           
            Aim();

        }


    }
    private void Aim()
    {


        for(int i=0; i<aimObjects.Length; i++)
        {
            float t = i * 1.0f;
            aimObjects[i].transform.position = new Vector3(m_transform.position.x , m_transform.position.y, (m_transform.position.z + t) );
            aimObjects[i].SetActive(true);
        }
    }
}
