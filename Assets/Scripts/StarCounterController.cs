using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StarCounterController : MonoBehaviour
{
    public float starRotation;
    public float starAnimDuration;
    public float starYOffset;
    public float middleOffset;
    public Ease starRotationEase;
    public AnimationCurve starCurve;
    public Transform transStar;
    public ParticleSystem correctAnswer;
    public TextMeshProUGUI txtCounter;
    public AudioClip clpStarEffect;

    private Vector2 _originalPos;
    private int starCounter = 0;
    private AudioSource m_AudioSource;
    private RectTransform m_RectTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = gameObject.AddComponent<AudioSource>();
        m_AudioSource.playOnAwake = false;
        m_AudioSource.clip = clpStarEffect;
        m_AudioSource.volume = 0.4f;
        m_RectTransform = GetComponent<RectTransform>();
        _originalPos = m_RectTransform.anchoredPosition;
        SetSavedStars();
    }

    private void SetSavedStars()
    {
        if (PlayerPrefs.HasKey("starCounter"))
        {
            starCounter = PlayerPrefs.GetInt("starCounter");
            SetStarCounter();
        }
    }


    public void AddStar()
    {
        IncraseStarCounter();
        MakeStarRotation();
        SetStarCounter();
        PlayStarSound();
    }

    public void RemoveStar()
    {
        DecreaseStarCounter();
        SetStarCounter();
    }

    public void SetStarCounterToMiddle(float delay)
    {
        Invoke("SetAchorPositionToMiddleWithDelay", delay);
    }

    public void SetStarCounterToOriginal(float delay)
    {
        Invoke("SetAchorPositionToOriginalWithDelay", delay);
    }

    private void SetAchorPositionToMiddleWithDelay()
    {
        SetAnchorPosition(new Vector2(middleOffset, _originalPos.y));
    }

    private void SetAchorPositionToOriginalWithDelay()
    {
        SetAnchorPosition(_originalPos);
    }

    private void SetAnchorPosition(Vector2 anchorPos)
    {
        m_RectTransform.anchoredPosition = anchorPos;
    }

    private void IncraseStarCounter()
    {
        starCounter++;
        PlayerPrefs.SetInt("starCounter", starCounter);
    }

    private void DecreaseStarCounter()
    {
        starCounter = Mathf.Clamp(starCounter - 1, 0, 10000);
    }

    private void SetStarCounter()
    {
        txtCounter.text = starCounter.ToString();
    }

    //make star rotation as indicator that player win 
    private void MakeStarRotation()
    {
        transStar.DORotate(new Vector3(0.0f, starRotation, 0.0f), starAnimDuration, RotateMode.LocalAxisAdd).SetEase(starRotationEase).OnComplete(() =>
        {
            transStar.rotation = Quaternion.identity;
        });
        transStar.DOLocalMoveY(transStar.localPosition.y + starYOffset, starAnimDuration).SetLoops(1, LoopType.Yoyo).SetEase(starCurve);
        correctAnswer.Play();
    }

    private void PlayStarSound()
    {
        m_AudioSource.Play();
    }

    public void ResetStarToOriginal()
    {

    }
}
