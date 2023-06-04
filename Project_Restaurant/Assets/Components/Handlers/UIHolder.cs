using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHolder : MonoBehaviour
{
    public static UIHolder instance;

    public PlayerGUI player;
    public StorageUI storage;
    public StaffUI staff;
    public StaffDescriptionUI staffDescription;
    public BuildingUI building;

    private void Awake()
    {
        instance = this;
    }

    public bool CanUseInput()
    {
        if (storage.IsHolder()) return false;


        return true;
    }

}
