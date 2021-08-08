using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceBreakfastItemPlay : MonoBehaviour
{
    public string strVoiceName;


    public void PlayVoiceOverByName()
    {
        //Debug.Log("PlayVoiceOverByName " + strVoiceName);
        if (strVoiceName != "")
        {
            //Debug.Log("Playing String Voice on " + name);
            VoiceOverManager.Instance.PlayBreakfastByName(strVoiceName);
        }
        else
        {
            //Debug.Log("Random wrong Voice on " + name);
            VoiceOverManager.Instance.PlayRandomWrongVoice();
        }
    }
}
