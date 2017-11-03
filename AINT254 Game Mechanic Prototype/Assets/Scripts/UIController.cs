using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField] Transform uiTransform;
    [SerializeField] Transform uiLastPos;
    [SerializeField] GameObject winGameUI;

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
}
