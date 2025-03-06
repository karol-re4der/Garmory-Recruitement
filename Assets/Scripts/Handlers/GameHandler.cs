using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameHandler : MonoBehaviour
{
    public PlayerMovementHandler MovementHandler;
    public bool IsRunning = true;
    public int Score = 0;

    public TimeSpan PlayTime = new TimeSpan();

    //Game loop
    void Update()
    {
        if (IsRunning)
        {
            MovementHandler.HandleInputs();

            PlayTime += TimeSpan.FromSeconds(Time.deltaTime);
        }
    }

    public void SetRunning(bool state)
    {
        IsRunning = state;
    }
}
