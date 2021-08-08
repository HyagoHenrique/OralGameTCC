using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MiniGameController : MonoBehaviour
{
    public GameManager gameManager;

    public List<MiniGameItem> arrAllItems;
    //items used as pool
    public List<MniGameFlyingItem> lstBubbles;


    [Header("Random bound")]
    [SerializeField]
    private float minXPos;
    [SerializeField]
    private float maxXPos;
    [SerializeField]
    private float yPos;
    [Header("UI elements")]
    [SerializeField]
    private Transform miniStartButton;
    [SerializeField]
    private Transform miniTimer;
    [SerializeField]
    private Transform miniCoins;

    [SerializeField]
    private Transform miniStartLable;
    [SerializeField]
    private Transform gameOverObjects;
    [Header("Timer")]
    [SerializeField]
    private float gameLength;
    [SerializeField]
    private MiniGameTimer miniGameTimer;
    private bool isPlaying = false;
    private float trackingTimer = 0.0f;


    private List<int> lstHistory = new List<int>();

    private Coroutine corGameLoop;
    private Coroutine corTimer;

    // Start is called before the first frame update
    void Start()
    {
        SetGameTimer();
    }

    private void SetGameTimer()
    {
        miniGameTimer.SetGameTime(gameLength);
    }

    private void StartGame()
    {
        isPlaying = true;
        corGameLoop = StartCoroutine(MakeItemsLoop());
        corTimer = StartCoroutine(TrackTimer());
        miniGameTimer.StartTimer();
    }

    private IEnumerator TrackTimer()
    {
        while (trackingTimer < gameLength)
        {
            trackingTimer = Mathf.Clamp(trackingTimer + Time.deltaTime, 0.0f, 200.0f);
            yield return null;
        }
        isPlaying = false;
    }

    private IEnumerator MakeItemsLoop()
    {
        while (isPlaying)
        {
            //get random item
            int randIndex = Random.Range(0, arrAllItems.Count);
            //make sure not ot duplicate same item
            while (lstHistory.Contains(randIndex))
            {
                randIndex = Random.Range(0, arrAllItems.Count);
            }
            lstHistory.Add(randIndex);
            //if history reach 5 remove first item
            if (lstHistory.Count >= 5)
            {
                lstHistory.RemoveAt(0);
            }

            //create new item
            MniGameFlyingItem item = GetUnUsedBubble();
            if (item != null)
            {
                item.InitItem(arrAllItems[randIndex].itemSprite, arrAllItems[randIndex].isCorrect);
                item.transform.position = new Vector3(Random.Range(minXPos, maxXPos), yPos, 0.0f);
            }
            //make random delay between items
            yield return new WaitForSeconds(Random.Range(0.3f, 0.5f));
        }
        StopLoopAndDestroyAllBubbles();
        StartCoroutine(MakeEndGameSequence());
    }

    private MniGameFlyingItem GetUnUsedBubble()
    {
        for (int i = 0; i < lstBubbles.Count; i++)
        {
            if (!lstBubbles[i].GetIsUsed())
            {
                return lstBubbles[i];
            }
        }
        return null;
    }

    public void EndGameByCorrectDestory()
    {
        StopLoopAndDestroyAllBubbles();
        StartCoroutine(MakeEndGameSequence());
    }

    //public void EndGameByWrongClick()
    //{
    //    StopLoopAndDestroyAllBubbles();
    //    StartCoroutine(MakeEndGameSequence());
    //}

    private void StopLoopAndDestroyAllBubbles()
    {
        StopCoroutine(corGameLoop);
        DestroyAllItemsByGameOver();
    }


    private void DestroyAllItemsByGameOver()
    {
        for (int i = 0; i < lstBubbles.Count; i++)
        {
            lstBubbles[i].DestroyItemByGameOver();
        }
    }

    private IEnumerator MakeEndGameSequence()
    {
        //stop timer 
        miniGameTimer.StopTimer();
        StopCoroutine(corTimer);

        //animate game over
        gameOverObjects.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.5f);
        VoiceOverManager.Instance.PlayMiniGameOver();
        //yield return new WaitForSeconds(3.0f);
        //gameOverObjects.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);



    }

    private void RsetGameToDefault()
    {
        lstHistory.Clear();
    }

    public void BtnMiniGameStart_OnClick()
    {
        StartGame();
        miniTimer.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
        miniCoins.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack).SetDelay(0.1f);
        miniStartLable.DOScale(Vector3.zero, 0.35f).SetEase(Ease.InBack);
        miniStartButton.DOScale(Vector3.zero, 0.35f).SetEase(Ease.InBack).SetDelay(0.1f);
    }

    public void BtnHome_OnClick()
    {
        VoiceOverManager.Instance.PlayRatingIntro();
        gameManager.ShowRating();
        RsetGameToDefault();
    }

}

[System.Serializable]
public class MiniGameItem
{
    public Sprite itemSprite;
    public bool isCorrect;
}
