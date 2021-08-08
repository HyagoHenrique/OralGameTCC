using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MniGameFlyingItem : MonoBehaviour
{
    public MiniGameController miniGameController;
    public ParticleSystem correctEffect;
    public MiniGameCounter miniGameCounter;

    [Header("Y axis")]
    public float ySpeed;

    [Header("X axis")]
    public float xOffset;
    public float xSpeed;

    [SerializeField]
    private bool _canfly;

    private float xTime = 0.0f;

    private SpriteRenderer spriteRenderer;

    private bool _isCorrect;
    private bool _isUsed = false;
    private Vector3 _originalPos;



    // Start is called before the first frame update
    void Start()
    {

    }

    private void SetRef()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _originalPos = transform.localPosition;
    }

    public void InitItem(Sprite spItem, bool isCorrectItem)
    {
        SetRef();
        ySpeed = Random.Range(2.5f, 6.0f);
        spriteRenderer.sprite = spItem;
        _isCorrect = isCorrectItem;
        _isUsed = true;
        _canfly = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyCollider"))
        {
            //CheckGameOverByCollider();
            ResetObjectToDefult();
        }
    }

    public void OnMouseDown()
    {
        if (_isCorrect)
        {
            SoundsManager.Instance.PlayPopSound();
            correctEffect.Play();
            correctEffect.transform.position = transform.position;
            ResetObjectToDefult();
            VoiceOverManager.Instance.PlayMiniGameRandomCorrect();
            miniGameCounter.AddCounter();
        }
        else
        {
            miniGameCounter.SubtractCounter();
            ResetObjectToDefult();
            SoundsManager.Instance.PlaySFXWrong();
            VoiceOverManager.Instance.PlayMiniGameRandomWrong();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canfly)
        {
            Fly();
        }
    }

    private void Fly()
    {
        xTime = Mathf.Clamp(xTime + (Time.deltaTime * xSpeed), 0.0f, 360.0f);
        if (xTime == 360.0f)
        {
            xTime = 0.0f;
        }
        transform.position = new Vector3(transform.position.x + (Mathf.Sin(xTime) * xOffset), transform.position.y + Time.deltaTime * ySpeed, transform.position.z);
    }

    //private void CheckGameOverByCollider()
    //{
    //    if (_isCorrect)
    //    {
    //        miniGameController.EndGameByCorrectDestory();
    //    }
    //}

    public bool GetIsUsed()
    {
        return _isUsed;
    }

    internal void DestroyItemByGameOver()
    {
        correctEffect.Play();
        correctEffect.transform.position = transform.position;
        ResetObjectToDefult();
    }

    public void ResetObjectToDefult()
    {
        _canfly = false;
        _isUsed = false;
        _isCorrect = false;
        if (spriteRenderer != null)
            spriteRenderer.sprite = null;
        transform.localPosition = _originalPos;
    }


}
