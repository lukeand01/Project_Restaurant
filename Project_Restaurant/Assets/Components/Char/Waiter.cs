
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : CharBase
{
    //waiters have etwo functions: get orders and bring the food.
    //they prioritize first orders.

    //i think waiter should be constantly checking for something to do rather than reelying on events.
    //
    bool acting;
    Table tableServing;
    RestaurantHandler restaurant;
    OrderClass currentOrder;

    public override void GetDescription()
    {
        DescriptionClass description = new DescriptionClass(this, staff.name);
        UIHolder.instance.staffDescription.SetUp(description);
    }

    private void Start()
    {
        //SetUp();
    }
    public override void SetUp(StaffClass staff)
    {
        base.SetUp(staff);
        restaurant = PlayerHandler.instance.restaurant;
    }

    private void Update()
    {
        if (acting) return;
       
        if (HasClient())
        {
            ChangeTask("Moving to Client");
            tableServing = restaurant.waitingTable[0];
            restaurant.RemoveWaitingTable(tableServing);
            MoveToVector(tableServing.GetWaiterPos());
            EventArrived += WaiterArrivedInClient;
            acting = true;
            return;
        }

        if (HasFood())
        {
            ChangeTask("Getting food");
            currentOrder = restaurant.GetFoodReady();
            StartCoroutine(DeliverFoodProcess());
            acting = true;

        }
        ChangeTask("");
    }

    //the food is simply getting a list from a the food holder.
    //the client 

    IEnumerator DeliverFoodProcess()
    {


        MoveToVector(restaurant.foodHolder.GetWaiterPos());

        while (isWalking)
        {
            yield return null;
        }
        IdleAnimation(restaurant.foodHolder.GetWaiterFacingPos());
        yield return new WaitForSeconds(2);


        MoveToVector(currentOrder.table.GetWaiterPos());

        while (isWalking)
        {
            yield return null;
        }

        IdleAnimation(currentOrder.table.GetWaiterFacingPos());

        yield return new WaitForSeconds(2);


        currentOrder.table.StartEating();
    }


    void WaiterArrivedInClient()
    {     
        EventArrived -= WaiterArrivedInClient;       
        IdleAnimation(tableServing.GetWaiterFacingPos());

        List<Client> clientList = tableServing.GetFullClientList();
        StartCoroutine(GettingOrderProcess(clientList));

    }

    IEnumerator GettingOrderProcess(List<Client> clientList)
    {
        tableServing.mainClient.EventClientCancel += Cancel;
        List<RecipeData> orderList = new();
        List<RecipeData> menuList = PlayerHandler.instance.chef.knownRecipeList;
        ChangeTask("Getting food order");

         
        for (int i = 0; i < clientList.Count; i++)
        {
            int choice = Random.Range(0, menuList.Count);
            orderList.Add(menuList[choice]);
            yield return new WaitForSeconds(1.5f);

        }

        tableServing.mainClient.EventClientCancel -= Cancel;
        OrderClass order = new OrderClass(tableServing, orderList);
        tableServing.GetCopyOfOrder(order);
        restaurant.AddOrder(order);      
        yield return new WaitForSeconds(2);      
        acting = false;
    }


    void Cancel()
    {
        StopAllCoroutines();
        tableServing.mainClient.EventClientCancel -= Cancel;
    }

    bool HasFood()
    {
        return restaurant.foodReady.Count > 0;
    }
    bool HasClient()
    {
        return restaurant.waitingTable.Count > 0;
    }


}

