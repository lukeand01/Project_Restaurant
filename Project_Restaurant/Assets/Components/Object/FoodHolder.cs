using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHolder : MonoBehaviour
{
    //food holder have space. they cannot have more fellas being prepared or ready than the limit.



    public Vector3 GetCookPos() => transform.position + Vector3.up;

    public Vector2 GetCookFacingPos() => Vector2.down;

    public Vector3 GetWaiterPos() => transform.position + Vector3.down;

    public Vector2 GetWaiterFacingPos() => Vector2.up;


    //


}
