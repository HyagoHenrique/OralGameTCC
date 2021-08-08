using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StaticArabicText : MonoBehaviour
{
    public string strValue;
    private TextMeshProUGUI txtlocal;
    private TextMeshPro txtlocal2;


    // Start is called before the first frame update
    void Start()
    {
        txtlocal = GetComponent<TextMeshProUGUI>();
        txtlocal2 = GetComponent<TextMeshPro>();
        if (txtlocal != null)
        {
            if (string.IsNullOrEmpty(txtlocal.text))
            {
                txtlocal.text = ArabicSupport.ArabicFixer.Fix(strValue);
            }
            else
            {
                string strValue = txtlocal.text;
                txtlocal.text = ArabicSupport.ArabicFixer.Fix(strValue);
            }
            
        }
        else
        {
            txtlocal2.text = ArabicSupport.ArabicFixer.Fix(strValue);
        }
    }


}
