using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public float brushingCameraSize;


    private float originalSize;
    private Camera m_Camera;


    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        originalSize = m_Camera.orthographicSize;
    }

    public void ResetCameraSize(float delay)
    {
        Debug.Log("Resetting camera size");
        m_Camera.DOOrthoSize(originalSize, 0.05f).SetDelay(delay);
    }

    public void SetCameraZoomToBrushing(float delay)
    {
        Debug.Log("Setting camera zoom to brushing " + delay + " , Orth size : " + brushingCameraSize);
        //m_Camera.DOOrthoSize(brushingCameraSize, 0.35f).SetEase(Ease.OutCubic).SetDelay(delay);
        Invoke("DelayCameraSize", delay);
    }

    private void DelayCameraSize()
    {
        m_Camera.orthographicSize = brushingCameraSize;
    }
}
