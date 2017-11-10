using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAim : MonoBehaviour {
    private GameObject uiManager;

    [SerializeField] private GameObject aimPrefab; //The prefab that will create the aim line
    private GameObject aimObject; //Used to store prefabs in an array
    [SerializeField] private GameObject aimContainer; //Where the aim prefab will be placed

    private Transform m_transform; //Make a reference for the transform
    private Rigidbody m_rigidbody; //Rigidbody of the player object

    //Vars for the force
    [SerializeField] private float base_force = 500f;
    private float m_force;
    
    //Mouse Positions
    private Vector3 m_mouseDownPos;
    private Vector3 m_mouseUpPos;
    private float m_mouseDiffY;

    private float rotAngle;

    //Useful Calculation vars for mouse drag
    float gameMaxY;
    float diffPercentPower;

    //Used to check what's happening in the game
    private bool moving = false;
    private bool angleSet = false;

    private UIController uiControl;


    // Use this for initialization
    void Start () {

       
        //Find Width and height of the camera - used to find percentage difference with mouse drag
        string[] res = UnityStats.screenRes.Split('x');
        gameMaxY = float.Parse(res[1]);       

        //Create Transform reference - Efficiency!
        m_transform = transform;
        
        //Get the rigidbody
        m_rigidbody = GetComponent<Rigidbody>();

        //Instantiate the aim line
        aimObject = Instantiate(aimPrefab);
        aimObject.SetActive(false);
        aimObject.transform.parent = aimContainer.transform;

        uiManager = GameObject.Find("UIController");
        uiControl = uiManager.GetComponent<UIController>();
	}
	
	// Update is called once per frame
	void Update () {        
     
     if(angleSet && Input.GetMouseButtonDown(1))
        {
            //User right clicked - reset the rotation and UI states!
            angleSet = false;
            m_transform.localRotation = Quaternion.Euler(0, 0, 0);
            uiControl.ToggleUI("AngleSet");
        }
     if(UnityEngine.Input.GetMouseButtonDown(0))
        {
            m_mouseDownPos = Input.mousePosition;    
            m_mouseDownPos.z = 0;
        }
    if (UnityEngine.Input.GetMouseButtonUp(0))
        {            
           //Ensure player cant move whilst car is still in motion
            if (!moving && angleSet)
            {   
                //Modify force based on how much the mouse was dragged, then apply the force
                m_force = base_force * (diffPercentPower/5);
                Debug.Log("m_force:" + m_force);
                if(m_force > 0)
                m_rigidbody.AddForce(transform.forward * (m_force));
                moving = true;

                //Scale UI power bars!
                uiControl.SetLastPosScale();
                uiControl.ResetPowerBar();

                angleSet = false;
                
            }
            else
            {
                angleSet = true;
                uiControl.ToggleUI("AngleSet");
            }
                aimObject.SetActive(false);
        }

    if(Input.GetMouseButton(0))
        {
            m_mouseUpPos = Input.mousePosition;
            m_mouseUpPos.z = 0;

            if (!moving && !angleSet)
            {
                rotAngle = m_transform.rotation.x - (m_mouseDownPos.x - m_mouseUpPos.x);
                m_transform.localRotation = Quaternion.Euler(0, rotAngle, 0);
                aimContainer.transform.rotation = Quaternion.Euler(0, rotAngle, 0);
                Aim();
            }
            else
            {
                //Set mouse difference, then calculate percentage change
                m_mouseDiffY = (m_mouseDownPos.y - m_mouseUpPos.y) * 1.5f;
                diffPercentPower = (m_mouseDiffY / gameMaxY) * 100;

                //Cap mouse drag at 100% difference.
                if (diffPercentPower > 100)
                    diffPercentPower = 100;

                //Scale power bar appropriately
                uiManager.GetComponent<UIController>().ScalePowerBar(new Vector3(1, diffPercentPower / 100, 1));
            }

        }
    }
    private void FixedUpdate()
    {
        if (m_rigidbody.velocity == new Vector3(0, 0, 0))
        {
            moving = false;           
        }
        
    }

    //Some notes - Camera X is left/right,, Camera Y is up/down. Z not used.
    private void Aim()
        {    
            aimObject.transform.position = aimContainer.transform.position;
            aimObject.SetActive(true);

            int angle = (int)rotAngle;
            uiControl.UpdateText("Angle", angle.ToString());
        }
    private void OnTriggerEnter(Collider other)
    {


        if(other.tag == "Finish")
        {
            //Display End UI
            uiManager.GetComponent<UIController>().EndGameUI();
        }
    }

}
