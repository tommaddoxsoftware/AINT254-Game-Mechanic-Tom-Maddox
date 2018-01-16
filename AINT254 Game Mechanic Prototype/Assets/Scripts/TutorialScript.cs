using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {
    [SerializeField] private GameObject gameUi;
    [SerializeField] private GameObject tutorialUi;
    [SerializeField] private GameObject welcomePanel;
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject player;
    [SerializeField] private Text promptHeader;
    [SerializeField] private Text promptText;
    [SerializeField] private Text promptBtn;

    private string header, msg;
    private bool tutorial = true;
    private bool firstShotPrompt;
    private string title;
    


    // Use this for initialization
    void Start () {
        header = "";
        msg = "";
        tutorialUi.SetActive(true);

        player.GetComponent<PlayerAim>().tutorial = true;  
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial == false && UnityEngine.Input.GetMouseButtonUp(0) && player.GetComponent<PlayerAim>().angleSet && !firstShotPrompt)
        {
            Debug.Log("You're fired");
            header = "Congratulations!";
            msg = "You've learned to control the car. Let's get you into the action!";
            promptBtn.text = "Track Selection";
            StartCoroutine(DelayPrompt("Finish", header, msg));
            firstShotPrompt = true;
        }
    }


    public void StartTutorial()
    {        
        welcomePanel.SetActive(false);

        //Initial prompt setup
        header = "Controlling your car";
        msg = "To control your car, click and drag left/right to set the angle of your car. Once you let go of the left mouse button, your angle will be set! (Press right mouse button to unset your angle) \nOnce you're happy with your angle, click and drag down/up to adjust the level of power to apply to the car!";
        title = "Control";
        StartCoroutine(DelayPrompt("Control", header, msg));
    }

    IEnumerator DelayPrompt(string title, string header, string msg)
    {
        yield return new WaitForSeconds(2);
        ShowPrompt(title, header, msg);       
    }

    private void ShowPrompt(string title, string header, string msg)
    {
        player.GetComponent<PlayerAim>().tutorial = true;
        prompt.SetActive(true);
        promptHeader.text = header;
        promptText.text = msg;
       
    

    }
    public void ClosePrompt()
    {
        Time.timeScale = 1;
        prompt.SetActive(false);
        player.GetComponent<PlayerAim>().tutorial = false;
        if (title.Equals("Control"))
            StartCoroutine(LockPrompt());

    }
    IEnumerator LockPrompt()
    {
        yield return new WaitForSeconds(1);
        tutorial = false;
        
    }
}
