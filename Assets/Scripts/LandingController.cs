using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingController : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }


    public void BtnStart_OnClick()
    {
        gameManager.ShowChracterSelection();
    }
}
