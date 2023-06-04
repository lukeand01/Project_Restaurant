using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStation : MonoBehaviour
{
    //these are the food station.
    public ProcedureType[] procedure;
    public Vector3 facingDir;


    public bool HasUtility(ProcedureType type)
    {
        foreach (var item in procedure)
        {
            if (item == type) return true;
        }
        return false;
    }

    public Vector3 GetPos()
    {
        return transform.position + facingDir;
    }
    public Vector3 GetFacingPos()
    {
        return facingDir * -1;
    }

}
