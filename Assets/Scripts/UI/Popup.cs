using System.Collections;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private string _displayText;
    [SerializeField] private GameObject _overlay;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private float _shakeDegree;
    [SerializeField] private float _shakeTime;
    [SerializeField] private float _shakeDelay;
    public void Display()
    {
        GameObject clone = Instantiate(_overlay);
        clone.transform.SetParent(_canvas.transform);
        clone.transform.SetAsLastSibling();
        RectTransform rt = clone.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector3.zero;
        rt.localScale = Vector3.one;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;
        PopupOverlay behav = clone.GetComponent<PopupOverlay>();
        behav.Display(_displayText);
        Destroy(gameObject);
    }

    private bool _shaking = false;
    private void Update()
    {
        if (_shaking) return;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        _shaking = true;
        float t = 0f;
        while (t < _shakeTime)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(0, -_shakeDegree, t / _shakeTime));
            t += Time.deltaTime;
            yield return null;
        }
        t = 0f;
        while (t < _shakeTime * 2f)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(-_shakeDegree, _shakeDegree, t / (_shakeTime * 2f)));
            t += Time.deltaTime;
            yield return null;
        }
        t = 0f;
        while (t < _shakeTime)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(_shakeDegree, 0f, t / _shakeTime));
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_shakeDelay);
        _shaking = false;
    }
}
