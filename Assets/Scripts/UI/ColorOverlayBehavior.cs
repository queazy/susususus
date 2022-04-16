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

    public void Update(float t)
    {
        print("ajflajflka");
        float mapped = Mathf.Clamp01(t);
        _overlay.color = _colorGradient.Evaluate(mapped);
    }
}