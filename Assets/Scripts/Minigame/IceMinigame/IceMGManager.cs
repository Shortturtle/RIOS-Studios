using UnityEngine;
using System.Collections.Generic;

public class IceMGManager : MonoBehaviour
{
    public int numberOfIceToMelt;
    //private int numberOfIceMelted;

    public void IceMelted()
    {
        numberOfIceToMelt--;

        if(numberOfIceToMelt == 0)
        {
            //call end to minigame
            Debug.Log("end minigame");
        }
    }
}
