using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehaviorMove : TouchBehavior
{
    Vector3 touchStart;
    bool moveBlocked;
    public TouchBehaviorMove()
    {

    }


    public override void ReceiveTouch(Touch[] touches)
    {
        if (touches.Length >= 2)
        {
            moveBlocked = true;
        }

        if (touches.Length != 1) return;

        if (moveBlocked)
        {
            if (touches[0].phase == TouchPhase.Began) moveBlocked = false;
            else return;    
        }


        //only if it moves.
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Vector2.Distance(touches[0].position, touchStart) < 0.3f)
        {
            Debug.Log("shouldnt move");
            return;
        }
      
        if (Input.GetMouseButton(0))
        {
            Vector3 dir = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += dir;
        }
    }
}
