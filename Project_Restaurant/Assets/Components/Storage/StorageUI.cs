using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorageUI : MonoBehaviour
{
    GameObject holder;
    PlayerResource handler;




    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
        cannotBuyColor = buttonImage.color;
    }
    private void Start()
    {
        handler = PlayerHandler.instance.resource;
    }

    public bool IsHolder() => holder.activeInHierarchy;

    public void OpenUI()
    {
        UIHolder.instance.player.EventCloseOption += CloseUI;
        holder.SetActive(true);
    }
    public void CloseUI()
    {
        UIHolder.instance.player.EventCloseOption -= CloseUI;
        holder.SetActive(false);
    }

    [SerializeField] StorageUnit template;
    [SerializeField] Transform container;
    
    public Dictionary<IngredientData, StorageUnit> storageDictionary = new(); 
    public void AddUnit(IngredientClass ingredient)
    {
        if (ingredient.data == null) Debug.Log("no data");

        StorageUnit newObject = Instantiate(template, container.transform.position, Quaternion.identity);
        newObject.transform.parent = container.transform;
        newObject.SetUp(this, ingredient);

        storageDictionary.Add(ingredient.data, newObject);
    }

    public void UpdateUnit(IngredientClass ingredient)
    {
        storageDictionary[ingredient.data].SetUp(this,ingredient);
    }

    [Separator("Descriptor")]
    [SerializeField] Image portrait;
    [SerializeField] TextMeshProUGUI descriptorName;
    [SerializeField] TextMeshProUGUI descriptorPrice;
    [SerializeField] TextMeshProUGUI storageText;
    [SerializeField] Image buttonImage;
    IngredientData currentIngredient;

    Color canBuyColor;
    [SerializeField] Color cannotBuyColor;

    public void SelectIngredient(IngredientData ingredient)
    {
        if (!storageDictionary.ContainsKey(ingredient)) return;
        //then we select it.
        currentIngredient = ingredient;
        portrait.sprite = ingredient.ingredientSprite;
        descriptorName.name = ingredient.ingredientName;
        descriptorPrice.text = "Price: " + ingredient.ingredientPrice.ToString();
        storageText.text = "Owned: " + PlayerHandler.instance.resource.GetResourceQuantity(ingredient).ToString();
        HandleBuyButton();
        

    }

    void HandleBuyButton()
    {
        if (handler.HasGold(currentIngredient.ingredientPrice))
        {
            buttonImage.color = canBuyColor;
        }
        else
        {
            buttonImage.color = cannotBuyColor;
        }
    }

    public void Buy()
    {
        if (currentIngredient == null) return;
        if (!handler.HasGold(currentIngredient.ingredientPrice)) return;

        handler.AddSingleIngredient(new IngredientClass(currentIngredient));
        storageText.text = "Owned: " + handler.GetResourceQuantity(currentIngredient).ToString();
        HandleBuyButton();
    }
    public void UpdateBuyButton()
    {

    }


}
