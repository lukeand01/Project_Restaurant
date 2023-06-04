using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingUnit : ButtonBase
{
    BuildingData data;

    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI priceText;


    public void SetUp(BuildingData data)
    {
        this.data = data;
        UpdateUI();
    }

    void UpdateUI()
    {
        if(data.buildingSprite != null) icon.sprite = data.buildingSprite;
        priceText.text = data.price.ToString();

    }
        


    public override void OnPointerClick(PointerEventData eventData)
    {
        //givo order for building handler. 
        PlayerHandler.instance.build.StartBuilding(data);
        //also give order for the ui to behave.

    }

}
