using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public static PlayerHandler instance;

    [HideInInspector]public RestaurantHandler restaurant;
    [HideInInspector]public PlayerController controller;
    [HideInInspector] public PlayerChef chef;
    [HideInInspector] public PlayerResource resource;
    [HideInInspector] public PlayerStaff staff;
    [HideInInspector] public BuildHandler build;
    
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        restaurant = GetComponent<RestaurantHandler>();
        chef = GetComponent<PlayerChef>();
        resource = GetComponent<PlayerResource>();
        staff = GetComponent<PlayerStaff>();
        build = GetComponent<BuildHandler>();

        instance = this;
    }
}


//what to create.
    //
