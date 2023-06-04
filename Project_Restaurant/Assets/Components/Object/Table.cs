using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Table : MonoBehaviour
{
    //the tables and chair connections will come made.
    //you buy the tables and each come wiht one chair so you need to click on the table and buy more to that table.

   
    public List<Chair> chairList = new List<Chair>();
    public Client mainClient = null;
    OrderClass tableOrder;


    public event Action EventTableCleared;
    public void OnTableCleared() => EventTableCleared?.Invoke();

    //when a table is cleared we still cant get a table with less chairs.
    //being skipped in the line will give make them angry.

    RestaurantHandler restaurant;
    

    
    private void Start()
    {
        restaurant = PlayerHandler.instance.restaurant;
    }


    #region CHAIR
    public void AddChair()
    {
        //tables always have four avaiable positions.
        //and that is defined by the number of chairs.
        //the first chair is left, right, up, down

        if (chairList.Count == 4) return;

        Vector3 pos = Vector3.zero;

        if (chairList.Count == 0) pos = Vector3.left;
        if (chairList.Count == 1) pos = Vector3.right;
        if (chairList.Count == 2) pos = Vector3.up;
        if (chairList.Count == 3) pos = Vector3.down;

        Chair template = PlayerHandler.instance.restaurant.chairTemplate;
        Chair newObject = Instantiate(template, transform.position + pos, Quaternion.identity);
        newObject.transform.parent = transform;
        chairList.Add(newObject);
        newObject.SetUp(transform.position + pos, pos * -1);
    }
    public Chair GetFreeChair()
    {
        return null;
    }
    #endregion

    #region CLIENTS
    public bool isFree()
    {
        return mainClient == null;
    }

    public Table AssignClient(Client client)
    {
        mainClient = client;
        return this;
    
    }

    //we have to create these fellas.
    public void ClientSit()
    {
        //

        restaurant.AddWaitingTable(this);

        Vector2 mainChairPos = chairList[0].GetSittingPos();
        mainClient.transform.position = mainChairPos;
        mainClient.IdleAnimation(mainChairPos * -1);
        mainClient.ChangeTask("Waiting for Food");

        for (int i = 0; i < mainClient.followers.Count; i++)
        {
            Vector2 chairPos = chairList[i + 1].GetSittingPos();
            mainClient.followers[i].transform.position = chairPos;
            mainClient.followers[i].IdleAnimation(chairPos);
            mainClient.followers[i].ChangeTask("Waiting for Food");
        }

        //start ordering.
        //



    }

    public void ClientStand()
    {
        //we get the first in the list and put the in the field.
        //we remove everyone else.

        mainClient.transform.position = GetWaiterPos();
        mainClient.IdleAnimation(GetWaiterFacingPos() * -1);

        for (int i = 0; i < mainClient.followers.Count; i++)
        {
            mainClient.followers[i].transform.position = new Vector3(1000, 1000,1000);
        }

        mainClient = null;
        restaurant.OnTableFree();
        tableOrder.isOpen = false;
        tableOrder = null;
        //trigger an event.

    }


    public List<Client> GetFullClientList()
    {
        //all it does is create a list for the mainclient to be inside.
        List<Client> clientList = new();

        clientList.Add(mainClient);

        for (int i = 0; i < mainClient.followers.Count; i++)
        {
            clientList.Add(mainClient.followers[i]);
        }

        return clientList;

    }

    public void StartEating()
    {
        mainClient.ChangeTask("Eating Food");
        foreach (var item in mainClient.followers)
        {
            item.ChangeTask("Eating Food");
        }
        StartCoroutine(EatingProcess());
    }

    public void GetCopyOfOrder(OrderClass order) => tableOrder = order;

    IEnumerator EatingProcess()
    {
        yield return new WaitForSeconds(2);
        RewardPlayer();
        yield return new WaitForSeconds(1);
        LeaveTable();
    }

    void LeaveTable()
    {
        mainClient.LeaveRestaurant();
        ClientStand();      
    }

    void RewardPlayer()
    {
        //give money. can only be more.
        //give reputation. can be negative.

        int money = tableOrder.GetTotalBasePrice();
        PlayerHandler.instance.resource.GainGold(money);
        //do some cool little effect.




    }

    #endregion


    #region POS
    public Vector3 GetWaiterPos()
    {
        if (chairList.Count <= 2) return transform.position + Vector3.up;
        if (chairList.Count == 3) return transform.position + Vector3.down;
        if (chairList.Count == 4) return chairList[0].transform.position + Vector3.up;

        return Vector3.zero;
    }
    public Vector3 GetWaiterFacingPos()
    {
        //if above then face down.
        //if under then face up.
        
        if(chairList.Count <= 2 || chairList.Count == 4)
        {
            return Vector3.down;
        }

        if (chairList.Count == 3)
        {
            return Vector3.up;
        }

        return Vector3.zero;
    }
    #endregion

  

}
