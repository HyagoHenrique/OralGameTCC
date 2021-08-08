using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DinnerController : MonoBehaviour, IScreenRset
{

    public GameManager gameManager;
    public ExcersizeItems[] arrExcersieItems;
    public Transform[] transFirstExcItems;
    //public Transform transDentist;
    //public Transform transStar;

    public Image imgFirstItem;
    public TextMeshProUGUI txtFirstItem;

    //public Slider starsSlider;

    public ParticleSystem correctAnswer;

    public StarCounterController starCounterController;

    //[Header("star anim")]
    //public float starAnimDuration;
    //public float starRotation;
    //public float starYOffset;
    //public Ease starRotationEase;
    //public AnimationCurve starCurve;

    ////used to determine the correc answer  for the star slider
    //private Dictionary<string, bool> dicCorrectAnswers = new Dictionary<string, bool>();



    private List<int> lstHistory = new List<int>();
    private int answerCounter = 0;

    private ExcersizeItems firstExcItem;
    //private ExcersizeItems secondExcItem;

    private Coroutine corExcerSwitch;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init()
    {
        firstExcItem = GetRandomItem();
        //secondExcItem = GetRandomItem();
        InitFirstItemDetails();
        //InitSecondItemDetails();
        TweenFirstExcersizeItems(1.0f, Ease.OutBack);
        starCounterController.transform.DOScale(1.0f, 0.35f).SetDelay(0.5f).SetEase(Ease.OutBack);
        //TweenSecondExcersizeItems(1.0f, Ease.OutBack);
    }

    private void InitFirstItemDetails()
    {
        imgFirstItem.sprite = firstExcItem.spItem;
        txtFirstItem.text = firstExcItem.itemName;
    }

    private void ReInitializeDinnerItem()
    {
        ExcersizeItems tempItem = GetRandomItem();
        //hide the excercise group because all items was taken
        if (tempItem == null)
        {
            TweenFirstExcersizeItems(0.0f, Ease.InBack);
        }
        else
        {
            firstExcItem = tempItem;
            corExcerSwitch = StartCoroutine(MakeExcersizeSwithSequence());
        }
    }

    private ExcersizeItems GetRandomItem()
    {
        //check if there is no any items left to show
        if (lstHistory.Count == arrExcersieItems.Length)
        {
            return null;
        }
        else
        {
            int rndIndex = Random.Range(0, arrExcersieItems.Length);
            while (lstHistory.Contains(rndIndex))
            {
                rndIndex = Random.Range(0, arrExcersieItems.Length);
            }
            lstHistory.Add(rndIndex);
            return arrExcersieItems[rndIndex];
        }
    }

    private float TweenFirstExcersizeItems(float scaleValue, Ease ease)
    {
        float delay = 0.0f;

        for (int i = 0; i < transFirstExcItems.Length; i++)
        {
            transFirstExcItems[i].DOScale(scaleValue, 0.5f).SetDelay(delay).SetEase(ease);
            delay += 0.1f;
        }
        return delay;
    }

    private IEnumerator MakeExcersizeSwithSequence()
    {
        //first hide the excersize item
        TweenFirstExcersizeItems(0.0f, Ease.InBack);

        yield return new WaitForSeconds(1.5f);
        //initialize the text and the sprite
        InitFirstItemDetails();

        yield return new WaitForSeconds(0.1f);
        //show the excersize again
        TweenFirstExcersizeItems(1.0f, Ease.OutBack);
    }

    public void VerifyAnswer(EatStatus eatStatus)
    {
        bool isAnswerCorrect = false;
        string itemName = firstExcItem.itemName;
        if (firstExcItem.answer == eatStatus)
        {
            answerCounter++;
            isAnswerCorrect = true;
            ReInitializeDinnerItem();
            ShowCorrectEffect(eatStatus);
            starCounterController.AddStar();
            //if (eatStatus != EatStatus.CanntEat)
            //{ }
            VoiceOverManager.Instance.PlayDinnerRandomCorrect(eatStatus);
            SoundsManager.Instance.PlaySFXCorrect();
        }
        else
        {
            starCounterController.RemoveStar();
            ShowWrongEffect(eatStatus);
            VoiceOverManager.Instance.PlayDinnerRandomWrong();
            SoundsManager.Instance.PlaySFXWrong();
        }

        ////add the answer to make sure not to consider worng answers after player answer them as correct for the second time
        //if (!dicCorrectAnswers.ContainsKey(itemName))
        //{
        //    dicCorrectAnswers.Add(itemName, isAnswerCorrect);
        //}
        //CheckAnswersAndUpdateStarsUI();
        CheckMovingToGameSelection();
    }

    private void ShowCorrectEffect(EatStatus eatStatus)
    {
        switch (eatStatus)
        {
            case EatStatus.CanEat:
                correctAnswer.transform.position = transFirstExcItems[5].position;
                break;
            case EatStatus.CanntEat:
                correctAnswer.transform.position = transFirstExcItems[3].position;
                break;
            case EatStatus.SometimeEat:
                correctAnswer.transform.position = transFirstExcItems[4].position;
                break;
        }
        correctAnswer.Play();
    }

    private void ShowWrongEffect(EatStatus eatStatus)
    {
        switch (eatStatus)
        {
            case EatStatus.CanEat:
                transFirstExcItems[5].DOPunchScale(Vector3.one * 0.1f, 0.35f);
                break;
            case EatStatus.CanntEat:
                transFirstExcItems[3].DOPunchScale(Vector3.one * 0.1f, 0.35f);
                break;
            case EatStatus.SometimeEat:
                transFirstExcItems[4].DOPunchScale(Vector3.one * 0.1f, 0.35f);
                break;
        }
    }

    //Moving back to game selection after dinner is finished
    private void CheckMovingToGameSelection()
    {
        //float delayValue = 0.0f;
        //if (starsSlider.value >= 0.95f)
        //{
        //    delayValue = 5.0f;
        //    gameManager.ActivateMiniGame();
        //    MakeStarRotation();
        //}
        //else
        //{
        //    delayValue = 1.0f;
        //}
        if (answerCounter == arrExcersieItems.Length)
        {
            gameManager.SetDinnerFinish();
            Invoke("ShowTeethBrushingWithDelay", VoiceOverManager.Instance.GetCurrentVOLength());
        }
    }

    private void ShowTeethBrushingWithDelay()
    {
        gameManager.ShowTeethBrushing(1.0f);
    }

    //private void CheckAnswersAndUpdateStarsUI()
    //{
    //    int correctCount = 0;
    //    foreach (KeyValuePair<string, bool> item in dicCorrectAnswers)
    //    {
    //        if (item.Value)
    //        {
    //            correctCount++;
    //        }
    //    }
    //    float percentage = (float)correctCount / (float)arrExcersieItems.Length;
    //    starsSlider.value = percentage;
    //}

    ////make star rotation as indicator that player win 
    //private void MakeStarRotation()
    //{
    //    transStar.DORotate(new Vector3(0.0f, starRotation, 0.0f), starAnimDuration, RotateMode.LocalAxisAdd).SetEase(starRotationEase);
    //    transStar.DOLocalMoveY(transStar.localPosition.y + starYOffset, starAnimDuration).SetLoops(1, LoopType.Yoyo).SetEase(starCurve);
    //    correctAnswer.transform.position = transStar.position;
    //    correctAnswer.Play();
    //}

    //private void ShowDentistWithDelay()
    //{
    //    gameManager.ShowDentist();
    //}

    public void OnScreenRset()
    {
        ResetToDefualt();
    }

    private void ResetToDefualt()
    {
        if (corExcerSwitch != null)
            StopCoroutine(corExcerSwitch);
        DOTween.KillAll();
        lstHistory.Clear();
        answerCounter = 0;
        //dicCorrectAnswers.Clear();
        //starsSlider.value = 0.0f;
        firstExcItem = null;
        imgFirstItem.sprite = null;
        txtFirstItem.text = string.Empty;
        starCounterController.transform.localScale = Vector3.zero;
        for (int i = 0; i < transFirstExcItems.Length; i++)
        {
            transFirstExcItems[i].localScale = Vector3.zero;
        }
    }

    public void RsetByBack()
    {
        ResetToDefualt();
    }


    /// -------------------------------------------------------- item events

    public void BtnFirstCanEat_OnClick()
    {
        VerifyAnswer(EatStatus.CanEat);
    }

    public void BtnFirstCanntEat_OnClick()
    {
        VerifyAnswer(EatStatus.CanntEat);
    }

    public void BtnFirstSometimeEat_OnClick()
    {
        VerifyAnswer(EatStatus.SometimeEat);
    }

    public void DisableButtonsWithDelay()
    {
        StartCoroutine(LoopButtonsActivation());
    }

    private IEnumerator LoopButtonsActivation()
    {
        SetButtonsActive(false);
        yield return new WaitForSeconds(2.0f);
        SetButtonsActive(true);
    }

    private void SetButtonsActive(bool isActive)
    {
        for (int i = 3; i < transFirstExcItems.Length; i++)
        {
            transFirstExcItems[i].GetComponent<Button>().interactable = isActive;
        }
    }

    /// -------------------------------------------------------- item events

    public void BtnInstructionCanEat_OnClick()
    {
        VoiceOverManager.Instance.PlayInstructionCanEat();
    }

    public void BtnInstructionCantEat_OnClick()
    {
        VoiceOverManager.Instance.PlayInstructionCantEat();
    }

    public void BtnInstructionSomeTimeEat_OnClick()
    {
        VoiceOverManager.Instance.PlayInstructionSometimeEat();
    }

    //public void BtnDentist_OnClick()
    //{
    //    gameManager.ShowDentist();
    //}


}

public enum EatStatus
{
    CanEat,
    CanntEat,
    SometimeEat
}

[System.Serializable]
public class ExcersizeItems
{
    public string itemName;
    public Sprite spItem;
    public EatStatus answer;
}