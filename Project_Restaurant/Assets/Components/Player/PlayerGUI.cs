using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{

    private void Awake()
    {
        InitialOptions();
    }


    [Separator("Options Bar")]
    [SerializeField] GameObject optionHolder;
    [SerializeField] Transform optionContainer;
    [SerializeField] OptionButton optionTemplate;
    #region OPTIONS BAR
    public event Action EventCloseOption;
    public void OnCloseOption() => EventCloseOption?.Invoke();
    void InitialOptions()
    {
        CreateOptionButton("Storage");
        CreateOptionButton("Staff");
    }

    public void CreateOptionButton(string choice)
    {
        OptionButton newObject = Instantiate(optionTemplate, optionContainer.position, Quaternion.identity);
        newObject.transform.parent = optionContainer;
        newObject.SetUp(this, choice);
    }

    public void StartOption(string choice)
    {
        //close any other fella.
        OnCloseOption();
        if (choice == "Storage")
        {
            UIHolder.instance.storage.OpenUI();
        }
        if(choice == "Staff")
        {
            UIHolder.instance.staff.OpenUI();
        }
    }
    

    

    #endregion

    [Separator("Gold")]
    [SerializeField] TextMeshProUGUI goldText;
    public void UpdateGold(int value)
    {
        goldText.text = "Gold: " + value.ToString();
    }

    [Separator("Reputation")]
    [SerializeField] TextMeshProUGUI reputationText;
    public void UpdateReputation(int value)
    {
        reputationText.text = "Reputation: " + value.ToString();
    }

    [Separator("Chest")]
    [SerializeField] TextMeshProUGUI chestText;
    public void UpdateChest(int value)
    {
        chestText.text = "Chest: " + value.ToString();
    }


    [Separator("Key")]
    [SerializeField] TextMeshProUGUI keyText;
    public void UpdateKey(int value)
    {
        keyText.text = "Keys: " + value.ToString();
    }

    [Separator("Storage")]
    [SerializeField] TextMeshProUGUI storageText;
    public void UpdateStorage(int current, int total)
    {
        storageText.text = $"{current} / {total}";
    }

    [Separator("Stars")]
    [SerializeField] TextMeshProUGUI starsText;
    [SerializeField] TextMeshProUGUI starProgressText;
    [SerializeField] Image starBar;
    public void UpdateStars(int star, float current, float total)
    {

    }
    public void StarGained()
    {

    }
}
