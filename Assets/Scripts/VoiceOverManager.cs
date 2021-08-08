using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverManager : MonoBehaviour
{
    public AudioClip clpIntro;
    public AudioClip clpIntrofemale;
    public AudioClip clpCharacterSelection;
    public float introDelay;
    public float characterSelectionDelay;

    [Header("Breakfast")]
    public AudioClip clpBreakfastIntro;
    public AudioClip clpBreakfastIntroFemale;

    public float breakfastIntroDelay;
    public AudioClip clpBreakfastFruit;
    public AudioClip clpBreakfastFruitFemale;
    public AudioClip clpBreakfastNutella;
    public AudioClip clpBreakfastNutellaFemale;
    public AudioClip clpBreakfastCerial;
    public AudioClip clpBreakfastCerialFemale;
    public AudioClip clpBreakfastDonuts;
    public AudioClip clpBreakfastDonutsFemale;
    public AudioClip clpbreakfastLabaneh;
    public AudioClip clpBreakfastChocolate;
    public AudioClip clpBreakfastChocolateFemale;
    public AudioClip clpBreakfastCheese;
    public AudioClip clpBreakfastEggs;
    public AudioClip clpBreakfastCerialSugar;
    public AudioClip clpBreakfastCerialSugarFemale;
    public AudioClip clpBreakfastBrushingTransition;
    public AudioClip clpBreakfastBrushingTransitionFemale;

    [Header("Teethbrushing")]
    public AudioClip clpTeethBrushingIntro;
    public AudioClip clpTeethBrushingIntroFemale;
    public float teethBrushingIntroDelay;
    public AudioClip clpTeethPast;
    public AudioClip clpTeethPastFemale;
    public AudioClip clpTeethBrushingUp;
    public AudioClip clpTeethBrushingUpFemale;
    public AudioClip clpTeethBrushingSide;
    public AudioClip clpTeethBrushingSideFemale;
    public AudioClip clpTeethBrushingSpit;
    public AudioClip clpTeethBrushingSpitFemale;

    [Header("lunch")]
    public AudioClip clplunchIntro;
    public AudioClip clplunchIntroFemale;
    public float lunchDelay;
    public AudioClip clplunchSanswitch;
    public AudioClip clplunchSanswitchFemale;
    public AudioClip clplunchMilk;
    public AudioClip clplunchMilkFemale;
    public AudioClip clplunchSweets;
    public AudioClip clplunchSweetsFemale;
    public AudioClip clplunchOrange;
    public AudioClip clplunchWater;
    public AudioClip clplunchWaterFemale;
    public AudioClip clplunchIceCream;
    public AudioClip clplunchIceCreamFemale;
    public AudioClip clpLunchJuice;
    public AudioClip clpLunchJuiceFemale;
    public AudioClip clpLunchCucumber;
    public AudioClip clpLunchArtificialJuice;
    public AudioClip clpLunchArtificialJuiceFemale;
    public AudioClip clpLunchApple;
    public AudioClip clpLunchBiscutsAndChips;
    public AudioClip clpLunchBiscutsAndChipsFemale;

    [Header("Dinner")]
    public AudioClip clpDinnerIntro;
    public AudioClip clpDinnerIntroFemale;
    public float dinnerDelay;

    public AudioClip clpInstructionCanEat;
    public AudioClip clpInstructionCantEat;
    public AudioClip clpInstructionSometimeEat;

    [Header("Dintest")]
    public AudioClip clpDentistIntro;
    public AudioClip clpDentistIntroFemale;
    public float dentistDelay;


    public AudioClip clpDetistMirrorEnd;
    public AudioClip clpDetistMirrorEndFemale;
    public AudioClip clpDetistWaterEnd;
    public AudioClip clpDetistWaterEndFemale;
    public AudioClip clpDetistToothBrushEnd;
    public AudioClip clpDetistToothBrushEndFemale;
    public AudioClip clpDetistMiniBrushEnd;
    public AudioClip clpDetistMiniBrushEndFemale;
    public AudioClip clpDentistNextTool;
    public AudioClip clpDentistNextToolFemale;
    public AudioClip clpDentistMiniBrushTransition;
    public AudioClip clpDentistMiniBrushTransitionFemale;

    public AudioClip[] arrClpDentistRandomWrong;
    public AudioClip[] arrClpDentistRandomWrongFemale;

    [Header("rating")]
    public AudioClip clpRatingIntro;
    public AudioClip clpRatingIntroFemale;
    public float ratingDelay;

    [Header("Random items ")]
    public AudioClip[] arrClpRandomCorrect;
    public AudioClip[] arrClpRandomWrong;
    public AudioClip[] arrClpRandomWrongFemale;

    [Header("Random Dinner")]
    public AudioClip[] arrClpDinnerRandomCorrect;
    public AudioClip[] arrClpDinnerRandomCorrectSomeTimeEat;
    public AudioClip[] arrClpDinnerRandomCorrectCantEat;
    public AudioClip[] arrClpDinnerRandomWrong;
    public AudioClip[] arrClpDinnerRandomCorrectFemale;
    public AudioClip[] arrClpDinnerRandomCorrectSomeTimeEatFemale;
    public AudioClip[] arrClpDinnerRandomCorrectCantEatFemale;
    public AudioClip[] arrClpDinnerRandomWrongFemale;

    [Header("Mini game")]
    public AudioClip clpMiniGameIntro;
    public AudioClip clpGameOver;
    public float miniGameDelay;
    public AudioClip[] arrClpRandomMiniCorrectMale;
    public AudioClip[] arrClpRandomMiniCorrectFemale;
    public AudioClip[] arrClpRandomMiniWrongMale;



    private AudioSource m_srcLocal;


    private Coroutine corDelayPlay = null;

    private string strGender;

    private static VoiceOverManager instance;
    public static VoiceOverManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<VoiceOverManager>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_srcLocal = gameObject.AddComponent<AudioSource>();
        m_srcLocal.playOnAwake = false;
    }

    public void SetCharacterGender(string gender)
    {
        strGender = gender;
    }

    public void StopVoiceOver()
    {
        if (corDelayPlay != null)
        {
            StopCoroutine(corDelayPlay);
            corDelayPlay = null;
        }
        m_srcLocal.Stop();
    }

    public void PlayIntro()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpIntro;
        }
        else
        {
            m_srcLocal.clip = clpIntrofemale;
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(introDelay));
        }
    }

    public void PlayIntro(float delay)
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpIntro;
        }
        else
        {
            m_srcLocal.clip = clpIntrofemale;
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(delay));
        }
    }

    public void PlayBreakfastIntro()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpBreakfastIntro;
        }
        else
        {
            m_srcLocal.clip = clpBreakfastIntroFemale;
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(breakfastIntroDelay));
        }
    }

    public void PlayCharacterSelection()
    {
        m_srcLocal.clip = clpCharacterSelection;
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(breakfastIntroDelay));
        }
    }

    public void PlayBreakfastByName(string name)
    {

        Debug.Log("Playing clip for name ==> " + name);

        switch (name)
        {
            case "fruit":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clpBreakfastFruit;
                }
                else
                {
                    m_srcLocal.clip = clpBreakfastFruitFemale;
                }
                break;
            case "nutella":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clpBreakfastNutella;
                }
                else
                {
                    m_srcLocal.clip = clpBreakfastNutellaFemale;
                }
                break;
            case "cerial":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clpBreakfastCerial;
                }
                else
                {
                    m_srcLocal.clip = clpBreakfastCerialFemale;
                }
                break;
            case "donut":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clpBreakfastDonuts;

                }
                else
                {
                    m_srcLocal.clip = clpBreakfastDonutsFemale;
                }
                break;
            case "labaneh":
                m_srcLocal.clip = clpbreakfastLabaneh;
                break;
            case "Chocolate":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clpBreakfastChocolate;
                }
                else
                {
                    m_srcLocal.clip = clpBreakfastChocolateFemale;
                }
                break;
            case "Cheese":
                m_srcLocal.clip = clpBreakfastCheese;
                break;
            case "BoiledEggs":
                m_srcLocal.clip = clpBreakfastEggs;
                break;
            case "cerialSugar":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clpBreakfastCerialSugar;
                }
                else
                {
                    m_srcLocal.clip = clpBreakfastCerialSugarFemale;
                }
                break;
            case "sandwitch":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clplunchSanswitch;
                }
                else
                {
                    m_srcLocal.clip = clplunchSanswitchFemale;
                }
                break;
            case "milk":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clplunchMilk;
                }
                else
                {
                    m_srcLocal.clip = clplunchMilkFemale;
                }
                break;
            case "sweets":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clplunchSweets;
                }
                else
                {
                    m_srcLocal.clip = clplunchSweetsFemale;
                }
                break;
            case "orange":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clplunchOrange;
                }
                else
                {
                    m_srcLocal.clip = clpLunchJuiceFemale;
                }
                break;
            case "water":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clplunchWater;
                }
                else
                {
                    m_srcLocal.clip = clplunchWaterFemale;
                }
                break;
            case "iceCream":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clplunchSanswitch;
                }
                else
                {
                    m_srcLocal.clip = clplunchSanswitchFemale;
                }
                break;
            // if (strGender == "M")
            // {
            //     m_srcLocal.clip = clplunchIceCream;
            // }
            // else
            // {
            //     m_srcLocal.clip = clplunchIceCreamFemale;
            // }
            // break;
            case "fruitJuice":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clpLunchJuice;
                }
                else
                {
                    m_srcLocal.clip = clpLunchJuiceFemale;
                }
                break;
            case "cucumber":
                m_srcLocal.clip = clpLunchCucumber;
                break;
            case "artificialJuice":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clpLunchArtificialJuice;
                }
                else
                {
                    m_srcLocal.clip = clpLunchArtificialJuiceFemale;
                }
                break;
            case "apple":
                m_srcLocal.clip = clpLunchApple;
                break;
            case "biscuitAndChips":
                if (strGender == "M")
                {
                    m_srcLocal.clip = clplunchSanswitch;
                }
                else
                {
                    m_srcLocal.clip = clplunchSanswitchFemale;
                }
                break;
                // if (strGender == "M")
                // {
                //     m_srcLocal.clip = clpLunchBiscutsAndChips;
                // }
                // else
                // {
                //     m_srcLocal.clip = clpLunchBiscutsAndChipsFemale;
                // }

                // break;

        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayBrushingTransition()
    {
        Debug.Log("<b>PlayBrushingTransition </b>");
        if (strGender == "M")
        {
            m_srcLocal.clip = clpBreakfastBrushingTransition;
        }
        else
        {
            m_srcLocal.clip = clpBreakfastBrushingTransitionFemale;
        }

        corDelayPlay = StartCoroutine(DelayPlaying(0.1f));
    }

    public float GetCurrentVOLength()
    {
        if (m_srcLocal.clip != null)
        {
            //Debug.Log("Current clip name : " + m_srcLocal.clip.name);
            return m_srcLocal.clip.length;
        }

        return 0.0f;
    }

    //------------------------------------- Brushing VO

    public void PlayTeethBrushingIntro()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpTeethBrushingIntro;
        }
        else
        {
            m_srcLocal.clip = clpTeethBrushingIntroFemale;
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(teethBrushingIntroDelay));
        }
    }

    public void PlayTeethPast()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpTeethPast;
        }
        else
        {
            m_srcLocal.clip = clpTeethPastFemale;
        }

        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayTeethBrushingUp()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpTeethBrushingUp;
        }
        else
        {
            m_srcLocal.clip = clpTeethBrushingUpFemale;
        }

        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayTeethBrushingSide()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpTeethBrushingSide;
        }
        else
        {
            m_srcLocal.clip = clpTeethBrushingSideFemale;
        }

        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayTeethSpit()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpTeethBrushingSpit;
        }
        else
        {
            m_srcLocal.clip = clpTeethBrushingSpitFemale;
        }

        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    //------------------------------------- Lunch VO

    public void PlayLunchIntro()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clplunchIntro;
        }
        else
        {
            m_srcLocal.clip = clplunchIntroFemale;
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(lunchDelay));
        }
    }

    //------------------------------------- Dinner VO

    public void PlayDinnerIntro()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpDinnerIntro;
        }
        else
        {
            m_srcLocal.clip = clpDinnerIntroFemale;
        }

        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(dinnerDelay));
        }
    }

    public void PlayDinnerRandomCorrect(EatStatus eatStatus)
    {
        AudioClip clpCorrect = null;
        if (strGender == "M")
        {
            switch (eatStatus)
            {
                case EatStatus.CanEat:
                    clpCorrect = arrClpDinnerRandomCorrect[Random.Range(0, arrClpDinnerRandomCorrect.Length)];
                    break;
                case EatStatus.CanntEat:
                    clpCorrect = arrClpDinnerRandomCorrectCantEat[Random.Range(0, arrClpDinnerRandomCorrectCantEat.Length)];
                    break;
                case EatStatus.SometimeEat:
                    clpCorrect = arrClpDinnerRandomCorrectSomeTimeEat[Random.Range(0, arrClpDinnerRandomCorrectSomeTimeEat.Length)];
                    break;
            }
            m_srcLocal.clip = clpCorrect;
        }
        else
        {
            switch (eatStatus)
            {
                case EatStatus.CanEat:
                    clpCorrect = arrClpDinnerRandomCorrectFemale[Random.Range(0, arrClpDinnerRandomCorrectFemale.Length)];
                    break;
                case EatStatus.CanntEat:
                    clpCorrect = arrClpDinnerRandomCorrectCantEatFemale[Random.Range(0, arrClpDinnerRandomCorrectCantEatFemale.Length)];
                    break;
                case EatStatus.SometimeEat:
                    clpCorrect = arrClpDinnerRandomCorrectSomeTimeEatFemale[Random.Range(0, arrClpDinnerRandomCorrectSomeTimeEatFemale.Length)];
                    break;
            }
            m_srcLocal.clip = clpCorrect;
        }

        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayDinnerRandomWrong()
    {
        if (strGender == "M")
        {
            int index = Random.Range(0, arrClpDinnerRandomWrong.Length);
            m_srcLocal.clip = arrClpDinnerRandomWrong[index];
        }
        else
        {
            int index = Random.Range(0, arrClpDinnerRandomWrongFemale.Length);
            m_srcLocal.clip = arrClpDinnerRandomWrongFemale[index];
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayInstructionCanEat()
    {
        m_srcLocal.clip = clpInstructionCanEat;
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayInstructionCantEat()
    {
        m_srcLocal.clip = clpInstructionCantEat;
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayInstructionSometimeEat()
    {
        m_srcLocal.clip = clpInstructionSometimeEat;
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    //------------------------------------- Dentist VO

    public void PlayDentistIntro()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpDentistIntro;
        }
        else
        {
            m_srcLocal.clip = clpDentistIntroFemale;
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(dentistDelay));
        }
    }

    public void PlayDentistMirrorEnd()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpDetistMirrorEnd;
        }
        else
        {
            m_srcLocal.clip = clpDetistMirrorEndFemale;
        }

        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.2f));
        }
    }

    public void PlayDentistWaterEnd()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpDetistWaterEnd;
        }
        else
        {
            m_srcLocal.clip = clpDetistWaterEndFemale;
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.2f));
        }
    }

    public void PlayDentistToothBrushEnd()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpDetistToothBrushEnd;
        }
        else
        {
            m_srcLocal.clip = clpDetistToothBrushEndFemale;
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.2f));
        }
    }

    public void PlayDentistMiniBrushEnd()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpDetistMiniBrushEnd;
        }
        else
        {
            m_srcLocal.clip = clpDetistMiniBrushEndFemale;
        }

        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.2f));
        }
    }

    public IEnumerator PlayDentistNextTool(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (strGender == "M")
        {
            m_srcLocal.clip = clpDentistNextTool;
        }
        else
        {
            m_srcLocal.clip = clpDentistNextToolFemale;
        }
        yield return null;
        if (!m_srcLocal.isPlaying)
            m_srcLocal.Play();
    }

    public IEnumerator PlayDentistMiniBrushTransition(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (strGender == "M")
        {
            m_srcLocal.clip = clpDentistMiniBrushTransition;
        }
        else
        {
            m_srcLocal.clip = clpDentistMiniBrushTransitionFemale;
        }
        yield return null;
        if (!m_srcLocal.isPlaying)
            m_srcLocal.Play();
    }

    public void PlayDentistRandomWrongVoice(out float length)
    {
        if (strGender == "M")
        {
            int index = Random.Range(0, arrClpDentistRandomWrong.Length);
            length = arrClpDentistRandomWrong[index].length;
            m_srcLocal.clip = arrClpDentistRandomWrong[index];
        }
        else
        {
            int index = Random.Range(0, arrClpDentistRandomWrongFemale.Length);
            length = arrClpDentistRandomWrongFemale[index].length;
            m_srcLocal.clip = arrClpDentistRandomWrongFemale[index];
        }

        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    //------------------------------------- Rating VO

    public void PlayRatingIntro()
    {
        if (strGender == "M")
        {
            m_srcLocal.clip = clpRatingIntro;
        }
        else
        {
            m_srcLocal.clip = clpRatingIntroFemale;
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(ratingDelay));
        }
    }

    //------------------------------------- Random VO

    public void PlayRandomWrongVoice()
    {
        if (strGender == "M")
        {
            int index = Random.Range(0, arrClpRandomWrong.Length);
            m_srcLocal.clip = arrClpRandomWrong[index];
        }
        else
        {
            int index = Random.Range(0, arrClpRandomWrongFemale.Length);
            m_srcLocal.clip = arrClpRandomWrongFemale[index];
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayRandomCorrectVoice()
    {
        int index = Random.Range(0, arrClpRandomCorrect.Length);
        m_srcLocal.clip = arrClpRandomCorrect[index];
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    //------------------------------------- minigame VO

    public void PlayMiniGameIntro()
    {
        m_srcLocal.clip = clpMiniGameIntro;
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(miniGameDelay));
        }
    }

    public void PlayMiniGameOver()
    {
        m_srcLocal.clip = clpGameOver;
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayMiniGameRandomCorrect()
    {
        if (strGender == "M")
        {
            int index = Random.Range(0, arrClpRandomMiniCorrectMale.Length);
            m_srcLocal.clip = arrClpRandomMiniCorrectMale[index];
        }
        else
        {
            int index = Random.Range(0, arrClpRandomMiniCorrectFemale.Length);
            m_srcLocal.clip = arrClpRandomMiniCorrectFemale[index];
        }
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }

    public void PlayMiniGameRandomWrong()
    {
        int index = Random.Range(0, arrClpRandomMiniWrongMale.Length);
        m_srcLocal.clip = arrClpRandomMiniWrongMale[index];
        if (corDelayPlay == null)
        {
            corDelayPlay = StartCoroutine(DelayPlaying(0.0f));
        }
    }


    private IEnumerator DelayPlaying(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_srcLocal.Play();
        corDelayPlay = null;
    }
}
