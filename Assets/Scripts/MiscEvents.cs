using UnityEngine;
using System;

public class MiscEvents
{
    public event Action onCollectKanade;

    public void KanadeCollected()
    {
        if(onCollectKanade != null)
        {
            onCollectKanade();
        }
    }
}
