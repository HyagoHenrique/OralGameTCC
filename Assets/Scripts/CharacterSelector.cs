using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TeethPastPosition teethPastPosition;


    public List<CharacterScreenProperties> lstCharacterProp;

    [Header("Characters")]
    [SerializeField]
    private Transform maleCharacter;
    [SerializeField]
    private Transform femaleCharacter;

    [SerializeField]
    private Transform dentistMaleCharacter;
    [SerializeField]
    private Transform dentistFemaleCharacter;

    [Header("Animator")]
    [SerializeField]
    private GafCharacterAnimator maleAnimator;
    [SerializeField]
    private GafCharacterAnimator femaleAnimator;

    [Header("character parent")]
    [SerializeField]
    private Transform transBreakfastCharParent;
    [SerializeField]
    private Transform translaunchCharParent;
    [SerializeField]
    private Transform transBrushingCharParent;
    [SerializeField]
    private Transform transDentistCharParent;

    private bool isDentist = false;

    private CharacterType m_selectedCharacter;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SelecteMaleCharacter()
    {
        m_selectedCharacter = CharacterType.Male;
        VoiceOverManager.Instance.SetCharacterGender("M");
        teethPastPosition.SetGender("M");
        gameManager.ShowIntro();
    }

    public void SelecteFemaleCharacter()
    {
        m_selectedCharacter = CharacterType.Female;
        VoiceOverManager.Instance.SetCharacterGender("F");
        teethPastPosition.SetGender("F");
        gameManager.ShowIntro();
    }

    public void ShowCharacterInBreakfast()
    {
        SetDentist(false);
        SetCharacterActive(true);
        SetCharacterParent(transBreakfastCharParent);
        SetCharacterAnimation(AnimationType.Idle);
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                SetCharacterLocalPosition(lstCharacterProp[0].maleOffset);
                SetCharacterScale(lstCharacterProp[0].maleScale);
                //SetCharacterSize(lstCharacterProp[0].maleSize);
                break;
            case CharacterType.Female:
                SetCharacterLocalPosition(lstCharacterProp[0].femaleOffset);
                SetCharacterScale(lstCharacterProp[0].femaleScale);
                //SetCharacterSize(lstCharacterProp[0].femaleSize);
                break;
        }
    }

    public void ShowCharacterInLaunch()
    {
        SetDentist(false);
        SetCharacterActive(true);
        SetCharacterParent(translaunchCharParent);
        SetCharacterAnimation(AnimationType.Idle);
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                SetCharacterLocalPosition(lstCharacterProp[1].maleOffset);
                SetCharacterScale(lstCharacterProp[1].maleScale);
                //SetCharacterSize(lstCharacterProp[1].maleSize);
                break;
            case CharacterType.Female:
                SetCharacterLocalPosition(lstCharacterProp[1].femaleOffset);
                SetCharacterScale(lstCharacterProp[1].femaleScale);
                //SetCharacterSize(lstCharacterProp[1].femaleSize);
                break;
        }
    }

    public void ShowCharacterInBrushing()
    {
        SetDentist(false);
        SetCharacterActive(true);
        SetCharacterParent(transBrushingCharParent);
        SetCharacterAnimation(AnimationType.BrushIdle);
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                SetCharacterLocalPosition(lstCharacterProp[2].maleOffset);
                SetCharacterScale(lstCharacterProp[2].maleScale);
                //SetCharacterSize(lstCharacterProp[2].maleSize);
                break;
            case CharacterType.Female:
                SetCharacterLocalPosition(lstCharacterProp[2].femaleOffset);
                SetCharacterScale(lstCharacterProp[2].femaleScale);
                //SetCharacterSize(lstCharacterProp[2].femaleSize);
                break;
        }
    }

    public void ShowCharacterInDentist()
    {
        SetDentist(true);
        SetCharacterActive(true);
        SetCharacterParent(transDentistCharParent);
        SetCharacterAnimation(AnimationType.DentistIdle);
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                SetCharacterLocalPosition(lstCharacterProp[3].maleOffset);
                SetCharacterScale(lstCharacterProp[3].maleScale);
                //SetCharacterSize(lstCharacterProp[3].maleSize);
                break;
            case CharacterType.Female:
                SetCharacterLocalPosition(lstCharacterProp[3].femaleOffset);
                SetCharacterScale(lstCharacterProp[3].femaleScale);
                //SetCharacterSize(lstCharacterProp[3].femaleSize);
                break;
        }
    }

    public void HideCharacter()
    {
        SetCharacterParent(null);
        SetCharacterActive(false);
    }

    public void UnparentCharacter()
    {
        SetCharacterParent(null);
    }

    public void SetDentist(bool isDent)
    {
        isDentist = isDent;
    }

    public void SetCharacterAnimation(AnimationType animType)
    {
        Debug.Log("Setting character animation state : " + animType + " , Selected Character : " + m_selectedCharacter);
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                switch (animType)
                {
                    case AnimationType.Idle:
                        maleAnimator.PlayAnimation("Boy_Idle", 0.0f);
                        break;
                    case AnimationType.BrushVertical:
                        StartCoroutine(LoopVerticalAnimation());
                        break;
                    case AnimationType.BrushHorizontal:
                        maleAnimator.PlayAnimation("Bruch_Horizontal", 0.0f);
                        break;
                    case AnimationType.BrushIdle:
                        maleAnimator.PlayAnimation("Boy_dirty_Idle", 0.0f);
                        break;
                    case AnimationType.Spit:
                        maleAnimator.PlayAnimation("Boy_Spit", 0.0f);
                        break;
                    case AnimationType.DentistIdle:
                        break;
                }
                //SetTrigger(animType.ToString());
                break;
            case CharacterType.Female:
                switch (animType)
                {
                    case AnimationType.Idle:
                        femaleAnimator.PlayAnimation("Girl_idle", 0.0f);
                        break;
                    case AnimationType.BrushVertical:
                        StartCoroutine(LoopVerticalAnimation());
                        break;
                    case AnimationType.BrushHorizontal:
                        femaleAnimator.PlayAnimation("Girl_brush_Horizontal", 0.0f);
                        break;
                    case AnimationType.BrushIdle:
                        femaleAnimator.PlayAnimation("Girl_Idle_Dirty ", 0.0f);
                        break;
                    case AnimationType.Spit:
                        femaleAnimator.PlayAnimation("Girl_Spit", 0.0f);
                        break;
                    case AnimationType.DentistIdle:
                        break;
                }
                break;
        }
    }

    private IEnumerator LoopVerticalAnimation()
    {
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                maleAnimator.PlayAnimation("Boy_Brush teeth_Vertical", 0.0f);
                yield return new WaitForSeconds(1.5f);
                maleAnimator.PlayAnimation("Vertical_Clean", 0.0f);
                break;
            case CharacterType.Female:
                femaleAnimator.PlayAnimation("Girl_brush_vertical", 0.0f);
                yield return new WaitForSeconds(1.5f);
                femaleAnimator.PlayAnimation("Girl_brush_Clean", 0.0f);
                break;
        }
    }

    private IEnumerator ShowSpitAnimation()
    {
        SetCharacterAnimation(AnimationType.Spit);
        yield return new WaitForSeconds(1.0f);
        SetCharacterAnimation(AnimationType.Idle);
    }

    //----------------------------------- brushing animation

    public void BrushTeethVertical()
    {
        SetCharacterAnimation(AnimationType.BrushVertical);
        //switch (m_selectedCharacter)
        //{
        //    case CharacterType.Male:
        //        maleAnimator.SetTrigger(AnimationType.BrushVertical.ToString());
        //        break;
        //    case CharacterType.Female:
        //        femaleAnimator.SetTrigger(AnimationType.BrushVertical.ToString());
        //        break;            
        //}
    }

    public void BrushTeethHorizontal()
    {
        SetCharacterAnimation(AnimationType.BrushHorizontal);
        //switch (m_selectedCharacter)
        //{
        //    case CharacterType.Male:
        //        maleAnimator.SetTrigger(AnimationType.BrushHorizontal.ToString());
        //        break;
        //    case CharacterType.Female:
        //        femaleAnimator.SetTrigger(AnimationType.BrushHorizontal.ToString());
        //        break;
        //}
    }

    public void MakeCharacterSpit()
    {
        StartCoroutine(ShowSpitAnimation());
    }

    //----------------------------------- setting character properties ----------------------------------- 

    private void SetCharacterActive(bool isActive)
    {
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                if (isDentist)
                {
                    dentistMaleCharacter.gameObject.SetActive(isActive);
                }
                else
                {
                    maleCharacter.gameObject.SetActive(isActive);
                }
                break;
            case CharacterType.Female:
                if (isDentist)
                {
                    dentistFemaleCharacter.gameObject.SetActive(isActive);
                }
                else
                {
                    femaleCharacter.gameObject.SetActive(isActive);
                }
                break;
        }
    }

    private void SetCharacterParent(Transform parent)
    {
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                if (isDentist)
                {
                    dentistMaleCharacter.SetParent(parent);
                }
                else
                {
                    maleCharacter.SetParent(parent);
                }
                break;
            case CharacterType.Female:
                if (isDentist)
                {
                    dentistFemaleCharacter.SetParent(parent);
                }
                else
                {

                    femaleCharacter.SetParent(parent);
                }
                break;
        }
    }

    private void SetCharacterLocalPosition(Vector2 pos)
    {
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                if (isDentist)
                {
                    dentistMaleCharacter.localPosition = pos;
                }
                else
                {
                    maleCharacter.localPosition = pos;
                }
                break;
            case CharacterType.Female:
                if (isDentist)
                {
                    dentistFemaleCharacter.localPosition = pos;
                }
                else
                {
                    femaleCharacter.localPosition = pos;
                }
                break;
        }

    }

    private void SetCharacterScale(Vector3 scale)
    {
        switch (m_selectedCharacter)
        {
            case CharacterType.Male:
                if (isDentist)
                {
                    dentistMaleCharacter.localScale = scale;
                }
                else
                {
                    maleCharacter.localScale = scale;
                }
                break;
            case CharacterType.Female:
                if (isDentist)
                {
                    dentistFemaleCharacter.localScale = scale;
                }
                else
                {
                    femaleCharacter.localScale = scale;
                }
                break;
        }
    }

    //private void SetCharacterSize(Vector2 size)
    //{
    //    switch (m_selectedCharacter)
    //    {
    //        case CharacterType.Male:
    //            maleCharacter.GetComponent<RectTransform>().sizeDelta = size;
    //            break;
    //        case CharacterType.Female:
    //            femaleCharacter.GetComponent<RectTransform>().sizeDelta = size;
    //            break;
    //    }
    //}

}

[System.Serializable]
public class CharacterScreenProperties
{
    public string propertyName;
    [Header("offset")]
    public Vector2 maleOffset;
    public Vector2 femaleOffset;

    [Header("Scale")]
    public Vector3 maleScale;
    public Vector3 femaleScale;

    [Header("Size")]
    public Vector2 maleSize;
    public Vector2 femaleSize;
}

public enum AnimationType
{
    Idle,
    BrushVertical,
    BrushHorizontal,
    BrushIdle,
    DentistIdle,
    Spit,
}
