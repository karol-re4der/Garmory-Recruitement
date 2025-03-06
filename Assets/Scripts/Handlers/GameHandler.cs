using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public PlayerMovementHandler MovementHandler;
    public bool IsRunning = true;

    //Game loop
    void Update()
    {
        if (IsRunning)
        {
            MovementHandler.HandleInputs();
        }
    }
}
