using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorageUnit : ButtonBase
{
    //click it once then we go to description.
    //click it twice then we buy it.

    StorageUI handler;
    IngredientClass currentIngredient;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] Image icon;

    public void SetUp(StorageUI handler, IngredientClass ingredient)
    {
        if(ingredient.data == null)
        {
            Debug.Log("there is no data");
        }
        nameText.text = ingredient.data.ingredientName;
        quantityText.text = ingredient.quantity.ToString();
        icon.sprite = ingredient.data.ingredientSprite;

        this.handler = handler;
        currentIngredient = ingredient;

    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        //if we detect two clicks in a short amount of time then we buy.
        handler.SelectIngredient(currentIngredient.data);
    }


}
