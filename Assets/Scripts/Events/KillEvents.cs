using UnityEngine;
using System;

public class KillEvents
{
    public event Action onKillMushroom;

    public void MushroomsKilled()
    {
        if (onKillMushroom != null)
        {
            Debug.Log("Killed");
            onKillMushroom();
        }
    }
}
