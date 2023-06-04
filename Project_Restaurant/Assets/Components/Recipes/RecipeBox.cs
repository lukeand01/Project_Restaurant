using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Box")]
public class RecipeBox : ScriptableObject
{
    public string boxName;
    public Sprite boxSprite;
    public int boxCost;
    public List<RecipeData> recipeList = new();

    //each box is opened by a resource.
 
}


//how do i create 