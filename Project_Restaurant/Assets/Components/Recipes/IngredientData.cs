using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ingredient")]
public class IngredientData : ScriptableObject
{
    //
    public string ingredientName;
    public Sprite ingredientSprite;
    public int ingredientPrice;
    public IngredientType ingredientType;

}
public enum IngredientType
{
    Basic,
    Cold,
    Magical
}

[System.Serializable]
public class IngredientClass 
{
    public IngredientData data;
    public int quantity;

    public IngredientClass(IngredientData data, int quantity = 1)
    {
        this.data = data;
        this.quantity = quantity;
    }

    public void RemoveQuantity(int change)
    {
        quantity -= change;
        quantity = Mathf.Clamp(quantity, 0, int.MaxValue);
        
    }

    public void AddQuantity(int change)
    {
        quantity += change;
    }
}