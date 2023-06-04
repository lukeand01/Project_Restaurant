using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerResource : MonoBehaviour
{
    //this is where we deal with resources
    //storage
    //money, influence and etc.

    //storage

    public List<IngredientClass> ingredientList = new();
    public List<IngredientType> allowedIngredientList = new();

    public int gold;
    public int reputation;
    public int chest;
    public int key;
    public int storageLimit;



    private void Start()
    {
        SetUpUI();
    }

    void SetUpUI()
    {
        StorageUI storage = UIHolder.instance.storage;
        for (int i = 0; i < ingredientList.Count; i++)
        {
            storage.AddUnit(ingredientList[i]);
        }
    }

    #region STORAGE
    public bool CanAddIngredient(IngredientType type)
    {
        for (int i = 0; i < allowedIngredientList.Count; i++)
        {
            if (allowedIngredientList[i] == type) return true;
        }
        return false;
    }
    public void AddIngredientType(IngredientType type)=> allowedIngredientList.Add(type);
    public bool HasEnoughIngredient(List<IngredientClass> requestList)
    {
        List<IngredientClass> intersectionList = GetIntersection(requestList, ingredientList);

        if(intersectionList.Count != requestList.Count)
        {
            Debug.Log("intersection difference");
            return false;
        }


        for (int i = 0; i < requestList.Count; i++)
        {
            for (int y = 0; y < intersectionList.Count; y++)
            {
                //we check if everyone has the thing.
                if (requestList[i].data != intersectionList[y].data) continue;
                if (requestList[i].quantity > intersectionList[y].quantity)
                {
                    return false;
                }


            }
        }



        return true;
    }
    public List<IngredientClass> GetNeedList(List<IngredientClass> requestList)
    {
        //get a the difference.
        List<IngredientClass> newList = new();

        foreach (var item in requestList)
        {
            newList.Add(new IngredientClass(item.data, item.quantity));
        }

        for (int i = 0; i < requestList.Count; i++)
        {
            for (int y = 0; y < ingredientList.Count; y++)
            {
                if (requestList[i].data == ingredientList[y].data)
                {
                    newList[i].RemoveQuantity(ingredientList[y].quantity);
                }

            }
        }


        for (int i = 0; i < newList.Count; i++)
        {
            if (newList[i].quantity <= 0)
            {
                newList.Remove(newList[i]);
                i--;
            }
        }




        return newList;
    }
    List<IngredientClass> GetIntersection(List<IngredientClass> listA, List<IngredientClass> listB)
    {
        //create a list with the same itens.
        List<IngredientClass> newList = new();

        for (int i = 0; i < listA.Count; i++)
        {
            for (int y = 0; y < listB.Count; y++)
            {
                if (listA[i].data == listB[y].data)
                {
                    newList.Add(new IngredientClass(listA[i].data, listA[i].quantity));
                    continue;
                }
            }
        }
        
        return newList;
    }
    List<IngredientClass> GetDifference(List<IngredientClass> listA, List<IngredientClass> listB)
    {
        //here we get all fellas that are not in the other.
        //and all fellas that have less than their counterpart.



        return null;
    }
    public void AddIngredientList(List<IngredientClass> ingredientList)
    {
        foreach (var item in ingredientList)
        {
            AddSingleIngredient(item);
        }
    }
    public void AddSingleIngredient(IngredientClass ingredient)
    {
        //check if it already exists otherwise we create a new one.

        foreach (var item in ingredientList)
        {
            if(item.data == ingredient.data)
            {
                //then we add it and return
                //are there limits?
                item.AddQuantity(ingredient.quantity);
                //UIHolder.instance.storage.UpdateUnit(item);
                return;
            }
        }

        ingredientList.Add(new IngredientClass(ingredient.data, ingredient.quantity));
        //UIHolder.instance.storage.AddUnit(ingredient);

    }
    public void SpendIngredient(List<IngredientClass> requestList)
    {
        foreach (var item in requestList)
        {
            SpendSingleIngredient(item);
        }
    }
    void SpendSingleIngredient(IngredientClass request)
    {
        foreach (var item in ingredientList)
        {
            if(item.data == request.data)
            {
                item.RemoveQuantity(request.quantity);
                //UIHolder.instance.storage.UpdateUnit(item);
                return; 
            }
        }
    }

    public int GetResourceQuantity(IngredientData ingredient)
    {
        foreach (var item in ingredientList)
        {
            if (item.data == ingredient) return item.quantity;
        }
        return 0;
    }

    #endregion



    #region MONEY
    public void GainGold(int value)
    {
        gold += value;
        UIHolder.instance.player.UpdateGold(gold);
    }
    public bool HasGold(int value)
    {
        return gold >= value;
    }

    #endregion
}
