using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //i want to have all teh controls behave as "lego blocks" rather than being hard coded here.



    public event Action<Touch[]> EventTouched;
    public void OnTouched(Touch[] touches) => EventTouched?.Invoke(touches);


    #region TOUCH BEHAVIOR

    TouchBehaviorMove touchMove = new();
    TouchBehaviorZoom touchZoom = new();
    TouchBehaviorChar touchChar = new();

    #endregion


    private void Awake()
    {
        
    }

    //

    private void Update()
    {
        //if there is an ui. we cannot do that.
        if (!UIHolder.instance.CanUseInput()) return;
        touchMove.ReceiveTouch(Input.touches);
        touchZoom.ReceiveTouch(Input.touches);
        touchChar.ReceiveTouch(Input.touches);

    }

    

    void ControlTouch()
    {
        TESTE();

        return;
        if (Input.touchCount <= 0) return;

        if(Input.touchCount == 1)
        {
            NormalTouch();
            
        }
        if(Input.touchCount >= 2)
        {
            ZoomTouch();
        }
        
        Debug.Log("touch quantity " + Input.touchCount);
        
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, 1, 8);
    }


    protected Plane plane;
    Vector3 touchStart;
    void TESTE()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.touchCount >= 2)
        {
            Touch touchZero = Input.touches[0];
            Touch touchOne = Input.touches[1];

            Vector2 touchZeroPrevious = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevious = touchOne.position - touchOne.deltaPosition;

            float prevMagntude = (touchZeroPrevious - touchOnePrevious).magnitude;
            float currentMagntude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagntude - prevMagntude;

            Zoom(difference * 0.01f);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 dir = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += dir;
        }
       
    }



    Vector2 original;
    [Separator("MOVE TOUCH")]
    [SerializeField] float moveSpeed = 1;

    void MoveTouch()
    {

    }
    void NormalTouch()
    {
        Touch touch = Input.touches[0];
        Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
           
        RaycastHit2D hit = Physics2D.Raycast(pos, Camera.main.transform.position - pos, 0, 001);

        if (hit.collider == null) return;

        ITouchable touchable = hit.collider.GetComponent<ITouchable>();

        if (touchable == null) return;


        

    }


    [Separator("ZOOM")]
    [SerializeField] float zoomSpeed = 2;
    [SerializeField] float minZoom = 2;
    [SerializeField] float maxZoom = 4;
    void ZoomTouch()
    {
        Debug.Log("two fingers in teh screen");

        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        



    }
}
