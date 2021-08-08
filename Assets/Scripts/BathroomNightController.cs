using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomNightController : MonoBehaviour
{

    public SpriteRenderer spRndbackground;
    public SpriteRenderer spRndMirror;
    public SpriteRenderer spRndLight;

    public Sprite spDayBackground;
    public Sprite spDayMirror;

    public Sprite spNightBackground;
    public Sprite spNightMirror;
    public Sprite spNightLight;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetDayTime(bool isDay)
    {
        if (isDay)
        {
            spRndbackground.sprite = spDayBackground;
            spRndMirror.sprite = spDayMirror;
            spRndLight.sprite = null;
        }
        else
        {
            spRndbackground.sprite = spNightBackground;
            spRndMirror.sprite = spNightMirror;
            spRndLight.sprite = spNightLight;
        }
    }
}
