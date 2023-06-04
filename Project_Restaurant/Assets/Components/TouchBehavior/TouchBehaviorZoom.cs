using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehaviorZoom : TouchBehavior
{
    float zoomMin;
    float zoomMax;
    
    public TouchBehaviorZoom()
    {
        zoomMin = 1;
        zoomMax = 8;
    }


    public override void ReceiveTouch(Touch[] touches)
    {
        if (touches.Length != 2) return;

        Touch touchZero = Input.touches[0];
        Touch touchOne = Input.touches[1];

        Vector2 touchZeroPrevious = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevious = touchOne.position - touchOne.deltaPosition;

        float prevMagntude = (touchZeroPrevious - touchOnePrevious).magnitude;
        float currentMagntude = (touchZero.position - touchOne.position).magnitude;

        float difference = currentMagntude - prevMagntude;

        Zoom(difference * 0.003f);
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, 1, 8);
    }

}
