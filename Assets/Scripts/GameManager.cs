using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("lunch Unlock")]
    public Image imglunchIcon;
    public Button btnlunch;
    public GameObject lockLunchObject;

    [Header("dinner Unlock")]
    public Image imgDinnerIcon;
    public Button btnDinner;
    public GameObject lockDinnerObject;

    [Header("dentist Unlock")]
    public Image imgDentistIcon;
    public Button btnDentist;
    public GameObject lockDentistObject;

    [Header("minigame Unlock")]
    public Image imgMiniGameIcon;
    public Button btnMniGame;
    public GameObject lockMiniGameObject;



    public Color deactiveColor;

    [Header("Dentist")]
    public DentistController dentistController;

    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private CharacterSelector characterSelector;

    [Header("Animators")]
    [SerializeField]
    private IntroController m_introController;

    [SerializeField]
    private SinjabController m_SinjabController;


    [Header("Camera")]
    [SerializeField]
    private CameraController m_CameraController;

    [Header("star counter")]
    [SerializeField]
    private StarCounterController m_starCounter;

    [Header("Mini Game")]
    [SerializeField]
    private MiniGameController miniGameController;

    [Header("Analytics tracker")]
    [SerializeField]
    private AnalyticsTracker analyticsTracker;

    [Header("teeth brushing")]
    [SerializeField]
    private BrushingTeethController brushingTeethController;
    [SerializeField]
    private BathroomNightController bathroomNightController;

    [Header("Shiny Effect")]
    [SerializeField]
    private Coffee.UIExtensions.UIShiny btnLaunchShiny;
    [SerializeField]
    private Coffee.UIExtensions.UIShiny iconLaunchShiny;
    [SerializeField]
    private Coffee.UIExtensions.UIShiny btnDinnerShiny;
    [SerializeField]
    private Coffee.UIExtensions.UIShiny iconDinnerShiny;
    [SerializeField]
    private Coffee.UIExtensions.UIShiny btnDentistShiny;




    private bool m_wasBreakfastFinished;
    private bool m_wasLunchFinished;
    private bool m_wasDennierFinished;

    private bool m_CanLunchShine = false;
    private bool m_CanDinnerShine = false;
    private bool m_CanDentistShine = false;

    private bool m_launchPressed = false;
    private bool m_DinnerPressed = false;
    private bool m_DentistPressed = false;
    //private int finishedCounter = 0;


    private BrushingTransition m_brushingTransition;

    private ScreenType characterNextScreen = ScreenType.Landing;



    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        CheckUnlock();
        CheckShineEffect();
    }

    public void ShowChracterSelection()
    {
        uiManager.MoveToScreen(ScreenType.CharacterSelection);
        characterNextScreen = ScreenType.CharacterSelection;
        VoiceOverManager.Instance.PlayCharacterSelection();
    }

    public void ShowIntro()
    {
        uiManager.MoveToScreen(ScreenType.Intro);
        characterNextScreen = ScreenType.Intro;
        Invoke("DelaySenjabChange", 0.0f);
        VoiceOverManager.Instance.PlayIntro();
    }

    public void ShowGameSelection()
    {
        uiManager.MoveToScreen(ScreenType.GameSelection);
        characterNextScreen = ScreenType.GameSelection;
        Invoke("DelaySenjabChange", 0.5f);
        Invoke("DelayCharacterChange", 0.5f);
        VoiceOverManager.Instance.StopVoiceOver();
        CheckUnlock();
        CheckShineEffect();
        SoundsManager.Instance.SwitchMusicLoops();
    }

    public void ShowBreakfast()
    {
        uiManager.MoveToScreen(ScreenType.Breakfast);
        characterNextScreen = ScreenType.Breakfast;
        Invoke("DelaySenjabChange", 0.2f);
        Invoke("DelayCharacterChange", 0.0f);
        VoiceOverManager.Instance.PlayBreakfastIntro();
        SoundsManager.Instance.SwitchMusicLoops();
    }

    public void ShowLaunch()
    {
        m_launchPressed = true;
        StopLunchShiny();
        uiManager.MoveToScreen(ScreenType.Launch);
        characterNextScreen = ScreenType.Launch;
        Invoke("DelaySenjabChange", 0.2f);
        Invoke("DelayCharacterChange", 0.2f);
        VoiceOverManager.Instance.PlayLunchIntro();
        SoundsManager.Instance.SwitchMusicLoops();
    }

    public void ShowDinner()
    {
        m_DinnerPressed = true;
        StopDinnerShiny();
        uiManager.MoveToScreen(ScreenType.Dinner);
        characterSelector.HideCharacter();
        VoiceOverManager.Instance.PlayDinnerIntro();
        SoundsManager.Instance.SwitchMusicLoops();
    }

    public void ShowTeethBrushing(float delayCamera)
    {
        Debug.Log("<b>Move to Teeth brushing</b>");
        characterNextScreen = ScreenType.BrushingTeeth;
        uiManager.MoveToScreen(ScreenType.BrushingTeeth);
        m_CameraController.SetCameraZoomToBrushing(delayCamera);
        VoiceOverManager.Instance.PlayTeethBrushingIntro();
        brushingTeethController.DeactivateButtonByStartTransition();
        //brushingTeethController.MoveStarCounterByTeethBrushing(2.0f);
        Invoke("DelayCharacterChange", 0.35f);
        SoundsManager.Instance.SwitchMusicLoops();
        //characterSelector.ShowCharacterInBrushing();
    }

    public void ShowDentist()
    {
        m_DentistPressed = true;
        StopDentistShiny();
        characterSelector.HideCharacter();
        uiManager.MoveToScreen(ScreenType.Dentist);
        dentistController.MakeDentistSequeceWithDelay(1.5f);
        VoiceOverManager.Instance.PlayDentistIntro();
        Invoke("DelaySinjabToDentist", 0.2f);
        SoundsManager.Instance.SwitchMusicLoops();
        dentistController.DisableButtonsAtStart();
        //characterSelector.ShowCharacterInDentist();
    }

    private void DelaySinjabToDentist()
    {
        m_SinjabController.SetSenjabToDentist();
    }

    public void ShowRating()
    {
        analyticsTracker.SubmitAllGamesAreFinished();
        uiManager.MoveToScreen(ScreenType.Rating);
        characterSelector.HideCharacter();
        SoundsManager.Instance.SwitchMusicLoops();
    }

    public void ShowMiniGame()
    {

        characterSelector.HideCharacter();
        uiManager.MoveToScreen(ScreenType.MiniGame);
        SoundsManager.Instance.SwitchMusicLoops();
    }

    private void DelaySenjabChange()
    {
        Debug.Log("DelaySenjabChange " + characterNextScreen);
        switch (characterNextScreen)
        {
            case ScreenType.Intro:
                m_SinjabController.SetSenjabToIntro();
                break;
            case ScreenType.GameSelection:
                characterSelector.HideCharacter();
                m_SinjabController.SetSenjabToGameSelection();
                break;
            case ScreenType.Breakfast:
                m_SinjabController.SetSenjabToBreakfast();
                break;
            case ScreenType.Launch:
                m_SinjabController.SetSenjabToLunch();
                break;
        }
    }

    private void DelayCharacterChange()
    {
        switch (characterNextScreen)
        {
            case ScreenType.GameSelection:
            case ScreenType.BrushingTeeth:
                characterSelector.HideCharacter();
                break;
            case ScreenType.Breakfast:
                characterSelector.ShowCharacterInBreakfast();
                break;
            case ScreenType.Launch:
                characterSelector.ShowCharacterInLaunch();
                break;

        }
    }

    public void ShowSinjabByBackButton(ScreenType nextScreen)
    {
        characterNextScreen = nextScreen;
        Invoke("DelaySenjabChange", 0.5f);
    }

    //----------------------------------- unlocking lunch and dinner
    public void SetBreakfastFinish()
    {
        m_wasBreakfastFinished = true;
        m_CanLunchShine = true;
        PlayerPrefs.SetInt("wasBreakfastFinished", 1);
        //IncreaseCounter();
        //UpdateStarCounter();
    }

    public void SetLunchFinish()
    {
        m_wasLunchFinished = true;
        m_CanDinnerShine = true;
        PlayerPrefs.SetInt("wasLunchFinished", 1);
        //IncreaseCounter();
        //UpdateStarCounter();
    }

    public void SetDinnerFinish()
    {
        m_wasDennierFinished = true;
        m_CanDentistShine = true;
        PlayerPrefs.SetInt("wasDennierFinished", 1);
        //IncreaseCounter();
        //UpdateStarCounter();
    }

    /// <summary>
    /// check the unlock of lunch and dinner. 
    /// </summary>
    public void CheckUnlock()
    {
        //unlock breakfast
        if (PlayerPrefs.HasKey("wasBreakfastFinished"))
        {
            m_wasBreakfastFinished = true;
            imglunchIcon.color = Color.white;
            btnlunch.interactable = true;
            lockLunchObject.SetActive(false);
        }
        else if (!m_wasBreakfastFinished)
        {
            imglunchIcon.color = deactiveColor;
            btnlunch.interactable = false;
            lockLunchObject.SetActive(true);
        }
        else
        {
            imglunchIcon.color = Color.white;
            btnlunch.interactable = true;
            lockLunchObject.SetActive(false);
        }
        //unlock lunch
        if (PlayerPrefs.HasKey("wasLunchFinished"))
        {
            m_wasLunchFinished = true;
            imgDinnerIcon.color = Color.white;
            btnDinner.interactable = true;
            lockDinnerObject.SetActive(false);
        }
        else if (!m_wasLunchFinished)
        {
            imgDinnerIcon.color = deactiveColor;
            btnDinner.interactable = false;
            lockDinnerObject.SetActive(true);
        }
        else
        {
            imgDinnerIcon.color = Color.white;
            btnDinner.interactable = true;
            lockDinnerObject.SetActive(false);
        }
        //unlock dinner
        if (PlayerPrefs.HasKey("wasDennierFinished"))
        {
            m_wasDennierFinished = true;
            imgDentistIcon.color = Color.white;
            btnDentist.interactable = true;
            lockDentistObject.SetActive(false);
        }
        else if (!m_wasDennierFinished)
        {
            imgDentistIcon.color = deactiveColor;
            btnDentist.interactable = false;
            lockDentistObject.SetActive(true);
        }
        else
        {
            imgDentistIcon.color = Color.white;
            btnDentist.interactable = true;
            lockDentistObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("canPlayMiniGame"))
        {
            imgMiniGameIcon.color = Color.white;
            btnMniGame.interactable = true;
            lockMiniGameObject.SetActive(false);
        }
        else
        {
            imgMiniGameIcon.color = deactiveColor;
            btnMniGame.interactable = false;
            lockMiniGameObject.SetActive(true);
        }
    }


    private void CheckShineEffect()
    {
        if (m_CanLunchShine && !m_launchPressed)
        {
            btnLaunchShiny.Play();
            iconLaunchShiny.Play();
        }
        else
        {
            btnLaunchShiny.Stop();
            iconLaunchShiny.Stop();
        }

        if (m_CanDinnerShine && !m_DinnerPressed)
        {
            btnDinnerShiny.Play();
            iconDinnerShiny.Play();
        }
        else
        {
            btnDinnerShiny.Stop();
            iconDinnerShiny.Stop();
        }

        if (m_CanDentistShine && !m_DentistPressed)
        {
            btnDentistShiny.Play();
        }
        else
        {
            btnDentistShiny.Stop();
        }
    }

    private void StopLunchShiny()
    {
        btnLaunchShiny.Stop();
        iconLaunchShiny.Stop();
    }

    private void StopDinnerShiny()
    {
        btnDinnerShiny.Stop();
        iconDinnerShiny.Stop();
    }

    private void StopDentistShiny()
    {
        btnDentistShiny.Stop();
    }

    //----------------------------------- brushing

    public void SetBrushTransition(BrushingTransition transition)
    {
        m_brushingTransition = transition;
    }

    public void SetBathroomTime(bool isDay)
    {
        bathroomNightController.SetDayTime(isDay);
    }

    public void FinishBrushing()
    {
        switch (m_brushingTransition)
        {
            case BrushingTransition.FromBreakfast:
                SetBreakfastFinish();
                break;
            case BrushingTransition.FromLunch:
                //SetLunchFinish();
                break;
        }
        m_brushingTransition = BrushingTransition.idle;
    }

    //----------------------------------- MiniGame

    //private void IncreaseCounter()
    //{
    //    finishedCounter++;
    //}

    //private void UpdateStarCounter()
    //{
    //    m_starCounter.SetStarCounter(finishedCounter);
    //}


}

public enum BrushingTransition
{
    idle,
    FromBreakfast,
    FromLunch,
}

public enum CharacterType
{
    Male,
    Female
}