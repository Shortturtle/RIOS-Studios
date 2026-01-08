using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rope : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool isLeftRope;
    public bool isSuccess;
    public Color ropeColor;
    private Image ropeImage;

    private bool isDragStarted;
    public UILineRenderer lineRenderer;
    public Canvas canvas;
    public NetConnectManager netConnectManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ropeImage = GetComponent<Image>();
    }

    private void Update()
    {
        lineRenderer.SetAllDirty();
        if (isDragStarted)
        {
            Vector2 movePos;

            // gets end position to move Line Renderer to
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                     lineRenderer.transform as RectTransform,
                     Input.mousePosition,
                     canvas.worldCamera,
                     out movePos);

            lineRenderer.points[0] = Vector3.zero;
            lineRenderer.points[1] = movePos;
        }

        else
        {
            if (!isSuccess)
            {
                // hides line renderer if not dragging and not completed
                lineRenderer.points[0] = Vector3.zero;
                lineRenderer.points[1] = Vector3.zero;
            }
        }

        // checks if rope is hovered
        bool isHovered =
        RectTransformUtility.RectangleContainsScreenPoint(
            transform as RectTransform, Input.mousePosition,
                                    canvas.worldCamera);

        // if yes, sets self to currentHoveredRope
        if (isHovered)
        {
            netConnectManager.currentlyHoveredRope = this;
        }

    }

    public void SetRopeColor(Color color)
    {
            ropeImage.color = color;
            lineRenderer.color = color;
            ropeColor = color;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!isLeftRope) { return; }
        if(isSuccess) { return; }

        isDragStarted = true;
        netConnectManager.currentlyDraggedRope = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (netConnectManager.currentlyHoveredRope != null)
        {
            if (netConnectManager.currentlyHoveredRope.ropeColor == ropeColor &&
                !netConnectManager.currentlyHoveredRope.isLeftRope)
            {
                isSuccess = true;
                netConnectManager.currentlyHoveredRope.isSuccess = true;
            }
        }

        isDragStarted= false;
        netConnectManager.currentlyDraggedRope = null;
    }
}
