using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BrushingTeethController : MonoBehaviour, IScreenRset
{

    public GameManager gameManager;
    public Transform transBtnHome;
    public CharacterSelector characterSelector;
    public CameraController cameraController;
    public Animator teethPastAnimator;
    public Transform transBtnBrushingUp;
    public Transform transBtnBrushingSide;
    public Transform transBtnSpit;
    public StarCounterController starCounter;
    public List<Button> lstButtons;
    [Header("Shine effects")]
    public Coffee.UIExtensions.UIShiny btnTeethPastShiny;
    public Coffee.UIExtensions.UIShiny btnBrushingUpShiny;
    public Coffee.UIExtensions.UIShiny btnBrushingSideShiny;
    public Coffee.UIExtensions.UIShiny btnSpitShiny;
    public Coffee.UIExtensions.UIShiny btnHomeShiny;

    private bool isTeethPastOn = false;
    private bool isTeethBrushUp = false;
    private bool isBrushingSide = false;
    private bool canSpit = true;
    private bool canSpitTwice = false;

    private int stepCount = 0;

    private int ButtonShakeTweenID;


    // Start is called before the first frame update
    void Start()
    {
        ButtonShakeTweenID = Random.Range(123456, 654321);
    }

    public void DeactivateButtonByStartTransition()
    {
        StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength()));
    }

    //public void MoveStarCounterByTeethBrushing(float delay)
    //{
    //    starCounter.SetStarCounterToMiddle(delay);
    //}

    public void SetTeethPastAnimation(bool isActive)
    {
        teethPastAnimator.gameObject.SetActive(isActive);
    }

    public void BtnToothPast_OnClick()
    {
        if (!isTeethPastOn)
        {
            btnTeethPastShiny.Stop();
            starCounter.AddStar();
            SetTeethPastAnimation(true);
            isTeethPastOn = true;
            stepCount++;
            CheckStepsCount();
            VoiceOverManager.Instance.PlayTeethPast();
            StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength()));
        }
    }

    public void BtnBrushingUp_Onclick()
    {
        if (!isTeethBrushUp && isTeethPastOn)
        {
            btnBrushingUpShiny.Stop();
            starCounter.AddStar();
            SetTeethPastAnimation(false);
            isTeethBrushUp = true;
            stepCount++;
            CheckStepsCount();
            characterSelector.BrushTeethVertical();
            VoiceOverManager.Instance.PlayTeethBrushingUp();
            StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength()));
        }
    }

    public void BtnBrushingSide_OnClick()
    {
        if (!isBrushingSide && isTeethBrushUp && isTeethPastOn)
        {
            btnBrushingSideShiny.Stop();
            starCounter.AddStar();
            SetTeethPastAnimation(false);
            isBrushingSide = true;
            stepCount++;
            CheckStepsCount();
            characterSelector.BrushTeethHorizontal();
            VoiceOverManager.Instance.PlayTeethBrushingSide();
            StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength()));
        }
    }

    public void BtnSpit_OnClick()
    {
        if (isBrushingSide && isTeethBrushUp && canSpit)
        {
            btnSpitShiny.Stop();
            starCounter.AddStar();
            canSpit = false;
            canSpitTwice = true;
            stepCount++;
            CheckStepsCount();
            DOTween.Kill(ButtonShakeTweenID);
            transBtnSpit.localScale = Vector3.one;
            characterSelector.MakeCharacterSpit();
            VoiceOverManager.Instance.PlayTeethSpit();
            StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength()));
        }
        else if (canSpitTwice)
        {
            characterSelector.MakeCharacterSpit();
        }
    }

    private void ShowBtnBrushingUp()
    {
        transBtnBrushingUp.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack).SetDelay(0.1f).OnComplete(() =>
        {
            btnBrushingUpShiny.Play();
        });
    }

    private void ShowBtnBrushingSide()
    {
        transBtnBrushingSide.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack).SetDelay(0.1f).OnComplete(() =>
        {
            btnBrushingSideShiny.Play();
        });
    }

    private void ShowSpitButton()
    {
        transBtnSpit.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack).SetDelay(0.1f).OnComplete(() =>
        {
            transBtnSpit.DOPunchScale(Vector3.one * 0.03f, 0.25f).SetLoops(-1, LoopType.Restart).SetEase(Ease.InOutBack).SetId(ButtonShakeTweenID);
            btnSpitShiny.Play();
        });
    }

    private void CheckStepsCount()
    {
        if (stepCount == 4)
        {
            ShowHomeButton();
        }
    }

    private void ShowHomeButton()
    {
        transBtnHome.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);
    }

    public void BtnHome_OnClick()
    {
        //starCounter.SetStarCounterToOriginal(2.0f);
        cameraController.ResetCameraSize(2.0f);
        gameManager.FinishBrushing();
        gameManager.ShowGameSelection();
    }


    public void BtnBack_OnClick()
    {
        //starCounter.SetStarCounterToOriginal(2.0f);
    }

    public void OnScreenRset()
    {
        isTeethPastOn = false;
        isTeethBrushUp = false;
        isBrushingSide = false;
        canSpit = true;
        canSpitTwice = false;
        stepCount = 0;
        btnHomeShiny.Stop();
        btnTeethPastShiny.Stop();
        btnBrushingUpShiny.Stop();
        btnBrushingSideShiny.Stop();
        btnSpitShiny.Stop();
        DOTween.Kill(ButtonShakeTweenID);
        transBtnHome.localScale = Vector3.zero;
        transBtnSpit.localScale = Vector3.zero;
        transBtnBrushingUp.localScale = Vector3.zero;
        transBtnBrushingSide.localScale = Vector3.zero;
    }

    private IEnumerator LoopButtonsActivation(float delay)
    {
        SetAllButtonsActive(false);
        yield return new WaitForSeconds(delay);
        //Debug.Log("isTeeth past on " + isTeethPastOn + " is theeth brush up " + isTeethBrushUp + " , is teeth brush side " + isBrushingSide);
        if (isTeethPastOn && !isTeethBrushUp)
        {
            ShowBtnBrushingUp();
        }
        if (isTeethPastOn && isTeethBrushUp && !isBrushingSide)
        {
            ShowBtnBrushingSide();
        }
        if (isBrushingSide && isTeethBrushUp && isTeethPastOn && canSpit)
        {
            ShowSpitButton();
        }
        if (transBtnHome.localScale == Vector3.one)
        {
            btnHomeShiny.Play();
        }
        SetAllButtonsActive(true);
    }

    private void SetAllButtonsActive(bool isActive)
    {
        for (int i = 0; i < lstButtons.Count; i++)
        {
            lstButtons[i].interactable = isActive;
        }
    }




    //private void ResetToDefault()
    //{
    //    isTeethPastOn = false;
    //    isTeethBrushUp = false;
    //    isBrushingSide = false;
    //    stepCount = 0;
    //}
}
