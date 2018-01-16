using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public void PlayAgain()
    {
        SceneManager.LoadScene("StuntScene");
        
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
