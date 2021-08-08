using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniGameTimer : MonoBehaviour
{
    private bool canCount = false;
    private float timer = 60.0f;
    private TextMeshProUGUI txtlocal;

    // Start is called before the first frame update
    void Start()
    {
        txtlocal = GetComponent<TextMeshProUGUI>();
        SetString(timer);
    }

    // Update is called once per frame
    void Update()
    {
        if (canCount)
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0.0f, 200.0f);
            SetString(timer);
        }
    }

    public void SetGameTime(float totalTime)
    {
        timer = totalTime;
    }

    public void StartTimer()
    {
        canCount = true;
    }

    public void StopTimer()
    {
        canCount = false;
        SetString(0.0f);
    }

    private void SetString(float time)
    {
        string seconds = (time % 60).ToString("00");
        txtlocal.text = string.Format("{0}", seconds);
    }
}
