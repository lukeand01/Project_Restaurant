using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffClass
{

    

    public string name;
    public int salary;
    public int level;
    public int maxLevel;
    public int tier;

    //salary is based on what?
    //salary is based on level, staff
    
    public virtual void SetUpBase(int tier)
    {
        name = Utils.GetName();
        this.tier = tier;
        level = 0;
        maxLevel = tier * 5;        


    }


    public StaffType GetStaffType()
    {
        if (GetCook() != null) return StaffType.Cook;
        if (GetWaiter() != null) return StaffType.Waiter;
        if (GetClient() != null) return StaffType.Client;
        return StaffType.Cook;
    }
    //what can influence this thing?
    //reputation, modifier, luck, 
    public virtual void SetUpInfluence()
    {

    }



    public virtual CookClass GetCook() => null;
    public virtual WaiterClass GetWaiter() => null;

    public virtual ClientClass GetClient() => null;

}
[System.Serializable]
public class CookClass : StaffClass
{
    public float speed;
    public int cooking;
    public int knowledge;

    public CookClass()
    {

    }

    public override CookClass GetCook() => this;
    
}

[System.Serializable]
public class WaiterClass : StaffClass
{
    //this geneated in hiring.

    public override WaiterClass GetWaiter() => this;
    

}

public class ClientClass: StaffClass
{

    //what are these stats?
    //max money.



    public override ClientClass GetClient() => this;
}