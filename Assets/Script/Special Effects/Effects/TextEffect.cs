using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public float _effectSpeed = 10f;
    public Color updateColorEffect;

    [Header("Slide")]
    [Tooltip("Float 1 = X Value || Float 2 = Y Value")]
    [Range(-1000, 1000)]public float[] slideDistace = new float[2];

    [Header("Grow")]
    [Range(0.4f, 1.5f)] public float growSize = 1f;

    Vector3 _startSize;
    Vector3 _startPosition;
    Text _textObject;

    int effectIndex;

    private void Awake()
    {
        _textObject = transform.GetComponent<Text>();
        _startSize = transform.localScale;
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (effectIndex == 0)
        {
            ShrinkText();
        }

        else if (effectIndex == 1)
        {
            SlideInPlace();
        }
    }

    public void GrowText()
    {
        transform.localScale = new Vector3(growSize, growSize, growSize);
        _textObject.color = updateColorEffect;
        effectIndex = 0;
    }

    void ShrinkText()
    {
        if(Vector2.Distance(transform.position, _startSize) > 0.1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,
            _startSize, _effectSpeed * Time.deltaTime);
        }

        if(_textObject.color != new Color(1, 1, 1))
        {
            _textObject.color += new Color(0.01f, 0.01f, 0.01f);
        }

        else
        {

        }
    }

    public void Slide()
    {
        effectIndex = 1;
        transform.position = new Vector3(_startPosition.x + slideDistace[0], _startPosition.y + slideDistace[1], 0f);
    }

    void SlideInPlace()
    {
        if(Vector2.Distance(transform.position, _startPosition) > 0.1f)
        transform.position = Vector3.Lerp(transform.position,
            _startPosition, _effectSpeed * Time.deltaTime);
    }
}
