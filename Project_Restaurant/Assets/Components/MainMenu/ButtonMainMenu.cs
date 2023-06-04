using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMainMenu : ButtonBase
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ready custom");
    }
}
