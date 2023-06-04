using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehaviorChar : TouchBehavior
{
    float current;
    float total;
    GameObject charObject;

    public TouchBehaviorChar()
    {
        total = 0.18f;
    }

    public override void ReceiveTouch(Touch[] touches)
    {
        //you press. if there is something we start calculation fast. when we take the finger out it counts slower.   

        if (touches.Length != 1) return;


        if (touches[0].tapCount == 1)
        {
            ITouchable touch = GetTouch(touches[0]);

            if (touch != null)
            {
                touch.ReceiveTouch(1);
            }
            else
            {
                //then we tell staff description ui to stop.
                UIHolder.instance.staffDescription.Cancel();
            }

        }else if (touches[0].tapCount == 2)
        {
            ITouchable touch = GetTouch(touches[0]);

            if (touch == null) touch.ReceiveTouch(2);
        }

        

    }

    void FirstScript(Touch[] touches)
    {
        if (touches.Length != 1)
        {
            if (charObject != null)
            {
                if (current >= total)
                {
                    charObject.GetComponent<ITouchable>().ReceiveTouch(1);
                    ResetChar();
                    return;
                }
                else
                {
                    current += Time.deltaTime;
                }
            }

            return;
        }

        if (charObject != null)
        {
            //if you 

            if (touches[0].phase == TouchPhase.Began)
            {
                Debug.Log("tried a new fella");
                if (charObject == GetObject(touches[0]))
                {
                    //then tell it 
                    Debug.Log("same fella");
                    charObject.GetComponent<ITouchable>().ReceiveTouch(2);
                    ResetChar();
                    return;
                }
            }
            else if (touches[0].phase != TouchPhase.Began)
            {
                if (current >= total)
                {
                    charObject.GetComponent<ITouchable>().ReceiveTouch(1);
                    ResetChar();
                    return;
                }
                else
                {
                    current += Time.deltaTime * 1.3f;
                }

            }

        }

        if (charObject != null) return;
        //if the button 
        charObject = GetObject(touches[0]);
    }

    GameObject GetObject(Touch firstTouch)
    {
        Touch touch = firstTouch;
        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

        RaycastHit2D hit = Physics2D.Raycast(pos, Camera.main.transform.position - pos, 0, 001);

        if (hit.collider == null) return null;

        ITouchable touchable = hit.collider.GetComponent<ITouchable>();

        if (touchable == null) return null;

        return hit.collider.gameObject;
    }

    ITouchable GetTouch(Touch firstTouch)
    {
        Touch touch = firstTouch;
        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

        RaycastHit2D hit = Physics2D.Raycast(pos, Camera.main.transform.position - pos, 0, 001);

        if (hit.collider == null) return null;

        ITouchable touchable = hit.collider.GetComponent<ITouchable>();

        return touchable;
    }

    void ResetChar()
    {
        charObject = null;
        current = 0;
    }
}
