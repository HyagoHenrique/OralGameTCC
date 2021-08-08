using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsTracker : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    public void SubmitAllGamesAreFinished()
    {
        Analytics.CustomEvent("AllSectionsFinished", new Dictionary<string, object>() { { "finished", 1 } });
    }
}
