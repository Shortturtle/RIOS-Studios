using UnityEngine;
using System;

public class MiscEvents
{
    //this is the base code for collecting coins or whatever objects [so create an Action, and a Function, and place the Action under the Function created]
    //(idk if need to change for other things but im pretty sure not since the other scripts are referencing the function which calls the action)
    public event Action onCollectKanade;

    public void KanadeCollected()
    {
        if(onCollectKanade != null) { onCollectKanade(); }
    }

    public event Action talkedToPeople;

    public void PeopleInteracted()
    {
        if (talkedToPeople != null) { talkedToPeople(); }
    }
}
