using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameSelectionController : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Info screen")]
    public Image imgFade;
    public RectTransform rectInfoScreen;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    public void BtnBreakfast_OnClick()
    {
        gameManager.ShowBreakfast();
    }

    public void BtnLaunch_OnClick()
    {
        gameManager.ShowLaunch();
    }

    public void BtnDinner_OnClick()
    {
        gameManager.ShowDinner();
    }

    public void BtnDentist_Onclick()
    {
        gameManager.ShowDentist();
    }

    public void BtnMiniGameTemp_Onclick()
    {
        gameManager.ShowMiniGame();
    }

    public void BtnInfo_OnClick()
    {
        imgFade.gameObject.SetActive(true);
        imgFade.DOFade(0.5f, 0.35f).SetEase(Ease.OutCubic);
        rectInfoScreen.DOAnchorPosY(0.0f, 0.5f).SetEase(Ease.OutCubic);
    }

    public void BtnCloseInfo_OnClick()
    {
        
        imgFade.DOFade(0.0f, 0.35f).SetEase(Ease.OutCubic).OnComplete(()=>
        {
            imgFade.gameObject.SetActive(false);
        });
        rectInfoScreen.DOAnchorPosY(1200.0f, 0.5f).SetEase(Ease.InCubic);
    }

}
