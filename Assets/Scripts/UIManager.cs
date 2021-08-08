using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private UIScreen[] arrScreens;
    [SerializeField]
    private CharacterSelector characterSelector;
    [SerializeField]
    private GameManager gameManager;

    private MaskAnimator maskAnimator;

    private ScreenType m_activeScreen;
    private int m_LastActiveScreenIndex;

    private BackButtonController backController;

    // Start is called before the first frame update
    void Start()
    {
        backController = GetComponent<BackButtonController>();
        maskAnimator = GetComponent<MaskAnimator>();
        ShowStartScreen();

    }

    public void MoveToScreen(ScreenType wantedScreen)
    {
        if (m_activeScreen != wantedScreen)
        {
            //Debug.Log("Moving to screen : " + wantedScreen + " Active screen : " + m_activeScreen);
            StartCoroutine(SwitchScreensSequence(wantedScreen));
        }
    }

    private void ShowStartScreen()
    {
        StartCoroutine(StartScreenSequence());
        //ShowScreenByType(ScreenType.CharacterSelection);
        //m_activeScreen = ScreenType.CharacterSelection;
    }

    private void ShowScreenByType(ScreenType screenType)
    {
        for (int i = 0; i < arrScreens.Length; i++)
        {
            if (arrScreens[i].GetScreenType() == screenType)
            {
                arrScreens[i].ShowScreen();
                m_activeScreen = screenType;
                m_LastActiveScreenIndex = i;
                break;
            }
        }
    }

    private void SetScreenActivationByType(ScreenType screenType, bool isActive)
    {
        for (int i = 0; i < arrScreens.Length; i++)
        {
            if (arrScreens[i].GetScreenType() == screenType)
            {
                arrScreens[i].SetScreenActive(isActive);
            }
        }
    }

    //hide the active screen then move to the wanted screen
    private IEnumerator SwitchScreensSequence(ScreenType wantedScreen)
    {
        //deactivate back button
        backController.SetBack(false);

        //hide the active screen
        arrScreens[m_LastActiveScreenIndex].HideScreen();

        //wait till the screen is hidden
        while (arrScreens[m_LastActiveScreenIndex].GetScreenState() == ScreenState.Animating)
        {
            yield return null;
        }

        //show mask Animation
        maskAnimator.TweenMaskIn();
        //wait till mask hide all Screen
        yield return new WaitForSeconds(maskAnimator.MaskInDuration);

        //deactiate last active screen parent
        arrScreens[m_LastActiveScreenIndex].SetScreenActive(false);

        //enable the wanted screen parent
        SetScreenActivationByType(wantedScreen, true);

        ShowCharacterByScreenType(wantedScreen);

        //hide mask out animation
        maskAnimator.TweenMaskOut();
        yield return new WaitForSeconds(maskAnimator.MaskOutDuration);

        //show the wanted screen
        ShowScreenByType(wantedScreen);

        //wait till the screen is visiable
        while (arrScreens[m_LastActiveScreenIndex].GetScreenState() == ScreenState.Animating)
        {
            yield return null;
        }

        //activate back button
        backController.SetBack(true);
    }

    private IEnumerator StartScreenSequence()
    {
        //deactivate back button
        backController.SetBack(false);

        //enable the wanted screen parent
        SetScreenActivationByType(ScreenType.Landing, true);

        //show the wanted screen
        ShowScreenByType(ScreenType.Landing);

        //wait till the screen is visiable
        while (arrScreens[m_LastActiveScreenIndex].GetScreenState() == ScreenState.Animating)
        {
            yield return null;
        }
        SoundsManager.Instance.PlayMainTheme();
        //activate back button
        backController.SetBack(true);
    }

    

    /// <summary>
    /// called by back button
    /// </summary>
    public void MoveBackScreen()
    {
        Debug.Log("Moving back " + m_activeScreen);
        arrScreens[m_LastActiveScreenIndex].ResetScreen();
        switch (m_activeScreen)
        {
            case ScreenType.Intro:
                StartCoroutine(SwitchScreensSequence(ScreenType.CharacterSelection));
                gameManager.ShowSinjabByBackButton(ScreenType.CharacterSelection);
                break;
            case ScreenType.GameSelection:
                StartCoroutine(SwitchScreensSequence(ScreenType.Intro));
                gameManager.ShowSinjabByBackButton(ScreenType.Intro);
                VoiceOverManager.Instance.PlayIntro(4.1f);
                break;
            case ScreenType.Breakfast:
            case ScreenType.Launch:
            case ScreenType.Dinner:
            case ScreenType.BrushingTeeth:
            case ScreenType.Dentist:
            case ScreenType.Rating:
                StartCoroutine(SwitchScreensSequence(ScreenType.GameSelection));
                gameManager.ShowSinjabByBackButton(ScreenType.GameSelection);
                break;
        }
    }


    private void ShowCharacterByScreenType(ScreenType screenType)
    {
        switch (screenType)
        {
            case ScreenType.BrushingTeeth:
                characterSelector.ShowCharacterInBrushing();
                break;
            case ScreenType.Dentist:
                characterSelector.ShowCharacterInDentist();
                break;
        }
    }
}
