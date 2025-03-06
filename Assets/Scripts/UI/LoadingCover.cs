using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LoadingCover : MonoBehaviour
{
    public TextMeshProUGUI LoadingText;

    private int _progress = 0;
    private int _maxProgress = 3;
    private DateTime _lastUpdate = DateTime.MinValue;
    private TimeSpan _updateTime = TimeSpan.FromSeconds(0.5);

    void Update()
    {
        DateTime now = DateTime.Now;
        if (now - _lastUpdate >_updateTime)
        {
            LoadingText.text = Shortcuts.LOADING_TEXT;

            if (_progress > _maxProgress)
            {
                _progress = 0;
            }

            for (int i = 0; i < _progress; i++)
            {
                LoadingText.text += '.';
            }

            _progress++;
            _lastUpdate = now;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
