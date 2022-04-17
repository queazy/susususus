using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParagraphScroll : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 0.5f;
    [SerializeField] private float _endDelay = 3f;
    [SerializeField] private RectTransform _paragraph;

    private float _endY = 2020f;
    private bool _ended = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ended = true;
            Warp();
        }
        if (_paragraph.position.y < _endY)
        {
            _paragraph.position += _scrollSpeed * Time.deltaTime * Vector3.up;
        }
        else
        {
            if (_ended) return;
            StartCoroutine(EndScreen(_endDelay));
        }
    }

    private IEnumerator EndScreen(float delay = 0f)
    {
        _ended = true;
        yield return new WaitForSeconds(delay);
        Warp();
    }

    private void Warp()
    {
        SceneManager.LoadScene("Menu");
    }
}
