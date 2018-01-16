using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadTracks()
    {
        SceneManager.LoadScene("Tracks");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadTrack(int id)
    {
        switch(id)
        {
            case 1:
                SceneManager.LoadScene("StuntScene");
                break;
        }
    }
}
