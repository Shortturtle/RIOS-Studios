using UnityEngine;
using System.Collections.Generic;

public class IceMGManager : BaseMicrogameClass
{
    public int numberOfIceToMelt;
    //private int numberOfIceMelted;

    public override void StartMicrogame()
    {
        base.StartMicrogame();
    }

    public void IceMelted()
    {
        numberOfIceToMelt--;

        if(numberOfIceToMelt == 0)
        {
            EndMicrogame();
            Destroy(this.gameObject);
        }
    }
}
