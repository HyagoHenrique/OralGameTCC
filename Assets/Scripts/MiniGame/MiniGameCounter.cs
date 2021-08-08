using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MiniGameCounter : MonoBehaviour
{

    public TextMeshProUGUI txtCounter;
    public TextMeshProUGUI txtCounterGameOver;

    private int counter = 0;


    public AudioClip clpStarEffect;
    private AudioSource m_AudioSource;


    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = gameObject.AddComponent<AudioSource>();
        m_AudioSource.playOnAwake = false;
        m_AudioSource.clip = clpStarEffect;
        m_AudioSource.volume = 0.4f;
    }

    public void AddCounter()
    {
        IncraseCounter();
        MakeTextPunch();
        SetCounter();
        PlaySound();
    }

    public void SubtractCounter()
    {
        DecraseCounter();
        MakeTextPunch();
        SetCounter();
    }

    private void IncraseCounter()
    {
        counter++;
    }

    private void DecraseCounter()
    {
        counter = Mathf.Clamp(counter - 1, 0, 1000);
    }

    private void MakeTextPunch()
    {
        transform.DOPunchScale(Vector3.one * 0.3f, 0.25f);
    }

    public void SetCounter()
    {
        txtCounter.text = counter.ToString();
        txtCounterGameOver.text = counter.ToString();
    }

    private void PlaySound()
    {
        m_AudioSource.Play();
    }


}
