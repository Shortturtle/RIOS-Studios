using UnityEngine;
using System.Collections.Generic;

public class IceMGManager : BaseMinigameClass
{
    public int numberOfIceToMelt;
    //private int numberOfIceMelted;

    public override void StartMinigame()
    {
        base.StartMinigame();
    }

    public void IceMelted()
    {
        numberOfIceToMelt--;

        if(numberOfIceToMelt == 0)
        {
            EndMinigame();
            Destroy(this.gameObject);
        }
    }
}
