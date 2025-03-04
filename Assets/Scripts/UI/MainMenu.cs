using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class MainMenu : SubMenu
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Switch();
        }
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void StartGame()
    {
        throw new NotImplementedException();
    }
}
