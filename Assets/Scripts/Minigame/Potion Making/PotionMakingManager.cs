using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PotionMakingManager : BaseMicrogameClass
{
    public List<Sprite> allPossibleIngredientSprites;
    public List<Image> ingredientSlots;
    public List<Sprite> selectedIngredientSprites;
    public List<Ingredient> allIngredients;
    public Ingredient currentlyDraggedIngredient;
    public int ingredientsMixed;
    public Animator animator;

    public override void StartMicrogame()
    {
        InitializePotionGame();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePotionGame()
    {
        ingredientsMixed = 0;

        Canvas canvas = transform.parent.GetComponent<Canvas>();
        List<Sprite> availableIngredients = new List<Sprite>(allPossibleIngredientSprites);

        foreach (Image ingredientSlot in ingredientSlots)
        {
            ingredientSlot.sprite = availableIngredients[Random.Range(0, availableIngredients.Count)];
            ingredientSlot.SetNativeSize();
            selectedIngredientSprites.Add(ingredientSlot.sprite);
            availableIngredients.Remove(ingredientSlot.sprite);
        }

        foreach (Ingredient ingredient in allIngredients)
        {
            ingredient.canvas = canvas;
            ingredient.potionMakingManager = this;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Works");
        if (other.gameObject == currentlyDraggedIngredient.gameObject)
        {
            currentlyDraggedIngredient.isInPot = true;
        }
    }

    public void IngredientAdded()
    {
        StartCoroutine(IngredientAddedCo());
    }

    private IEnumerator IngredientAddedCo()
    {
        animator.SetBool("RightIngredient", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("RightIngredient", false);

        if (ingredientsMixed == ingredientSlots.Count)
        {
            EndMicrogame();
            Destroy(gameObject);
        }
    }
}
