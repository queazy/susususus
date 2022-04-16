using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOverlay : MonoBehaviour
{
    [SerializeField] private Text _text;
    public void Display(string txt)
    {
        _text.text = txt;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }
}
