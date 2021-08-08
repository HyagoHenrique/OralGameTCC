using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RatingController : MonoBehaviour
{
    public AnalyticsTracker analyticsTracker;
    public GameManager gameManager;
    public Transform transBtnHome;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BtnRatingAngry_OnClick()
    {
        //ShowButtonHome();
        BtnHome_OnClick();
    }

    public void BtnRatingSad_OnClick()
    {
        //ShowButtonHome();
        BtnHome_OnClick();
    }

    public void BtnIdle_OnClick()
    {
        //ShowButtonHome();
        BtnHome_OnClick();
    }

    public void BtnSmile_OnClick()
    {
        //ShowButtonHome();
        BtnHome_OnClick();
    }

    public void BtnHappy_OnClick()
    {
        //ShowButtonHome();
        BtnHome_OnClick();
    }

    public void BtnHome_OnClick()
    {
        DeleteAllProgress();
        MoveToStart();
    }

    private void DeleteAllProgress()
    {
        PlayerPrefs.DeleteAll();
    }

    private void ShowButtonHome()
    {
        transBtnHome.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);
    }

    public void MoveToStart()
    {
        SceneManager.LoadScene(0);
    }
}
