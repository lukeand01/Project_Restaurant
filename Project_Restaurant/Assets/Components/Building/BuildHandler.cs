using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHandler : MonoBehaviour
{
    //build handler.
    //building is a bunch of things you can buy for your restaurant.
    //kitchen, decoration, upgrades

    //you can place stuff 
    public Dictionary<BuildCategoryType, List<BuildingData>> dictionaryKnowBuildings = new();

    private void Awake()
    {
        SetUp();
    }

    void SetUp()
    {
        BuildingUI ui = UIHolder.instance.building;

        dictionaryKnowBuildings.Add(BuildCategoryType.Kitchen, new List<BuildingData>());
        ui.AddCategory(BuildCategoryType.Kitchen);
        dictionaryKnowBuildings.Add(BuildCategoryType.Client, new List<BuildingData>());
        ui.AddCategory(BuildCategoryType.Client);
        dictionaryKnowBuildings.Add(BuildCategoryType.Decoration, new List<BuildingData>());
        ui.AddCategory(BuildCategoryType.Decoration);
        dictionaryKnowBuildings.Add(BuildCategoryType.Upgrade, new List<BuildingData>());
        ui.AddCategory(BuildCategoryType.Upgrade);

    }

    GameObject currentObject = null;

    private void Update()
    {
        
    }

    void HandleBuilding()
    {
        if (currentObject == null) return;

        //otherwise we keep checking for the first touch.

        currentObject.transform.position = Input.touches[0].position;
        //check if that area.



        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            //then we end this.
            PerformAction();
            return;
        }

    }

    void PerformAction()
    {

    }
    
    void PlaceBuilding()
    {

    }
    void EndBuilding()
    {
        Destroy(currentObject);
        currentObject = null;
    }
    public void StartBuilding(BuildingData data)
    {
        //we need to take information from the first 
        //create a new object with the show prefab.
        currentObject = Instantiate(data.showPrefab, Vector3.zero, Quaternion.identity);
        //lower the ui aswell.

    }

}

public enum BuildCategoryType
{
    Kitchen,
    Client,
    Decoration,
    Upgrade
}