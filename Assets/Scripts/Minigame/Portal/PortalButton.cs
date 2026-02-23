using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PortalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int buttonNumber;

    public Image img;
    public Sprite up;
    public Sprite down;

    public void Click()
    {
        FindFirstObjectByType<PortalButtonManager>().ButtonPressWork(buttonNumber);
    }

    // This method is called when the mouse button is released over the button
    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = up;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        FindFirstObjectByType<PortalButtonManager>().ButtonPressWork(buttonNumber);
        img.sprite = down;
    }
}
