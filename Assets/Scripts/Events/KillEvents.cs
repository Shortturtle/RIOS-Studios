using UnityEngine;
using System;

public class KillEvents
{
    public event Action onKillFruit;

    public void KanadeCollected()
    {
        if (onKillFruit != null)
        {
            onKillFruit();
        }
    }
}
