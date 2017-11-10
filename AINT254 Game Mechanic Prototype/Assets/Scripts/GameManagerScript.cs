﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public void PlayAgain()
    {
        SceneManager.LoadScene("StuntScene");
        Debug.Log("Scene Loaded");
    }
}
