using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text _text;
    [SerializeField] private ColorOverlayBehavior _overlay;
    [Header("Aesthetics")]
    [SerializeField] Gradient _temperatureColors;
    [Header("Behavior")]
    [SerializeField] private float _startTemp = 1.0f;
    [SerializeField] private float _maxTemp = 4.0f;
    [SerializeField] private float _rateOfDecay = 0.005f; // temporary

    private float _currTemp;
    private string _degEnd = "°C";
    
    void Start()
    {
        _currTemp = _startTemp;
        _overlay.Update(Map());
        _text.text = "+" + _startTemp.ToString("n1") + _degEnd;
    }

    // Update is called once per frame
    void Update()
    {
        _text.color = DeriveColor(_currTemp);
        _text.text = FormatText(_currTemp);
        _overlay.Update(Map());
        _currTemp += _rateOfDecay * Time.deltaTime;
    }

    private string FormatText(float t)
    {
        string pref = (t > 0f) ? "+" : "-";
        float rounded = Mathf.Round(t * 10.0f) * 0.1f;
        return pref + rounded.ToString("n1") + _degEnd;
    }
    private Color DeriveColor(float t)
    {
        float mapped = Map();
        return _temperatureColors.Evaluate(mapped);
    }

    private float Map()
    {
        return Mathf.Clamp01((_currTemp - _startTemp) / (_maxTemp - _startTemp));
    }
}