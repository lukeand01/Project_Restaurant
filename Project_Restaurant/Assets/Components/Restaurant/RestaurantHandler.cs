using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestaurantHandler : MonoBehaviour
{
    //this keeps check on tables and queue.
    public List<Table> tableList = new();
    public List<Table> waitingTable = new(); //these are the clients that are arrived but havent met a waiter yet.
    public List<Client> queueList = new(); //these are the client waiting.
    


    [Separator("PREFABS")]
    public Chair chairTemplate;
    public Table tableTemplate;

    public Transform[] entrance;

    //groups of client might be formed. 
    //we always to look to get a table has the least necessary of chairs. if that cant be done.
    //also cannot a table that has less tahn teh required.

    private void Awake()
    {
        SetUpRestaurantValue();
    }


    #region EVENTS


    public event Action EventTableFree;
    public void OnTableFree() => EventTableFree?.Invoke();


    #endregion


    #region TABLE
    public void AddTable(Table table)
    {
        table.EventTableCleared += TableCleared;
        tableList.Add(table);
    }   

    public void RemoveTable(Table table)
    {
        table.EventTableCleared -= TableCleared;
        tableList.Remove(table);
    }

    public void AddWaitingTable(Table table)
    {
        waitingTable.Add(table);
    }
    public void RemoveWaitingTable(Table table)
    {
        waitingTable.Remove(table);
    }


    void TableCleared()
    {
        //if the table has just been liberated we check if there is queeu line and then 
        if (queueList.Count <= 0) return;

        for (int i = 0; i < queueList.Count; i++)
        {
            Table table = GetTable(queueList[i].GetFollowers());
            if(table != null)
            {
                //and we give the table to this especific client.
                Debug.Log("give this table to the fella");
                return;
            }

        }   


        
    }


    public Table GetTable(int clients)
    {
        Table bestCandidate = null;
        for (int i = 0; i < tableList.Count; i++)
        {
            if (!tableList[i].isFree()) continue;
            if (tableList[i].chairList.Count < clients) continue;
            if (tableList[i].chairList.Count == clients) return tableList[i];


            if(bestCandidate == null)
            {
                bestCandidate = tableList[i];
            }
            else
            {
                if(tableList[i].chairList.Count < bestCandidate.chairList.Count)
                {
                    bestCandidate = tableList[i];
                }
            }
        }

        //we give the bestcanditate but if thats null then thats means there are no more tables.
        return bestCandidate;
    }


    #endregion

    public void AddToQueue(Client client)
    {
        //give the position to wait and put it in the list.

    }

    //

    #region FOOD

    [Separator("FOOD")]
    public List<OrderClass> orderList = new();
    public List<OrderClass> foodReady = new();

    //should we hold food in certain places?


    public void AddFoodReady(OrderClass order) => foodReady.Add(order);
    public void RemoveFoodReady(OrderClass order) => foodReady.Remove(order);

    public void AddOrder(OrderClass order) => orderList.Add(order);

    public void RemoveOrder(OrderClass order) => orderList.Remove(order);

    public OrderClass GetOrder()
    {
        if (orderList.Count <= 0) return null;

        OrderClass order = new OrderClass(orderList[0].table, orderList[0].orderList);
        orderList.RemoveAt(0);
        return order;

    }

    public OrderClass GetFoodReady()
    {
        if (foodReady.Count <= 0) return null;

        OrderClass order = new OrderClass(foodReady[0].table, foodReady[0].orderList);
        foodReady.RemoveAt(0);
        return order;
    }

    public void GetRandomFood()
    {
        //it comes here and already leave it in the list.


    }



    #endregion


    [Separator("FOOD STATIONS")]
    public FoodHolder foodHolder;
    public Storage storage;
    public List<FoodStation> foodStationList = new();


    #region STORAGE


    public Vector3 GetStoragePos()
    {
        return storage.transform.position + Vector3.left;
    }





    #endregion


    #region FOOD STATION

    public FoodStation GetFoodStation(ProcedureType type)
    {
        foreach (FoodStation item in foodStationList)
        {
            if (item.HasUtility(type)) return item;
        }
        Debug.LogError("something wrong here " + type);
        return null;
    }

    #endregion


    #region WAY OUT

    public Transform GetRandomEntrance()
    {
        if (entrance.Length == 0) return null;

        return entrance[UnityEngine.Random.Range(0, entrance.Length)];
    }

    #endregion


    #region RESTAURANT VALUES
    //this shows how much of value it has for each thing
    public Dictionary<ClientType, float> dictionaryRestaurantValue = new();
    List<ClientType> restaurantValueList = new();
    void SetUpRestaurantValue()
    {
        dictionaryRestaurantValue.Add(ClientType.Civilian, 0);
        restaurantValueList.Add(ClientType.Civilian);
        dictionaryRestaurantValue.Add(ClientType.Gangster, 0);
        restaurantValueList.Add(ClientType.Gangster);
        dictionaryRestaurantValue.Add(ClientType.Alien, 0);
        restaurantValueList.Add(ClientType.Alien);
        dictionaryRestaurantValue.Add(ClientType.Magical, 0);
        restaurantValueList.Add(ClientType.Magical);
    }

    public float CheckValue(ClientType client)
    {
        if (!dictionaryRestaurantValue.ContainsKey(client))
        {
            Debug.LogError("PROBLEM CHECK VALUE " + client);
            return -1;
        }

        return dictionaryRestaurantValue[client];
    }

    public void ChangeValue(ClientType client, float value)
    {
        if (!dictionaryRestaurantValue.ContainsKey(client))
        {
            Debug.LogError("PROBLEM CHANGE VALUE " + client);
            return;
        }

        dictionaryRestaurantValue[client] += value;

        if(dictionaryRestaurantValue[client] < 0)
        {
            Debug.LogError("VALUE LESS THAN 0 " + client);
        }

    }

    public bool IsSpawnClientType(ClientType client)
    {
        //
        float random = UnityEngine.Random.Range(0, 100);
        float percentage = GetPercetageClientType(client);

        return random >= percentage;
    }
    public float GetPercetageClientType(ClientType client)
    {
        float totalValue = 0;
        foreach (var item in restaurantValueList)
        {
            totalValue += CheckValue(item);
        }

        float rightValue = CheckValue(client);
        totalValue -= rightValue;

        float percentage = (rightValue / totalValue) * 100;
        Debug.Log("percentage value " + percentage);

        return percentage;

    }

    #endregion
}
[System.Serializable]
public class OrderClass
{
    public bool isOpen;
    public List<RecipeData> orderList = new();
    public List<FoodClass> completeList = new();
    //but also i need to know the quality of them.


    public Table table;
    //this order is only complete whene there is nothing more in the orderlist.

    public void AddReadyFood(FoodClass food)
    {
        completeList.Add(food);
    }

    public OrderClass(Table table, List<RecipeData> orderList)
    {
        isOpen = true;
        this.orderList = orderList;
        this.table = table;
    } 

    public void CloseOrder()
    {
        isOpen = false;
    }
    
    public bool IsComplete()
    {
        return orderList.Count == completeList.Count;
    }

    public List<IngredientClass> GetTotalIngredient()
    {
        List<IngredientClass> newList = new();


        for (int i = 0; i < orderList.Count; i++)
        {
            //we check everyone.
            List<IngredientClass> ingredientList = orderList[i].ingredientList;

            foreach (var item in ingredientList)
            {
                AddToList(item, newList);
            }
             
        }


        return newList;
    }

    public void AddToList(IngredientClass ingredient, List<IngredientClass> useList)
    {
        for (int i = 0; i < useList.Count; i++)
        {
            if (useList[i].data == ingredient.data)
            {
                useList[i].AddQuantity(ingredient.quantity);
                return;
            }
        }

        useList.Add(new IngredientClass(ingredient.data, ingredient.quantity));




    }

    public int GetTotalBasePrice()
    {
        int total = 0;
        foreach (var item in orderList)
        {
            total += item.priceBase;
        }

        return total;
    }
}