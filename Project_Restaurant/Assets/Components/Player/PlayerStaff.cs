using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaff : MonoBehaviour
{
    //
    public CookClass[] initialCook;
    public WaiterClass[] initialWaiter;

    //
    public List<StaffClass> staffList = new();


    private void Start()
    {
        foreach (var item in initialCook)
        {
            AddStaff(item);
        }
        foreach (var item in initialWaiter)
        {
            AddStaff(item);
        }
    }

    public void AddStaff(StaffClass staff)
    {
        staffList.Add(staff);
        UIHolder.instance.staff.AddStaffUnit(staff);
    }
    public void RemoveStaff(StaffClass staff)
    {
        staffList.Remove(staff);
        UIHolder.instance.staff.RemoveStaffUnit(staff);
    }



}
