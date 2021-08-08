using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinjabController : MonoBehaviour
{
    public Transform transIntroSinjab;
    public Transform transGameSelectionSinjab;
    public Transform transBreakfastSinjab;
    public Transform transLunchSinjab;
    public Transform transDentistSinjab;


    private GafCharacterAnimator m_SinjabAnimator;


    // Start is called before the first frame update
    void Start()
    {
        m_SinjabAnimator = GetComponent<GafCharacterAnimator>();
    }



    public void SetSenjabToIntro()
    {
        CancelInvoke();
        SetSinjabIdleImmediatly();
        SetSinjabAndPlayAnimation(transIntroSinjab, "intro", 3.5f, 0.006f);
        MoveToIdleWithDelay(9.5f);
    }

    public void SetSenjabToGameSelection()
    {
        CancelInvoke();
        SetSinjabIdleImmediatly();
        SetSinjabAndPlayAnimation(transGameSelectionSinjab, "Idle", 3.5f, 0.006f);
    }

    public void SetSenjabToBreakfast()
    {
        CancelInvoke();
        SetSinjabIdleImmediatly();
        SetSinjabAndPlayAnimation(transBreakfastSinjab, "breakfast intro", 3.5f, 0.006f);
        MoveToIdleWithDelay(10.5f);
    }

    public void SetSenjabToLunch()
    {
        CancelInvoke();
        SetSinjabIdleImmediatly();
        SetSinjabAndPlayAnimation(transLunchSinjab, "talk_moving", 3.5f, 0.006f);
        MoveToIdleWithDelay(11.3f);
    }

    public void SetSenjabToDentist()
    {
        CancelInvoke();
        SetSinjabIdleImmediatly();
        SetSinjabAndPlayAnimation(transDentistSinjab, "talk_moving", 3.5f, 0.006f);
        MoveToIdleWithDelay(VoiceOverManager.Instance.GetCurrentVOLength() + 3.5f);
    }

    public void SetSenjabTalking(float length)
    {
        //Debug.Log("Clip length for talking : " + length);
        CancelInvoke();
        SetSinjabIdleImmediatly();
        m_SinjabAnimator.PlayAnimation("talk_moving", 0.0f);
        MoveToIdleWithDelay(length);
    }

    private void SetSinjabIdleImmediatly()
    {
        ResetToIdle();
    }

    private void MoveToIdleWithDelay(float delay)
    {
        Invoke("ResetToIdle", delay);
    }

    private void ResetToIdle()
    {
        m_SinjabAnimator.PlayAnimation("Idle Senjab", 0.0f);
    }

    private void SetSinjabAndPlayAnimation(Transform prnt, string strAnim, float delay, float scale)
    {
        m_SinjabAnimator.transform.SetParent(prnt);
        m_SinjabAnimator.transform.localPosition = Vector3.zero;
        m_SinjabAnimator.transform.localScale = Vector3.one * scale;
        m_SinjabAnimator.PlayAnimation(strAnim, delay);
    }

}
