using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] Transform uiTransform;
    [SerializeField] Transform uiLastPos;
    [SerializeField] GameObject winGameUI;
    [SerializeField] GameObject[] uiTexts = new GameObject[10];
    private GameObject tempObj;

    private void Start()
    {
        //Set power bar scale to 0
        uiTransform.transform.localScale = new Vector3(1, 0, 1);
        uiLastPos.transform.localScale = new Vector3(1, 0, 1);
    }

    public void ResetPowerBar()
    {
        uiTransform.transform.localScale = new Vector3(1, 0, 1);
    }

    public void ScalePowerBar(Vector3 newScale) {

        uiTransform.transform.localScale = newScale;
    }

    public void SetLastPosScale()
    {
        uiLastPos.transform.localScale = uiTransform.transform.localScale;
    }

    public void EndGameUI()
    {
        winGameUI.SetActive(true);
    }
    public void ToggleUI(string elemName)
    {
      
        switch (elemName)
        {
            case "AngleSet":
                tempObj = uiTexts[0];
                break;
        }

        if (tempObj.activeInHierarchy)
            tempObj.SetActive(false);
        else
            tempObj.SetActive(true);
    }

    public void UpdateText(string textObj, string newText)
    {

        switch(textObj)
        {
            case "Angle":
                uiTexts[1].GetComponent<Text>().text = "ANGLE: " + newText;
                break;
        }
    }
}
