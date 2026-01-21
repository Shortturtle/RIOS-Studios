using UnityEngine;
using UnityEngine.EventSystems;

public class Handle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Crank crank;

    public void OnBeginDrag(PointerEventData eventData)
    {
        crank.isDragStarted = true;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        crank.isDragStarted =false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
