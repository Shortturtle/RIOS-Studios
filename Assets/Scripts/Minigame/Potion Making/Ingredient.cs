using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public PotionMakingManager potionMakingManager;
    public Canvas canvas;
    private bool isDragStarted;
    public bool isInPot;
    private Vector2 ogPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ogPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragStarted)
        {
            Vector2 movePos;

            // gets moves object to mouse position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                     canvas.transform as RectTransform,
                     Input.mousePosition,
                     canvas.worldCamera,
                     out movePos);

             transform.position = canvas.transform.TransformPoint(movePos);
        }

        else
        {
            transform.position = ogPos;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragStarted = true;
        potionMakingManager.currentlyDraggedIngredient = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isInPot)
        {
            if (potionMakingManager.selectedIngredientSprites.Contains(gameObject.GetComponent<Image>().sprite))
            {
                int index = potionMakingManager.selectedIngredientSprites.IndexOf(gameObject.GetComponent<Image>().sprite);
                potionMakingManager.selectedIngredientSprites.RemoveAt(index);
                potionMakingManager.ingredientsMixed++;
                potionMakingManager.IngredientAdded();
                Destroy(gameObject);
            }
        }
        isDragStarted = false;
        potionMakingManager.currentlyDraggedIngredient = null;
    }
}
