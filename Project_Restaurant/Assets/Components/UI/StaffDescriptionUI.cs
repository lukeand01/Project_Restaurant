using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaffDescriptionUI : MonoBehaviour
{
    //what this tells.
    //the rank
    //what is doing.
    //tell the ability and its cooldown.
    //the name

    //base:name, task
    //staff: ability. 
    //cook: currentOrder
    //waiter
    //client

    //if you click anywhere else we remove it.
    //if you click on the same fella then we take it out.


    GameObject holder;
    DescriptionClass description;

    [Separator("Text")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI salaryText;
    public TextMeshProUGUI currentTaskText;

    [Separator("Ability")]
    public GameObject abilityHolder;
    public Image abilityIcon;
    public Image abilityBar;
    public TextMeshProUGUI abilityText;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        //this updatees constantly the task.
        if (description == null) return;
        if (holder.activeInHierarchy)
        {
            if (description.ShouldExist())
            {
                description.UpdatePerTick(this);
            }
            else
            {
                Debug.Log("close this fella");
                holder.SetActive(false);
            }
            
        }
    }

    public void SetUp(DescriptionClass description)
    {
        description.UpdateUI(this);
        this.description = description;
        holder.SetActive(true);
    }  

    public void Cancel()
    {
        holder.SetActive(false);
    }



}

public class DescriptionClass
{
    public CharBase reference;
    public string charName;
    public StaffType charType;
    bool isClient;


    public DescriptionClass(CharBase reference, string clientName)
    {
        this.reference = reference;
        isClient = true;
        charType = reference.staff.GetStaffType();
        Debug.Log("char type " + charType);
    }
    public DescriptionClass(CharBase reference, string name, StaffType type)
    {
        this.reference = reference;
        isClient = false;
    }
   
    public void UpdateUI(StaffDescriptionUI handler)
    {
        if (charName != "") handler.nameText.text = charName;
        handler.typeText.text = charType.ToString();


        handler.abilityHolder.SetActive(false);
    }

    public void UpdatePerTick(StaffDescriptionUI handler)
    {
        //task and 
        if (reference.currentTask != "") handler.currentTaskText.text = reference.currentTask;
        else handler.currentTaskText.text = "Doing nothing";

        
        if (reference.staff != null && !isClient) handler.salaryText.text = reference.staff.salary.ToString();

    }

   public bool ShouldExist()
    {
        return reference != null;
    }
}