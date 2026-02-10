using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PotionMakingManager : BaseMicrogameClass
{ 
    // Lists to manage all the ingredients
    public List<Sprite> allPossibleIngredientSprites;
    public List<Image> ingredientSlots;
    public List<Sprite> selectedIngredientSprites;
    public List<Ingredient> allIngredients;

    //variables to track game state
    public Ingredient currentlyDraggedIngredient; // ingredient currently being dragged
    public int ingredientsMixed; //responsible for ending game
    public Animator animator;

    public GameObject tickImg;

    public override void StartMicrogame()
    {
        InitializePotionGame(); //starts game
    }


    public void InitializePotionGame()
    {
        ingredientsMixed = 0; 

        Canvas canvas = transform.parent.GetComponent<Canvas>(); //gets current canvas for movement tracking
        List<Sprite> availableIngredients = new List<Sprite>(allPossibleIngredientSprites); //gets all possible ingredients and puts it in a list

        foreach (Image ingredientSlot in ingredientSlots)
        {
            ingredientSlot.sprite = availableIngredients[Random.Range(0, availableIngredients.Count)]; //sets sprite to random one available
            ingredientSlot.SetNativeSize(); //makes it normal sized
            selectedIngredientSprites.Add(ingredientSlot.sprite); //sets it as a selected ingredient
            availableIngredients.Remove(ingredientSlot.sprite); // removes from pool
        }

        foreach (Ingredient ingredient in allIngredients)
        {
            ingredient.canvas = canvas; // for movement
            ingredient.potionMakingManager = this; //for game tracking
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
            EndMicrogame(tickImg);
        }
    }
}
