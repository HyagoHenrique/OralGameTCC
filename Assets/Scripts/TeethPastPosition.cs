using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethPastPosition : MonoBehaviour
{
    public Vector3 malePos;
    public Vector3 femalePos;


    private string gender;


    private void OnEnable()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        if (gender == "M")
        {
            transform.localPosition = malePos;
        }
        else
        {
            transform.localPosition = femalePos;
        }
    }

    public void SetGender(string gender)
    {
        this.gender = gender;
    }
}
