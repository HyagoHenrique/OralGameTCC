using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DinnerScreen : UIScreen
{
    [Header("Instruction items")]
    public Transform instructionParent;
    public Transform[] instructionItems;
    [Header("Excersize items")]
    public Transform excersizeParent;
    //public Transform transSlider;
    public Transform transBack;

    public DinnerController dinnerController;

    private bool canMoveNextToExersize = false;
    private Coroutine corScreenSequence;
    private List<int> tweenIDs = new List<int>();

    public override void Start()
    {
        base.Start();
        for (int i = 0; i < instructionItems.Length; i++)
        {
            tweenIDs.Add(Random.Range(123456, 654321));
        }
    }

    public override void ShowScreen()
    {
        corScreenSequence = StartCoroutine(MakeScreenSequence());
    }

    private IEnumerator MakeScreenSequence()
    {
        screenParent.SetActive(true);
        _screenState = ScreenState.Animating;
        instructionParent.gameObject.SetActive(true);
        float totalDelay = 0.0f;

        #region Instruction
        yield return new WaitForSeconds(0.01f);
        totalDelay = TweenInstruction(1.0f, Ease.OutBack);
        yield return new WaitForSeconds(0.6f + totalDelay);
        _screenState = ScreenState.Visible;

        //wait for while then hide the instruction to show the main excersizes
        yield return new WaitUntil(CanMoveNext);

        totalDelay = TweenInstruction(0.0f, Ease.InBack);
        yield return new WaitForSeconds(totalDelay + 0.6f);
        instructionParent.gameObject.SetActive(false);
        //tweenIDs.Clear();
        #endregion


        #region Excersize
        excersizeParent.gameObject.SetActive(true);
        dinnerController.Init();
        //transSlider.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
        transBack.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack).SetDelay(0.15f);
        #endregion

    }

    private float TweenInstruction(float scaleVal, Ease ease)
    {
        float delay = 0.0f;

        for (int i = 0; i < instructionItems.Length; i++)
        {
            instructionItems[i].DOScale(scaleVal, 0.5f).SetDelay(delay).SetEase(ease).SetId(tweenIDs[i]);
            delay += 0.1f;
        }
        return delay;
    }

    private bool CanMoveNext()
    {
        return canMoveNextToExersize;
    }

    public void BtnFinishInstruction_OnClick()
    {
        canMoveNextToExersize = true;
    }


    public override void ResetScreen()
    {
        for (int i = 0; i < instructionItems.Length; i++)
        {
            instructionItems[i].localScale = Vector3.zero;
        }

        instructionParent.gameObject.SetActive(false);

        if (corScreenSequence != null)
            StopCoroutine(corScreenSequence);

        for (int i = 0; i < tweenIDs.Count; i++)
        {
            DOTween.Kill(tweenIDs);
        }
        canMoveNextToExersize = false;
        //transSlider.localScale = Vector3.zero;
        base.ResetScreen();
    }

    public void ResetByBack()
    {
        transBack.localScale = Vector3.zero;
    }
}
