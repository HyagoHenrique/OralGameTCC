using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIScreen : ScreenBase
{
    IScreenRset iScreenRset;

    // Start is called before the first frame update
    public override void Start()
    {
        iScreenRset = GetComponent<IScreenRset>();
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
        if (_screenType == ScreenType.Breakfast || _screenType == ScreenType.Launch)
        {
            GetComponent<ItemsSelectionController>().InitItemsOnShow();
        }
    }

    public override void ResetScreen()
    {
        if (iScreenRset != null)
        {
            iScreenRset.OnScreenRset();
        }
    }
}
