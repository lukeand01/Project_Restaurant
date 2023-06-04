using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StaffUnit : ButtonBase
{
    //where do i hold information about it?
    //how do i get different stuff here?

    StaffClass staff;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI typeText;


    public void SetUp(StaffClass staff)
    {
        //
        this.staff = staff;
        nameText.text = staff.name;
        typeText.text = staff.GetStaffType().ToString();

    }

    void UpdateUI()
    {

    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        //we put this in the descriptor.
        //we can upgrade him, we can see info and we can fire him.



    }

}
