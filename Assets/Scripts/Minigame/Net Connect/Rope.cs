using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Rope : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool isLeftRope;
    public bool isSuccess;
    public bool isHovered;
    public Color ropeColor;
    public Material ropeMaterial;
    public Material lineRendererMaterial;
    private Image ropeImage;

    private bool isDragStarted;
    public UILineRenderer lineRenderer;
    public Canvas canvas;
    public NetConnectManager netConnectManager;
    public AK.Wwise.Event ropeConnect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ropeImage = GetComponent<Image>();
    }

    private void Update()
    {
        lineRenderer.SetAllDirty();
        lineRenderer.SetMaterialDirty();
        if (isDragStarted)
        {
            Vector2 movePos;

            // gets end position to move Line Renderer to
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                     lineRenderer.transform as RectTransform,
                     Input.mousePosition,
                     canvas.worldCamera,
                     out movePos);

            lineRenderer.Points[0] = Vector3.zero;
            lineRenderer.Points[1] = movePos;
        }

        else
        {
            if (!isSuccess)
            {
                // hides line renderer if not dragging and not completed
                lineRenderer.Points[0] = Vector3.zero;
                lineRenderer.Points[1] = Vector3.zero;
            }
        }

        // checks if rope is hovered
        isHovered =
        RectTransformUtility.RectangleContainsScreenPoint(
            transform as RectTransform, Input.mousePosition,
                                    canvas.worldCamera);

        // if yes, sets self to currentHoveredRope
        if (isHovered)
        {
            netConnectManager.currentlyHoveredRope = this;
        }

    }

    public void SetRopeColorAndSprite(Color color, Sprite sprite)
    {
        ropeImage.color = color;
        Material tempMat =  new Material(ropeMaterial);
        tempMat.mainTexture = sprite.texture;
        ropeImage.material = tempMat;
        lineRenderer.sprite = sprite;
        lineRenderer.color = color;
        ropeColor = color;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!isLeftRope) { return; } // ensures only left rope can be dragged
        if(isSuccess) { return; } // checks if not complete

        isDragStarted = true;
        netConnectManager.currentlyDraggedRope = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (netConnectManager.currentlyHoveredRope != null)
        {
            if (netConnectManager.currentlyHoveredRope.ropeColor == ropeColor &&
                !netConnectManager.currentlyHoveredRope.isLeftRope) // if hovering on right rope of same color
            {
                isSuccess = true;
                netConnectManager.currentlyHoveredRope.isSuccess = true;
                AudioManager.instance.PlayAudioEvent(ropeConnect, gameObject);
            }
        }

        isDragStarted= false;
        netConnectManager.currentlyDraggedRope = null;
    }
}
