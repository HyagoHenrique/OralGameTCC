using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DentistController : MonoBehaviour
{
    public GameManager gameManager;
    public Transform transBtnRating;
    public SinjabController sinjabController;
    public List<Transform> lstTools = new List<Transform>();
    public float selectionOffset;
    public ParticleSystem waterParticle;
    public ParticleSystem brushBuff;
    [Header("tools animation")]
    public GafCharacterAnimator mirrorAnimator;
    public GafCharacterAnimator brushAnimator;
    public List<Button> lstButtons = new List<Button>();
    public List<Image> lstImages = new List<Image>();
    public List<GAF.Core.GAFMovieClip> lstGafAnim;
    public Color colInActive;
    public StarCounterController starCounter;
    public AudioClip clpBrushing;
    public AudioClip clpMiniBrushFinish;

    private List<DentistTool> lstAnswerdSteps = new List<DentistTool>();
    private bool canTakeInput = false;

    private DentistTool[] correctAnswer = new DentistTool[] { DentistTool.Mirror };

    private List<Vector3> lstOrigianlToolsPos = new List<Vector3>();

    private Coroutine corMirrorAnim;
    //private Coroutine corMiniBrushAnim;

    private AudioSource m_Audiosource;

    // Start is called before the first frame update
    void Start()
    {
        m_Audiosource = gameObject.AddComponent<AudioSource>();
        m_Audiosource.playOnAwake = false;
        m_Audiosource.clip = clpBrushing;
        m_Audiosource.loop = true;
        m_Audiosource.volume = 0.2f;

        for (int i = 0; i < lstTools.Count; i++)
        {
            lstOrigianlToolsPos.Add(lstTools[i].localPosition);
        }
    }

    public void MakeDentistSequeceWithDelay(float delay)
    {
        StartCoroutine(MakeSequence(delay));
    }

    private IEnumerator MakeSequence(float delay)
    {

        yield return new WaitForSeconds(delay);
        canTakeInput = true;
        //wait till player click on all ltools
        while (lstAnswerdSteps.Count < 4)
        {
            yield return null;
        }
        PlayerPrefs.SetInt("canPlayMiniGame", 1);
        starCounter.AddStar();
        //show last VO message
        VoiceOverManager.Instance.PlayDentistMiniBrushEnd();
        sinjabController.SetSenjabTalking(VoiceOverManager.Instance.GetCurrentVOLength());
        MiniBrushingAnimation();
        DisableAllButtonsExpectMiniBrush();
        float voWaitTimer = VoiceOverManager.Instance.GetCurrentVOLength();
        Invoke("PlayMiniBrushFinishDelay", 5.5f);
        yield return new WaitForSeconds(voWaitTimer * 0.35f);
        EndMiniBrushAnimation();
        yield return new WaitForSeconds(0.3f);
        DeselectTool(DentistTool.MiniBrush);
        DisableAllButtonsImediatlly((voWaitTimer * 0.7f) + 1.0f);
        yield return new WaitForSeconds((voWaitTimer * 0.7f) + 1.0f);
        //check if Player can move to rating or can play miniGame first
        VoiceOverManager.Instance.PlayMiniGameIntro();
        ShowMiniGame();
    }

    private void PlayMiniBrushFinishDelay()
    {
        m_Audiosource.clip = clpMiniBrushFinish;
        m_Audiosource.volume = 0.5f;
        m_Audiosource.loop = false;
        m_Audiosource.Play();
    }

    private void CheckAnswer(DentistTool selectedTool)
    {
        Debug.Log("Checking answer : " + selectedTool);
        if (IsAnswerCorrect(selectedTool))
        {
            ActivateSelectedTool(selectedTool);
            lstAnswerdSteps.Add(selectedTool);
            switch (selectedTool)
            {
                case DentistTool.Mirror:
                    starCounter.AddStar();
                    correctAnswer = new DentistTool[] { DentistTool.Water, DentistTool.ToothBrush };
                    VoiceOverManager.Instance.PlayDentistMirrorEnd();
                    sinjabController.SetSenjabTalking(VoiceOverManager.Instance.GetCurrentVOLength());
                    corMirrorAnim = StartCoroutine(LoopMirrorAnimation());
                    DisableAllButtonsExpectMirror();
                    break;
                case DentistTool.Water:
                    starCounter.AddStar();
                    if (lstAnswerdSteps.Contains(DentistTool.ToothBrush))
                    {
                        correctAnswer = new DentistTool[] { DentistTool.MiniBrush };
                    }
                    else
                    {
                        correctAnswer = new DentistTool[] { DentistTool.ToothBrush };
                    }
                    VoiceOverManager.Instance.PlayDentistWaterEnd();

                    if (correctAnswer[0] == DentistTool.ToothBrush)
                    {
                        StartCoroutine(VoiceOverManager.Instance.PlayDentistNextTool(VoiceOverManager.Instance.GetCurrentVOLength()));
                        sinjabController.SetSenjabTalking(VoiceOverManager.Instance.GetCurrentVOLength() + 5.1f);
                        DisableAllButtonsExpectWater(5.1f);
                    }
                    else
                    {
                        StartCoroutine(VoiceOverManager.Instance.PlayDentistMiniBrushTransition(VoiceOverManager.Instance.GetCurrentVOLength()));
                        sinjabController.SetSenjabTalking(VoiceOverManager.Instance.GetCurrentVOLength() + 11.1f);
                        DisableAllButtonsExpectWater(11.1f);
                    }


                    //EndMirrorAnimation();

                    break;
                case DentistTool.ToothBrush:
                    starCounter.AddStar();

                    if (lstAnswerdSteps.Contains(DentistTool.Water))
                    {
                        correctAnswer = new DentistTool[] { DentistTool.MiniBrush };
                    }
                    else
                    {
                        correctAnswer = new DentistTool[] { DentistTool.Water };
                    }
                    VoiceOverManager.Instance.PlayDentistToothBrushEnd();
                    Debug.Log("tooth brush tool is Selected next shit is " + correctAnswer[0]);

                    float stopDelay = 0.0f;
                    if (correctAnswer[0] == DentistTool.Water)
                    {
                        StartCoroutine(VoiceOverManager.Instance.PlayDentistNextTool(VoiceOverManager.Instance.GetCurrentVOLength()));
                        sinjabController.SetSenjabTalking(VoiceOverManager.Instance.GetCurrentVOLength() + 5.1f);
                        DisableAllButtonsExpectToothBrush(5.1f);
                        stopDelay = 5.1f;
                    }
                    else
                    {
                        StartCoroutine(VoiceOverManager.Instance.PlayDentistMiniBrushTransition(VoiceOverManager.Instance.GetCurrentVOLength()));
                        sinjabController.SetSenjabTalking(VoiceOverManager.Instance.GetCurrentVOLength() + 11.1f);
                        DisableAllButtonsExpectToothBrush(11.1f);
                        stopDelay = 11.1f;
                    }

                    //play the brushing sound clip
                    m_Audiosource.clip = clpBrushing;
                    m_Audiosource.volume = 0.2f;
                    m_Audiosource.loop = true;
                    m_Audiosource.Play();
                    Invoke("StopBrushingSound", VoiceOverManager.Instance.GetCurrentVOLength()+ stopDelay);
                    break;
            }
        }
        else
        {
            float length = 0.0f;
            VoiceOverManager.Instance.PlayDentistRandomWrongVoice(out length);
            sinjabController.SetSenjabTalking(length);
        }
    }

    private bool IsAnswerCorrect(DentistTool selectedTool)
    {
        for (int i = 0; i < correctAnswer.Length; i++)
        {
            if (selectedTool == correctAnswer[i])
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator LoopMirrorAnimation()
    {
        mirrorAnimator.PlayAnimation("Mirror_animation", 1.0f);
        yield return new WaitForSeconds(VoiceOverManager.Instance.GetCurrentVOLength());
        mirrorAnimator.PlayAnimation("Mirror_idle", 0.0f);
        yield return new WaitForSeconds(0.01f);
        DeselectTool(DentistTool.Mirror);
    }

    private void EndMirrorAnimation()
    {
        StopCoroutine(corMirrorAnim);
        mirrorAnimator.PlayAnimation("Mirror_idle", 0.0f);
    }

    private void MiniBrushingAnimation()
    {
        brushAnimator.PlayAnimation("Brushing", 1.0f);
    }

    private void EndMiniBrushAnimation()
    {
        brushAnimator.PlayAnimation("Brushing_idle", 0.0f);
    }

    private void ShowMiniGame()
    {
        gameManager.ShowMiniGame();
        //if (gameManager.CanPlayMiniGame())
        //{
        //}
        //else
        //{
        //    //MoveToRating();
        //}
    }

    private void MoveToRating()
    {
        ShowRatingWindowButton();
    }

    private void ShowRatingWindowButton()
    {
        transBtnRating.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);
    }

    private void ActivateSelectedTool(DentistTool tool)
    {
        //reset all tools into original position
        for (int i = 0; i < lstTools.Count; i++)
        {
            lstTools[i].localPosition = lstOrigianlToolsPos[i];
        }
        //Tweenthe selected tool up
        switch (tool)
        {
            case DentistTool.Mirror:
                lstTools[0].DOLocalMoveY(lstTools[0].localPosition.y + selectionOffset, 0.35f).SetEase(Ease.InOutCubic);
                lstTools[0].DOScale(lstTools[0].localScale * 1.2f, 0.35f).SetEase(Ease.InOutCubic);
                break;
            case DentistTool.Water:
                lstTools[1].DOLocalMoveY(lstTools[1].localPosition.y + selectionOffset, 0.35f).SetEase(Ease.InOutCubic);
                lstTools[1].DOScale(lstTools[1].localScale * 1.2f, 0.35f).SetEase(Ease.InOutCubic);
                waterParticle.Play();
                break;
            case DentistTool.ToothBrush:
                lstTools[2].DOLocalMoveY(lstTools[2].localPosition.y + selectionOffset, 0.35f).SetEase(Ease.InOutCubic);
                lstTools[2].DOScale(lstTools[2].localScale * 1.2f, 0.35f).SetEase(Ease.InOutCubic);
                brushBuff.Play();
                break;
            case DentistTool.MiniBrush:
                lstTools[3].DOLocalMoveY(lstTools[3].localPosition.y - selectionOffset, 0.35f).SetEase(Ease.InOutCubic);
                //lstTools[3].DOScale(lstTools[3].localScale * 1.2f, 0.35f).SetEase(Ease.InOutCubic);
                break;
        }
    }

    private void DeselectTool(DentistTool tool)
    {
        switch (tool)
        {
            case DentistTool.Mirror:
                lstTools[0].DOLocalMove(lstOrigianlToolsPos[0], 0.25f).SetEase(Ease.InOutCubic);
                lstTools[0].DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutCubic);
                break;
            case DentistTool.Water:
                lstTools[1].DOLocalMove(lstOrigianlToolsPos[1], 0.25f).SetEase(Ease.InOutCubic);
                lstTools[1].DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutCubic);
                waterParticle.Stop();
                break;
            case DentistTool.ToothBrush:
                lstTools[2].DOLocalMove(lstOrigianlToolsPos[2], 0.25f).SetEase(Ease.InOutCubic);
                lstTools[2].DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutCubic);
                brushBuff.Stop();
                break;
            case DentistTool.MiniBrush:
                lstTools[3].DOLocalMove(lstOrigianlToolsPos[3], 0.25f).SetEase(Ease.InOutCubic);
                //lstTools[3].DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutCubic);
                break;
        }
    }

    public void DisableButtonsAtStart()
    {
        StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength() + 3.5f, -1, -1, DentistTool.EmptyTool));
    }

    public void DisableAllButtonsImediatlly(float disableLength)
    {
        Debug.Log("disable All buttons Imediatlly : ");
        StartCoroutine(LoopButtonsActivation(disableLength, -1, -1, DentistTool.EmptyTool));
    }

    private void DisableAllButtonsExpectMirror()
    {
        StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength(), 0, -1, DentistTool.Mirror));
    }

    private void DisableAllButtonsExpectWater(float addedDelay = 0.0f)
    {
        StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength() + addedDelay, -1, 0, DentistTool.Water));
    }

    private void DisableAllButtonsExpectToothBrush(float addedDelay = 0.0f)
    {
        StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength() + addedDelay, -1, 1, DentistTool.ToothBrush));
    }

    private void DisableAllButtonsExpectMiniBrush()
    {
        StartCoroutine(LoopButtonsActivation(VoiceOverManager.Instance.GetCurrentVOLength(), 1, -1, DentistTool.MiniBrush));
    }

    private IEnumerator LoopButtonsActivation(float waite, int gafSkipIndex, int imgSkipIndex, DentistTool tool)
    {
        SetAllButtonsActive(false, gafSkipIndex, imgSkipIndex);
        yield return new WaitForSeconds(waite);
        SetAllButtonsActive(true, gafSkipIndex, imgSkipIndex);
        DeselectTool(tool);
    }

    private void SetAllButtonsActive(bool isActive, int gafSkipIndex, int imgSkipIndex)
    {
        //Debug.Log("setting DentistButtons : " + isActive);
        for (int i = 0; i < lstButtons.Count; i++)
        {
            lstButtons[i].interactable = isActive;
        }
        for (int i = 0; i < lstImages.Count; i++)
        {
            if (i == imgSkipIndex)
            {
                continue;
            }
            if (!isActive)
            {
                lstImages[i].color = colInActive;
            }
            else
            {
                lstImages[i].color = Color.white;
            }
        }
        for (int i = 0; i < lstGafAnim.Count; i++)
        {
            if (i == gafSkipIndex)
            {
                continue;
            }
            if (!isActive)
            {
                lstGafAnim[i].setColorAndOffset(colInActive, Vector4.zero);
            }
            else
            {
                lstGafAnim[i].setColorAndOffset(Color.white, Vector4.zero);
            }
        }
    }

    private void StopBrushingSound()
    {
        //Stop the brushing sound clip
        m_Audiosource.Stop();
    }

    //---------------------------------------- Buttons

    public void BtnMirror_OnClick()
    {
        if (canTakeInput && !lstAnswerdSteps.Contains(DentistTool.Mirror))
            CheckAnswer(DentistTool.Mirror);
    }

    public void BtnWater_OnClick()
    {
        if (canTakeInput && !lstAnswerdSteps.Contains(DentistTool.Water))
            CheckAnswer(DentistTool.Water);
    }

    public void BtnToothBrush_OnClick()
    {
        if (canTakeInput && !lstAnswerdSteps.Contains(DentistTool.ToothBrush))
            CheckAnswer(DentistTool.ToothBrush);
    }

    public void BtnMiniBrush_OnClick()
    {
        if (canTakeInput && !lstAnswerdSteps.Contains(DentistTool.MiniBrush))
            CheckAnswer(DentistTool.MiniBrush);
    }

    public void BtnRating_OnClick()
    {
        gameManager.ShowRating();
    }

}

public enum DentistTool
{
    Mirror,
    Water,
    ToothBrush,
    MiniBrush,
    EmptyTool
}
