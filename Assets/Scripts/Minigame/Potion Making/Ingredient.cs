using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //gameplay variables
    public PotionMakingManager potionMakingManager;
    public Canvas canvas;
    public bool isInPot;

    //variables for movement
    private bool isDragStarted;
    private Vector2 ogPos;

    public AK.Wwise.Event ingredientWrong;
    public AK.Wwise.Event ingredientRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ogPos = transform.position; // stores original position
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragStarted)
        {
            //moves the object to mouse position if dragged
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
            //else keep it in original position
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
            //if one of the correct ingredients
            if (potionMakingManager.selectedIngredientSprites.Contains(gameObject.GetComponent<Image>().sprite))
            {
                //plays animation and increases win variable
                int index = potionMakingManager.selectedIngredientSprites.IndexOf(gameObject.GetComponent<Image>().sprite);
                potionMakingManager.selectedIngredientSprites.RemoveAt(index);
                potionMakingManager.ingredientsMixed++;
                potionMakingManager.IngredientAdded();
                AudioManager.instance.PlayAudioEvent(ingredientRight, gameObject);
                Destroy(gameObject);
            }

            else
            {
                AudioManager.instance.PlayAudioEvent(ingredientWrong, gameObject);
            }
        }
        isDragStarted = false;
        potionMakingManager.currentlyDraggedIngredient = null;
    }
}
