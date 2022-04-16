using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextSlideShow : MonoBehaviour
{
    [SerializeField] private GameObject _textPrefab;
    [SerializeField] private List<string> _sentences = new List<string>();
    [SerializeField] private List<float> _delayBetween = new List<float>();

    private int currIndex = 0;

    private void Start()
    {
        StartCoroutine(Spawn(null));
    }

    public void Next(TextBehavior old)
    {
        currIndex++;
        StartCoroutine(Spawn(old));
    }
    
    private IEnumerator Spawn(TextBehavior old)
    {
        yield return new WaitForSeconds(_delayBetween[Mathf.Clamp(currIndex - 1, 0, _sentences.Count)]);
        if (old) old.Fade();
        if (currIndex >= _sentences.Count)
        {
            SceneManager.LoadScene("Menu");
            yield break;
        }
        GameObject clone = Instantiate(_textPrefab);
        clone.transform.SetParent(transform);
        TextBehavior behav = clone.GetComponent<TextBehavior>();
        behav.DisplayText = _sentences[currIndex];
        behav.SlideShow = this;
    }
}
