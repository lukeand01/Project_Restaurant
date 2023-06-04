using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe/Recipe")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public Sprite recipeSprite;
    public int priceBase;
    public List<IngredientClass> ingredientList = new();
    public List<ProcedureClass> processList = new();
    //it has procedure. so it goes through different thing.



    //whats stats this has?
    //reputation, difficulty, price, what other events.

}
[System.Serializable]
public class ProcedureClass
{
    public ProcedureType type;
    public int cycles;
    public float cycleTime = 1;
}
public enum ProcedureType
{
    Boil,
    Fry,
    Oven,
    Cutting
}

public class FoodClass
{
    //its a recipe, but with information relating to how good it is
    public RecipeData data;
    public int quality;


    public FoodClass(RecipeData data, int quality )
    {
        this.data = data;
        this.quality = quality;
    }


}
