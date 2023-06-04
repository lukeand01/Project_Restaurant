using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaffOptionUnit : ButtonBase
{
    //

    StaffUI handler;
    StaffType staff;
    [SerializeField] TextMeshProUGUI titleOption;

    public void SetUp(StaffUI handler, StaffType staff)
    {
        this.handler = handler;
        this.staff = staff;

        titleOption.text = staff.ToString();
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        handler.ChangeContainer(staff);
    }

    public void Control(bool choice)
    {

    }

}

