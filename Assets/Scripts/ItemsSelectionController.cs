using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemsSelectionController : MonoBehaviour, IScreenRset
{
    public GameManager gameManager;
    public StarCounterController starCounter;
    public List<Image> lstSeletedItems;
    //public Transform transButtonNext;
    public Image imgMoving;
    public ParticleSystem particleTrail;
    public List<SelectionItem> lstSelectionItems;

    public bool isBreakfast = false;
    public int correctCount;
    public SinjabController sinjabController;

    public List<SelectableItem> lstScreenItems = new List<SelectableItem>();

    private int lastAddedItem = 0;
    private List<string> lstAddedItems = new List<string>();


    private Sprite originalSprite;
    private Vector3 originalMovingPosition;
    private Vector3 originalMovingSize;

    // Start is called before the first frame update
    void Start()
    {
        originalSprite = lstSeletedItems[0].sprite;
        originalMovingPosition = imgMoving.transform.position;
        originalMovingSize = imgMoving.GetComponent<RectTransform>().sizeDelta;
    }

    public void InitItemsOnShow()
    {
        List<int> lstRnd = new List<int>();
        int selectionIndex = 0;
        while (selectionIndex < lstSelectionItems.Count)
        {
            int randIndex = Random.Range(0, lstScreenItems.Count);
            while (lstRnd.Contains(randIndex))
            {
                randIndex = Random.Range(0, lstScreenItems.Count);
            }
            lstRnd.Add(randIndex);
            //init the random element
            lstSelectionItems[selectionIndex].InitItem(lstScreenItems[randIndex].itemSprite, lstScreenItems[randIndex].isCorrect);
            lstSelectionItems[selectionIndex].GetComponent<VoiceBreakfastItemPlay>().strVoiceName = lstScreenItems[randIndex].itemName;
            selectionIndex++;
        }
    }


    public void AddItemToSelection(Sprite spItem, Transform transItem)
    {
        if (!lstAddedItems.Contains(spItem.name))
        {
            AnimateSprite(transItem, lstSeletedItems[lastAddedItem].transform, spItem, lastAddedItem);
            lstAddedItems.Add(spItem.name);
            lastAddedItem++;
            starCounter.AddStar();
        }
        if (lastAddedItem == correctCount)
        {
            if (isBreakfast)
            {
                gameManager.SetBrushTransition(BrushingTransition.FromBreakfast);
                gameManager.SetBathroomTime(true);
                StartCoroutine(MoveToBrushingFromBreakfast());
                //Invoke("DealyTransitionToBrushingVoice", 3.5f);
                //Invoke("DelayTeethBrushing", 13.0f);
                //Invoke("DelaySinjabTalkingBreakfast", 3.7f);
            }
            else
            {
                //Debug.Log("<b>Lunch was finished </b> " + VoiceOverManager.Instance.GetCurrentVOLength());
                //gameManager.SetBrushTransition(BrushingTransition.FromLunch);
                //Invoke("DealyTransitionToBrushingVoice", 3.5f);
                //Invoke("DelaySinjabTalkingLunch", 3.5f);
                gameManager.SetBathroomTime(false);
                gameManager.SetLunchFinish();
                Invoke("DelayMoveToGameSelection", VoiceOverManager.Instance.GetCurrentVOLength() + 0.2f);
            }
        }
    }

    private IEnumerator MoveToBrushingFromBreakfast()
    {
        //delay till finish alst itme VO
        yield return new WaitForSeconds(VoiceOverManager.Instance.GetCurrentVOLength());
        VoiceOverManager.Instance.PlayBrushingTransition();
        sinjabController.SetSenjabTalking(VoiceOverManager.Instance.GetCurrentVOLength());
        //delay till finish transition VO
        yield return new WaitForSeconds(VoiceOverManager.Instance.GetCurrentVOLength());
        gameManager.ShowTeethBrushing(3.0f);
    }

    //private void DelaySinjabTalkingBreakfast()
    //{
    //    sinjabController.SetSenjabTalking(13.0f);
    //}

    public void AnimateSprite(Transform transSpriteOriginal, Transform transTopItem, Sprite spItem, int topItemIndex)
    {
        particleTrail.Play();
        imgMoving.transform.position = transSpriteOriginal.position;
        imgMoving.sprite = spItem;
        imgMoving.GetComponent<RectTransform>().DOSizeDelta(lstSeletedItems[topItemIndex].GetComponent<RectTransform>().sizeDelta, 0.5f);
        imgMoving.transform.DOMove(transTopItem.position, 0.5f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            particleTrail.Stop();
            lstSeletedItems[topItemIndex].sprite = spItem;
            imgMoving.transform.position = originalMovingPosition;
            imgMoving.GetComponent<RectTransform>().sizeDelta = originalMovingSize;
        });
    }

    //public void DealyTransitionToBrushingVoice()
    //{
    //    VoiceOverManager.Instance.PlayBrushingTransition();
    //}

    public void DelayMoveToGameSelection()
    {
        gameManager.ShowGameSelection();
    }

    //public void DelayTeethBrushing()
    //{
    //    gameManager.ShowTeethBrushing(3.0f);
    //}

    //deactivate all items and then reactivate them
    public void MakeActivationLoop(float waitTime)
    {
        StartCoroutine(ActivationLoop(waitTime));
    }

    private IEnumerator ActivationLoop(float waitTime)
    {
        SetItemsInteractable(false);
        yield return new WaitForSeconds(waitTime);
        SetItemsInteractable(true);
    }

    private void SetItemsInteractable(bool isInteractable)
    {
        for (int i = 0; i < lstSelectionItems.Count; i++)
        {
            lstSelectionItems[i].SetInteractable(isInteractable);
        }
    }

    public void BtnDinner_OnClick()
    {
        gameManager.ShowDinner();
    }

    public void OnScreenRset()
    {
        ResetToDefault();
    }

    public void ResetToDefault()
    {
        lastAddedItem = 0;
        lstAddedItems.Clear();
        for (int i = 0; i < lstSeletedItems.Count; i++)
        {
            lstSeletedItems[i].sprite = originalSprite;
        }
        for (int i = 0; i < lstSelectionItems.Count; i++)
        {
            lstSelectionItems[i].ResetItem();
        }
        //transButtonNext.localScale = Vector3.zero;
    }

    [System.Serializable]
    public class SelectableItem
    {
        public string itemName;
        public Sprite itemSprite;
        public bool isCorrect;
    }
}
