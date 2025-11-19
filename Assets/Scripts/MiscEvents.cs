using UnityEngine;
using System;

public class MiscEvents : MonoBehaviour
{
    public event Action testAction;

    public void TestActionFunction()
    {
        if(testAction != null)
        {
            testAction();
        }
    }
}
