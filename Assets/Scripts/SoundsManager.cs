using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{

    public List<AudioClip> lstClpButtons;
    public List<AudioClip> lstClpItemsClick;

    public AudioClip clpCorrectSFX;
    public AudioClip clpWrongSFX;
    public AudioClip clpPopSound;

    [Header("Music")]
    public float musicVolume = 0.25f;
    public float musicChangeSpeed;
    public AudioClip clpMainMusicFirst;
    public AudioClip clpMainMusicSecond;

    private bool isSoundOn = true;

    private AudioSource m_audioSource;
    private AudioSource m_SFXSource;
    private AudioSource m_MusicSource;

    private static SoundsManager _instance;
    public static SoundsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundsManager>();
            }

            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = gameObject.AddComponent<AudioSource>();
        m_audioSource.playOnAwake = false;
        m_audioSource.loop = false;

        m_MusicSource = gameObject.AddComponent<AudioSource>();
        m_MusicSource.playOnAwake = false;
        m_MusicSource.volume = musicVolume;
        m_MusicSource.loop = true;

        m_SFXSource = gameObject.AddComponent<AudioSource>();
        m_SFXSource.playOnAwake = false;
        m_SFXSource.loop = false;

    }

    public void PlayButtonSound()
    {
        if (isSoundOn)
        {
            int rndIndex = Random.Range(0, lstClpButtons.Count);
            m_audioSource.clip = lstClpButtons[rndIndex];
            m_audioSource.Play();
        }
    }

    public void PlayItemsClickSound()
    {
        if (isSoundOn)
        {
            int rndIndex = Random.Range(0, lstClpItemsClick.Count);
            m_audioSource.clip = lstClpItemsClick[rndIndex];
            m_audioSource.Play();
        }
    }

    public void PlayMainTheme()
    {
        if (m_MusicSource.clip == null)
        {
            m_MusicSource.clip = clpMainMusicFirst;
            m_MusicSource.Play();
        }
    }

    public void SwitchMusicLoops()
    {
        StartCoroutine(SwitchMusicLoop());
    }

    private IEnumerator SwitchMusicLoop()
    {
        //fade music
        while (m_MusicSource.volume > 0.05f)
        {
            m_MusicSource.volume -= Time.deltaTime * musicChangeSpeed;
            yield return null;
        }
        yield return null;

        //change sound effect
        m_MusicSource.Stop();
        if (m_MusicSource.clip == clpMainMusicFirst)
        {
            m_MusicSource.clip = clpMainMusicSecond;
        }
        else
        {
            m_MusicSource.clip = clpMainMusicFirst;
        }
        yield return null;

        m_MusicSource.volume = 0.0f;
        yield return new WaitForSeconds(2.0f);

        //make volume higher
        m_MusicSource.Play();
        while (m_MusicSource.volume < musicVolume)
        {
            m_MusicSource.volume += Time.deltaTime * musicChangeSpeed;

            if (m_MusicSource.volume >= musicVolume * 0.95f)
            {
                m_MusicSource.volume = musicVolume;
            }
            yield return null;
        }
    }

    public void PlaySFXCorrect()
    {
        m_SFXSource.clip = clpCorrectSFX;
        m_SFXSource.Play();
    }

    public void PlaySFXWrong()
    {
        m_SFXSource.clip = clpWrongSFX;
        m_SFXSource.Play();
    }

    public void PlayPopSound()
    {
        m_SFXSource.clip = clpPopSound;
        m_SFXSource.Play();
    }

}
