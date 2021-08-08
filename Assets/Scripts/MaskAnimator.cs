using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaskAnimator : MonoBehaviour
{
    //public SpriteRenderer spWhite;
    public Transform transMakColorLyr;
    public Transform transMask;
    public float MaskInDuration;
    public float MaskOutDuration;

    public AudioClip clpTransitionIn;
    public AudioClip clpTransitionOut;
    private AudioSource m_audiosource;

    // Start is called before the first frame update
    void Start()
    {
        m_audiosource = gameObject.AddComponent<AudioSource>();
        m_audiosource.playOnAwake = false;
    }

    public void TweenMaskIn()
    {
        transMakColorLyr.gameObject.SetActive(true);
        transMask.gameObject.SetActive(true);
        transMask.DOScale(0.0f, MaskInDuration).SetEase(Ease.InOutQuint);
        //spWhite.DOColor(Color.white, 0.2f).SetEase(Ease.OutQuad).SetDelay(MaskInDuration * 0.8f);
        transMask.DORotate(Vector3.zero, MaskInDuration).SetEase(Ease.OutSine);//.SetDelay(0.1f);

        PlayTransitionIn();
    }

    public void TweenMaskOut()
    {
        //spWhite.DOColor(new Color(1.0f, 1.0f, 1.0f, 0.0f), 0.2f).SetEase(Ease.OutQuad);
        transMask.DOScale(10.0f, MaskOutDuration).SetEase(Ease.InOutQuint).SetDelay(0.15f);
        transMask.DORotate(new Vector3(0.0f, 0.0f, 90.0f), MaskInDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            transMakColorLyr.gameObject.SetActive(false);
            transMask.gameObject.SetActive(false);
        });

        PlayTransitionOut();
    }

    public void PlayTransitionIn()
    {
        m_audiosource.clip = clpTransitionIn;
        m_audiosource.Play();
    }

    public void PlayTransitionOut()
    {
        m_audiosource.clip = clpTransitionOut;
        m_audiosource.Play();
    }
}
