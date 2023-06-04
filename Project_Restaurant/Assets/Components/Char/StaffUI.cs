using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaffUI : MonoBehaviour
{
    //

    Transform holder;
    StaffType current;
    
    [SerializeField] TextMeshProUGUI hireText;
    [Separator("Containers")]
    [SerializeField] List<Transform> containerList = new();
    [SerializeField] Transform containerTemplate;

    [Separator("Options")]
    [SerializeField] Transform optionContainer;
    [SerializeField] StaffOptionUnit optionTemplate;

    [Separator("Unit")]
    [SerializeField] StaffUnit unitTemplate;

    Dictionary<StaffType, StaffOptionUnit> dictionaryStaffOption = new();
    Dictionary<StaffType, Transform> dictionaryStaffContainer = new();


    [Separator("HIRE")]
    [SerializeField] GameObject hireHolder;


    private void Awake()
    {
        holder = transform.GetChild(0);
    }

    public void OpenUI()
    {
        UIHolder.instance.player.EventCloseOption += CloseUI;
        holder.gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        UIHolder.instance.player.EventCloseOption -= CloseUI;
        holder.gameObject.SetActive(false);
    }

    #region SECTION
    public void AddStaffSection(StaffType staff)
    {
        
        StaffOptionUnit newObjectOptions = Instantiate(optionTemplate, optionContainer.position, Quaternion.identity);
        newObjectOptions.transform.parent = optionContainer;
        newObjectOptions.SetUp(this, staff);
        dictionaryStaffOption.Add(staff, newObjectOptions);

        Transform newObjectContainer = Instantiate(containerTemplate, containerTemplate.position, Quaternion.identity);
        newObjectContainer.name = staff.ToString();
        newObjectContainer.parent = holder;
        newObjectContainer.localScale = new Vector3(1, 1, 1);
        containerList.Add(newObjectContainer);
        dictionaryStaffContainer.Add(staff, newObjectContainer);

        if (dictionaryStaffOption.Count == 1)
        {
            ChangeContainer(staff);
        }
    }
    public void ChangeContainer(StaffType staff)
    {
        //we unactive it.
        dictionaryStaffContainer[current].gameObject.SetActive(false);
        dictionaryStaffOption[current].Control(false);
        //we activa
        current = staff;
        dictionaryStaffContainer[current].gameObject.SetActive(true);
        dictionaryStaffOption[current].Control(true);

    }
    public void Clear()
    {

    }
    #endregion

    #region UNIT
    public void AddStaffUnit(StaffClass staffClass)
    {
        //in the moment that we add an unit we need to check if we need also to create a section.

        if (!dictionaryStaffOption.ContainsKey(staffClass.GetStaffType()))
        {
            AddStaffSection(staffClass.GetStaffType());
        }

        StaffUnit newObject = Instantiate(unitTemplate, Vector3.zero, Quaternion.identity);
        newObject.transform.parent = dictionaryStaffContainer[staffClass.GetStaffType()];        
        newObject.SetUp(staffClass);
          
    }
    public void RemoveStaffUnit(StaffClass staffClass)
    {
        //how to find it to remove the thing?

    }
    #endregion

    #region DESCRIPTION



    #endregion

    #region HIRE 
    //we need to know which category i am currently on.
    public void Hire()
    {
        //we click the button.
        //open hire ui and give the option to open a chest.

    }



    #endregion
}
public enum StaffType
{
    Cook,
    Waiter,
    Client
}