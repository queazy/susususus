using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehavior : MonoBehaviour
{
    public string DisplayText;
    public TextSlideShow SlideShow;
    [SerializeField] private Text _text;
    [SerializeField] private RectTransform _rt;
    [SerializeField] private Vector3 _spawnPos;
    [SerializeField] private Vector3 _targetPos = Vector3.zero;
    [SerializeField] private float _time;
    
    // Start is called before the first frame update
    void Start()
    {
        _text.text = DisplayText;
        StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        float t = 0f;
        Color tc = _text.color;
        while (t < _time)
        {
            _rt.anchoredPosition = Vector3.Lerp(_spawnPos, _targetPos, t / _time);
            float alpha = Mathf.Lerp(0f, 1f, t / _time);
            _text.color = new Color(tc.r, tc.g, tc.b, alpha);
            t += Time.deltaTime;
            yield return null;
        }

        SlideShow.Next(this);
    }

    public void Fade()
    {
        StartCoroutine(FadeAway());
    }
    
    private IEnumerator FadeAway()
    {
        float t = 0f;
        Color tc = _text.color;
        while (t < _time)
        {
            _rt.anchoredPosition = Vector3.Lerp(_targetPos, _targetPos + (_targetPos - _spawnPos), t / _time);
            float alpha = Mathf.Lerp(1f, 0f, t / _time);
            _text.color = new Color(tc.r, tc.g, tc.b, alpha);
            t += Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
