using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonController : MonoBehaviour
{
    UIManager uiManager;

    private bool canMakeBack = false;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canMakeBack)
        {
            MoveBackInScreen();
        }
    }

    public void SetBack(bool val)
    {
        canMakeBack = val;
    }

    //when player clicks on the back button
    public void BtnBack_OnClick()
    {
        if (canMakeBack)
        {
            MoveBackInScreen();
        }
    }


    public void BtnClose_OnClick()
    {
        Application.Quit();
    }


    private void MoveBackInScreen()
    {
        VoiceOverManager.Instance.StopVoiceOver();
        uiManager.MoveBackScreen();
    }

}
