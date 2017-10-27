using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAim : MonoBehaviour {

    [SerializeField] private GameObject aimPrefab; //The prefab that will create the aim line
    private GameObject aimObject; //Used to store prefabs in an array

    private Transform m_transform;
    private Vector3 m_direction; //Hold the direction player is aiming in
    [SerializeField] private float base_force = 500f;
    private float m_force;
    
    private Vector3 m_mouseDownPos;
    private Vector3 m_mouseUpPos;

    [SerializeField] Transform uiTransform;
    [SerializeField] Transform uiLastPos;   

    [SerializeField] private GameObject aimContainer;

    private Rigidbody m_rigidbody; //Rigidbody of the player object
        
    float gameMaxY;
    float m_mouseDiffY;
    
    float diffPercentPower;
    float oldPercent = 0;

    // Use this for initialization
    void Start () {

        //Set power bar scale to 0
        uiTransform.transform.localScale = new Vector3(1, 0, 1);
        uiLastPos.transform.localScale = new Vector3(1, 0, 1);
        //Find Width and height of the camera - used to find percentage difference with mouse drag
        string[] res = UnityStats.screenRes.Split('x');
        gameMaxY = float.Parse(res[1]);
       

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
                //Modify force based on how much the mouse was dragged, then apply the force
                m_force = base_force * (diffPercentPower/5);
                Debug.Log("m_force:" + m_force);
                if(m_force > 0)
                m_rigidbody.AddForce(transform.forward * (m_force));

                uiLastPos.localScale = uiTransform.localScale;
                uiTransform.localScale = new Vector3(1, 0, 1);
                
            }
                aimObject.SetActive(false);
        }

    if(Input.GetMouseButton(0))
        {
            m_mouseUpPos = Input.mousePosition;
            m_mouseUpPos.z = 0;
           
            if (m_rigidbody.velocity == new Vector3(0, 0, 0))
            {
                float rotAngle = m_transform.rotation.x - (m_mouseDownPos.x - m_mouseUpPos.x);
                m_transform.rotation = Quaternion.Euler(0, rotAngle, 0);
                aimContainer.transform.rotation = m_transform.rotation;
                Aim();
            }

            //Set mouse difference, then calculate percentage change
            m_mouseDiffY = (m_mouseDownPos.y - m_mouseUpPos.y) * 1.5f;
            diffPercentPower = (m_mouseDiffY / gameMaxY) * 100;

            //Cap mouse drag at 100% difference.
            if (diffPercentPower > 100)
                diffPercentPower = 100;
            
            //Scale power bar appropriately
            uiTransform.localScale = new Vector3(1,diffPercentPower / 100,1);

        }
    }

    //Some notes - Camera X is left/right,, Camera Y is up/down. Z not used.
    private void Aim()
        {    
            aimObject.transform.position = aimContainer.transform.position;
            aimObject.SetActive(true);
        }
    
}
