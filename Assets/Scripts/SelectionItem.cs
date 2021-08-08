using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class SelectionItem : MonoBehaviour
{
    [SerializeField]
    private bool isCorrectItem;
    [SerializeField]
    private Sprite ItemSprite;

    public SinjabController sinjabController;

    public ItemsSelectionController selectionController;

    private Button m_ButtonLocal;

    // Start is called before the first frame update
    void Start()
    {
        m_ButtonLocal = GetComponent<Button>();
    }

    public void SelectionItem_OnClick()
    {
        //Debug.Log("Adding correct item " + isCorrectItem + "Btn name : " + name);
        if (isCorrectItem)
        {
            //Debug.Log("Adding correct item");
            gameObject.SetActive(false);
            selectionController.AddItemToSelection(ItemSprite, transform);

        }
        else
        {
            transform.DOPunchScale(Vector3.one * 0.1f, 0.35f);
        }
        selectionController.MakeActivationLoop(VoiceOverManager.Instance.GetCurrentVOLength());
        sinjabController.SetSenjabTalking(VoiceOverManager.Instance.GetCurrentVOLength());
    }

    public void ResetItem()
    {
        gameObject.SetActive(true);
    }

    internal void InitItem(Sprite itemSprite, bool isCorrect)
    {
        isCorrectItem = isCorrect;
        ItemSprite = itemSprite;
        GetComponent<Image>().sprite = itemSprite;
    }

    internal void SetInteractable(bool isInteractable)
    {
        m_ButtonLocal.interactable = isInteractable;
    }
}
