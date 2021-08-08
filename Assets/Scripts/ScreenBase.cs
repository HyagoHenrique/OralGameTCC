using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject screenParent;
    [SerializeField]
    protected GameObject[] arrStaticObjects;

    [Header("TweenValues")]
    [SerializeField]
    protected float _finalScale;
    [SerializeField]
    protected float _showDuration;
    [SerializeField]
    protected float _HideDuration;
    [SerializeField]
    protected float _delay;
    [SerializeField]
    protected Ease _showEase;
    [SerializeField]
    protected Ease _hideEase;
    [SerializeField]
    protected Transform[] arrScreenElements;
    [SerializeField]
    protected ScreenType _screenType;

    protected ScreenState _screenState;

    public virtual void Awake()
    {
        _screenState = ScreenState.Hidden;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
    }

    public virtual void SetScreenActive(bool isActive)
    {
        screenParent.SetActive(isActive);
        for (int i = 0; i < arrStaticObjects.Length; i++)
        {
            arrStaticObjects[i].SetActive(isActive);
        }
    }

    public virtual void ShowScreen()
    {
        StartCoroutine(AnimateScreenShow());
    }

    public virtual void HideScreen()
    {
        StartCoroutine(AnimateScreenHide());
    }

    public virtual IEnumerator AnimateScreenShow()
    {
        //screenParent.SetActive(true);
        _screenState = ScreenState.Animating;
        //float totalDelay = 0;
        //for (int i = 0; i < arrScreenElements.Length; i++)
        //{
        //    arrScreenElements[i].DOScale(_finalScale, _showDuration).SetEase(_showEase).SetDelay(totalDelay);
        //    totalDelay += _delay;
        //}
        //Invoke("AnimateElementsSeperatly", 1.0f);
        AnimateElementsSeperatly();
        yield return new WaitForSeconds(_showDuration + 0.25f);//+ totalDelay
        _screenState = ScreenState.Visible;
    }

    private void AnimateElementsSeperatly()
    {
        float totalDelay = 0;
        for (int i = 0; i < arrScreenElements.Length; i++)
        {
            arrScreenElements[i].DOScale(_finalScale, _showDuration).SetEase(_showEase).SetDelay(totalDelay);
            totalDelay += _delay;
        }
    }

    public virtual IEnumerator AnimateScreenHide()
    {
        _screenState = ScreenState.Animating;
        float totalDelay = 0;
        for (int i = 0; i < arrScreenElements.Length; i++)
        {
            arrScreenElements[i].DOScale(0.0f, _HideDuration).SetEase(_hideEase).SetDelay(totalDelay);
            totalDelay += _delay;
        }
        yield return new WaitForSeconds(_HideDuration + totalDelay);
        _screenState = ScreenState.Hidden;
        ResetScreen();
        //screenParent.SetActive(false);
    }

    public ScreenType GetScreenType()
    {
        return _screenType;
    }

    public ScreenState GetScreenState()
    {
        return _screenState;
    }

    public virtual void ResetScreen()
    {

    }


}

public enum ScreenState
{
    Hidden,
    Animating,
    Visible
}

public enum ScreenType
{
    CharacterSelection,
    Intro,
    GameSelection,
    Breakfast,
    Launch,
    Dinner,
    BrushingTeeth,
    Dentist,
    Rating,
    Landing,
    MiniGame
}
