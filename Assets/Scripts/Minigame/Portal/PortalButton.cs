using UnityEngine;

public class PortalButton : MonoBehaviour
{
    public int buttonNumber;

    public void Click()
    {
        FindFirstObjectByType<PortalButtonManager>().ButtonPressWork(buttonNumber);
    }
}
