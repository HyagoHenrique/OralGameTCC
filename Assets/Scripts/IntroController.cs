using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{

    [SerializeField]
    private GameManager m_GameManager;

    public GameObject panelTeethBrushing;
    public GameObject panelFood;
    public GameObject panelDentistVisit;

    public Sprite spriteBtnActive;
    public Sprite spriteBtnInactive;

    public Image imgBtnTeethBrushing;
    public Image imgBtnFood;
    public Image imgBtnDentistVisit;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BtnPlay_OnClick()
    {
        m_GameManager.ShowGameSelection();
    }

    public void BtnPanelTeethBrushing_OnClick()
    {
        panelTeethBrushing.SetActive(true);
        panelFood.SetActive(false);
        panelDentistVisit.SetActive(false);

        imgBtnTeethBrushing.sprite = spriteBtnActive;
        imgBtnFood.sprite = spriteBtnInactive;
        imgBtnDentistVisit.sprite = spriteBtnInactive;
    }

    public void BtnPanelFood_OnClick()
    {
        panelTeethBrushing.SetActive(false);
        panelFood.SetActive(true);
        panelDentistVisit.SetActive(false);

        imgBtnTeethBrushing.sprite = spriteBtnInactive;
        imgBtnFood.sprite = spriteBtnActive;
        imgBtnDentistVisit.sprite = spriteBtnInactive;
    }

    public void BtnPanelDentistVisit_OnClick()
    {
        panelTeethBrushing.SetActive(false);
        panelFood.SetActive(false);
        panelDentistVisit.SetActive(true);

        imgBtnTeethBrushing.sprite = spriteBtnInactive;
        imgBtnFood.sprite = spriteBtnInactive;
        imgBtnDentistVisit.sprite = spriteBtnActive;
    }
}
