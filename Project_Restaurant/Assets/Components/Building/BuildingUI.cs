using MyBox;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] GameObject holder;
    Vector3 originalPos;
    bool isOpen;
    private void Awake()
    {
        originalPos = holder.transform.position;
    }

    [Separator("CATEGORY")]
    [SerializeField] GameObject containerTemplate;
    [SerializeField] BuildingUnit buttonTemplate;
    [SerializeField] CategoryUnit categoryTemplate;
    [SerializeField] Transform categoryContainer;
    Dictionary<BuildCategoryType, GameObject> dictionaryCategories = new();
    List<GameObject> listCategoryHolders = new();
    public void ChangeCategory(BuildCategoryType categoryType)
    {
        if (!dictionaryCategories.ContainsKey(categoryType)) return;

        foreach (var item in listCategoryHolders)
        {
            item.SetActive(false);
        }

        dictionaryCategories[categoryType].SetActive(true);
    }

    public void AddCategory(BuildCategoryType categoryType)
    {
        //we add it to a category.
        GameObject newObject = Instantiate(containerTemplate, holder.transform.position, Quaternion.identity);
        newObject.SetActive(false);
        listCategoryHolders.Add(newObject);
        dictionaryCategories.Add(categoryType, newObject);

        CategoryUnit secondObject = Instantiate(categoryTemplate, categoryContainer.transform.position, Quaternion.identity);
        secondObject.SetUp(categoryType);
        secondObject.transform.parent = categoryContainer;

        if (dictionaryCategories.Count == 1)
        {
            ChangeCategory(categoryType);
        }
    }
    public void AddBuildingButton(BuildCategoryType categoryType, BuildingData data)
    {
        Transform rightHolder = dictionaryCategories[categoryType].transform;
        BuildingUnit newObject = Instantiate(buttonTemplate, rightHolder.position, Quaternion.identity);
        newObject.SetUp(data);
        newObject.transform.parent = rightHolder;

    }

   


   

    #region OPEN AND CLOSE

    public void ControlUI()
    {
        if (isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(CloseProcess());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(OpenProcess());
        }  
    }
    IEnumerator OpenProcess()
    {
        StopAllCoroutines();
        isOpen = true;

        while (holder.transform.position.y < originalPos.y + 40)
        {
            holder.transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.001f);
        }
    }
    IEnumerator CloseProcess()
    {
        StopAllCoroutines();
        isOpen = false;

        while (holder.transform.position.y > originalPos.y)
        {
            holder.transform.position -= new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.001f);
        }
    }

    #endregion


}
