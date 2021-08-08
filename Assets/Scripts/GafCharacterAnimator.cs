using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAF.Core;

public class GafCharacterAnimator : MonoBehaviour
{
    private GAFMovieClip m_MovieClip;
    private string animationName;

    // Start is called before the first frame update
    void Start()
    {
        m_MovieClip = GetComponent<GAFMovieClip>();
    }

    public void PlayAnimation(string animName, float delay = 0.0f)
    {
        animationName = animName;
        if (delay == 0.0f)
        {
            m_MovieClip.setSequence(animationName, true);
        }
        else
        {
            CancelInvoke();
            Invoke("DealyPlayingAnimation", delay);
        }
    }

    private void DealyPlayingAnimation()
    {
        m_MovieClip.setSequence(animationName, true);
    }


}
