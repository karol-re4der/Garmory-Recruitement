using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreHandler : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI NextWaveText;

    void Update()
    {
        NextWaveText.text = Shortcuts.NEXT_WAVE_TEXT.Replace("{0}", ((int)Shortcuts.MAIN_HANDLER.WaveCountdown.TotalSeconds).ToString());
        ScoreText.text = Shortcuts.SCORE_TEXT.Replace("{0}", Shortcuts.MAIN_HANDLER.Score.ToString());
    }
}
