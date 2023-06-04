using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCook : MonoBehaviour
{

    Cook handler;


    [SerializeField] RecipeData recipe;

    private void Awake()
    {
        handler = GetComponent<Cook>();
    }

    [ContextMenu("DEBUGSTARTCOOKING")]
    public void StartCooking()
    {
        handler.CookOnlyRecipe(recipe);
    }

}
