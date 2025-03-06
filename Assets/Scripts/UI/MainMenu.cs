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

    protected override void _onFadedIn()
    {
        Cursor.lockState = CursorLockMode.None;
        Shortcuts.MAIN_HANDLER.SetRunning(false);
    }

    protected override void _onFadedOut()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Shortcuts.MAIN_HANDLER.SetRunning(true);
        Shortcuts.TOAST.Hide();
    }
}
