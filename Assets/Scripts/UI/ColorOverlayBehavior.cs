using UnityEngine;
using UnityEngine.UI;

public class ColorOverlayBehavior : MonoBehaviour
{
    [SerializeField] private Image _overlay;
    [SerializeField] private Gradient _colorGradient;
    
    void Start()
    {
        _overlay.color = _colorGradient.Evaluate(0f);
    }

    public void UpdateColor(float t)
    {
        float mapped = Mathf.Clamp01(t);
        _overlay.color = _colorGradient.Evaluate(mapped);
    }
}