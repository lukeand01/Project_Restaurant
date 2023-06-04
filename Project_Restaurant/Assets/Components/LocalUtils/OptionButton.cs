using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionButton : ButtonBase
{
    PlayerGUI handler;
    string choice;

    public void SetUp(PlayerGUI handler, string choice)
    {
        this.handler = handler;
        this.choice = choice;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        handler.StartOption(choice);
    }



}
