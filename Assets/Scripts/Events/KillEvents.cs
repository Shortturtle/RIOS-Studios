using UnityEngine;
using System;

public class KillEvents
{
    public event Action onKillFruit;

    public void FruitKilled()
    {
        if (onKillFruit != null)
        {
            onKillFruit();
        }
    }
}
