using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisable : MonoBehaviour
{
    public float disableDuration = 1;
    private Button btnLocal;

    // Start is called before the first frame update
    void Start()
    {
        btnLocal = GetComponent<Button>();
        if (btnLocal == null)
        {
            enabled = false;
            return;
        }

        btnLocal.onClick.AddListener(BtnLocal_OnClick);
    }

    private void BtnLocal_OnClick()
    {
        StartCoroutine(DisableButtonForDuration());
    }

    private IEnumerator DisableButtonForDuration()
    {
        btnLocal.interactable = false;
        yield return new WaitForSeconds(disableDuration);
        btnLocal.interactable = true;
    }
}
