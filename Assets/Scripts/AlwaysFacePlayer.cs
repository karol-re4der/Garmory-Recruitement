using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFacePlayer : MonoBehaviour
{
    public Transform Player;
    void Update()
    {
        transform.LookAt(Shortcuts.CHARACTER.transform);
    }
}
