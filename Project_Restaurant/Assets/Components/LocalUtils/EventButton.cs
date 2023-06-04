using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventButton : ButtonBase
{
    [SerializeField] UnityEvent unityEvent;

    public override void OnPointerClick(PointerEventData eventData)
    {
        //call function
        unityEvent.Invoke();
    }

}
