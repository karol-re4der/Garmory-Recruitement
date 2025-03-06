using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreHandler : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI PlayTimeText;

    void Update()
    {
        PlayTimeText.text = Shortcuts.PLAYTIME_TEXT.Replace("{0}", Shortcuts.MAIN_HANDLER.PlayTime.ToString(@"hh\:mm\:ss"));
        ScoreText.text = Shortcuts.SCORE_TEXT.Replace("{0}", Shortcuts.MAIN_HANDLER.Score.ToString());
    }
}
