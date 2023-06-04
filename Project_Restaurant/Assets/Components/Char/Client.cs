using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : CharBase
{
    //can be clicked as well.
    //the behavior will be made in another class not monobehavior.
    //for now what they will do is walk to the restaurant.
    //it asks if there is open space to the restauranthandler.
    //client can form groups that want that amount of chairs to be available.
    //those groups are formed with one leader.
    //maybe what we can have is templates of client groups.
    //for now i will simmply do this.

    public int startClient;
    public List<Client> followers = new();

    public event Action EventClientCancel;
    public void OnClientCancel() => EventClientCancel?.Invoke();

    public int GetFollowers()
    {
        return 1 + followers.Count;
    }
    //if they have a leader then they will follow the leader.
    //but they wont occupy their own square. for now they wont appear at all in the scream.

    #region DESCRIPTION

    public override void GetDescription()
    {
        //
        DescriptionClass description = new DescriptionClass(this, staff.name);
        UIHolder.instance.staffDescription.SetUp(description);

    }

    #endregion

    public void SetUpClient(bool follower = false)
    {
        //SetUp();
        if(!follower)StartClient();
    }
    

    public void LeaveRestaurant()
    {
        //they will walk to one of the exit points and then unload.
        ChangeTask("Leaving restaurant");
        StartCoroutine(LeaveProcess());
    }

    IEnumerator LeaveProcess()
    {

        Transform wayOut = PlayerHandler.instance.restaurant.GetRandomEntrance();

        if (wayOut == null)
        {
            Debug.LogError("couldnt find a way out");
            yield break;
        }

        //take table.
        MoveToVector(wayOut.position);

        while (isWalking)
        {
            yield return null;
        }

        //once we get to the way out just remove this fella
        Destroy(gameObject);
    }
    

    void StartClient()
    {
        //client always does this.
        for (int i = 0; i < startClient; i++)
        {
            Client newObject = Instantiate(this, new Vector3(1000, 1000, 1000), Quaternion.identity);
            //newObject.SetUp();
            followers.Add(newObject);
        }


        RestaurantHandler handler = PlayerHandler.instance.restaurant;
        Table table = handler.GetTable(GetFollowers());

        if (table != null)
        {
            MoveToTable(table);
        }
        else
        {

            handler.AddToQueue(this);
        }
    }


    public void MoveToTable(Table table)
    {
        ChangeTask("Moving to Table");
        StartCoroutine(MoveProcess(table));
    }

    IEnumerator MoveProcess(Table table)
    {
        table.AssignClient(this);
        MoveToVector(table.transform.position);

        while (isWalking)
        {
            yield return null;
        }

        table.ClientSit();

    }

    //he will ask the restaurant as soon as he starts if there for the table. otherwise he waits by the queue.

    [ContextMenu("DEBUGCANCELORDER")]
    public void CancelOrder()
    {
        //here the client cancels the order and leaave the restuarant. hurting its reputation.
        //info the cook.

        //tell anyone interacting with this fella to stop.


        //deal reputatio damage.

        LeaveRestaurant();

    }

}

