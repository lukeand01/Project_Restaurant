
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cook : CharBase
{
    //the cook it takes a current order and it will do its best to fufill it 

    OrderClass currentOrder;
    RestaurantHandler restaurant;
    bool isCooking;


    private void Start()
    {
        //SetUp();
        restaurant = PlayerHandler.instance.restaurant;
    }

    private void Update()
    {
        if (isCooking) return;
        if (IsWorking()) return;

        
        if (HasFreeOrder())
        {
            //then we take the order.
            //get the first
            ChangeTask("Collecting ingredients");
            currentOrder = restaurant.GetOrder();
            StartCoroutine(CookGetIngredientProcess());
            if(currentOrder == null || !currentOrder.isOpen)
            {
                //something wrong;
                Debug.LogError("Something wrong");
            }
        }
        ChangeTask("");
    }

    #region DESCRIPTION
    public override void GetDescription()
    {
        //name, type and currest taks.
        DescriptionClass description = new DescriptionClass(this, staff.name);
        UIHolder.instance.staffDescription.SetUp(description);
    }


    #endregion

    public void CookOnlyRecipe(RecipeData recipe)
    {
        isCooking = true;
        currentOrder = new OrderClass(null, new List<RecipeData>() { recipe});
        StartCoroutine(CookGetIngredientProcess());

    }

    [ContextMenu("Cancel Current Order")]
    void CancelOrder()
    {
        //give back to storage.
        //stop all courotine
        StopAllCoroutines();
        currentOrder.table.mainClient.EventClientCancel -= CancelOrder;

        if (isCooking)
        {
            PlayerHandler.instance.resource.AddIngredientList(currentOrder.GetTotalIngredient());

        }

        currentOrder.isOpen = false;
        currentOrder = null;
        isCooking = false;
    }


    #region PROCESSES
    IEnumerator CookGetIngredientProcess()
    {

        currentOrder.table.mainClient.EventClientCancel += CancelOrder;

        Vector3 pos = restaurant.GetStoragePos();
        MoveToVector(pos);
        //and we call thing when we reach.

        while (isWalking)
        {
            yield return null;
        }

        IdleAnimation(Vector2.right);
        //now lets see if what we need.

        PlayerResource resource = PlayerHandler.instance.resource;
        List<IngredientClass> totalList = currentOrder.GetTotalIngredient();


        if (resource.HasEnoughIngredient(totalList))
        {
            //use this list to continue.
            //
            float timer = totalList.Count;
            float current = 0;
            chargingUI.gameObject.SetActive(true);

            while (timer > current)
            {
                current += Time.deltaTime;
                chargingUI.HandleCharging(current, timer);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            chargingUI.StartScaleAnimation();
            yield return new WaitForSeconds(0.1f);
            chargingUI.TurnOff();

            resource.SpendIngredient(currentOrder.GetTotalIngredient());
            StartCoroutine(CookStationProcess());         
        }
        else
        {
            List<IngredientClass> needList = resource.GetNeedList(currentOrder.GetTotalIngredient());
            

        }


        while (!resource.HasEnoughIngredient(currentOrder.GetTotalIngredient()))
        {
            //waiting for input.
            ChangeTask("waiting for you to buy ingredients");
            yield return null;
        }

    }

    IEnumerator CookStationProcess()
    {
        //this is getting the food to the right food stations.
        List<RecipeData> recipeList = currentOrder.orderList;
        for (int i = 0; i < recipeList.Count; i++)
        {
            //first we find the position we want to be.
            //first we move to the right pos.
            //then we cook and go to the next.
            
            List<ProcedureClass> procedureList = recipeList[i].processList;
            for (int y = 0; y < procedureList.Count; y++)
            {
                

                 FoodStation station = restaurant.GetFoodStation(procedureList[y].type);

                if(station == null)
                {
                    Debug.Log("found no station");
                    yield break;
                }
                ChangeTask("Using food station " + station.name);

                 MoveToVector(station.GetPos());

                while (isWalking)
                {
                    yield return null;
                }

                //then we get there and we can using it.
                IdleAnimation(station.facingDir);

                //start process.
                float cycles = procedureList[y].cycles;
                float cycleTime = procedureList[y].cycleTime;

                float current = 0;
                float total = cycles * cycleTime;

                chargingUI.gameObject.SetActive(true);

                for (int z = 0; z < cycles; z++)
                {
                    //then we complete it and go to the next.
                    current += cycleTime;
                    chargingUI.HandleCharging(current, total);
                    yield return new WaitForSeconds(cycleTime);
                }

                chargingUI.StartScaleAnimation();           
                yield return new WaitForSeconds(0.2f);
                chargingUI.TurnOff();
               
            }
            currentOrder.AddReadyFood(new FoodClass(recipeList[i], 10));

            //we need something to stop this fella if the client ever stops.
        }
        StartCoroutine(CookDeliverProcess());

    }

    IEnumerator CookDeliverProcess()
    {
        FoodHolder foodHolder = restaurant.foodHolder;
        MoveToVector(foodHolder.GetCookPos());
        ChangeTask("Deliver food");

        while (isWalking)
        {
            yield return null;
        }

        IdleAnimation(foodHolder.GetCookFacingPos());
        //deliver the food and return 
        float total = 0;
        float current = 0;

        while(total > current)
        {
            current += Time.deltaTime;
            chargingUI.HandleCharging(current, total);
            yield return new WaitForSeconds(Time.deltaTime);
        }


        PlayerHandler.instance.restaurant.AddFoodReady(currentOrder);

        chargingUI.StartScaleAnimation();
        isCooking = false;
        currentOrder.isOpen = false;
        currentOrder = null;
        currentOrder.table.mainClient.EventClientCancel -= CancelOrder;

        chargingUI.TurnOff();

    }

    #endregion


    #region CHECK
    bool IsWorking()
    {
        if (currentOrder == null) return false;
        return currentOrder.isOpen;
    }

    bool HasFreeOrder()
    {
        return restaurant.orderList.Count > 0;
    }
    #endregion

}

