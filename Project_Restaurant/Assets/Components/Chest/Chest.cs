using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ScriptableObject
{
   //this is data of a certain chest.
   //
   //recipes are just the things here.

    //but the staff chest needs certain info
    //types of staff. types of abilities present.
    //max and min level for each category.
    //quantity of keys necessary.

}

//cook skills
//a char level dicatates how many stats can increase.
//speed, Cookingspeed, Cooking level

//waiter skills
//speed, charisma, haggling, attending level.


public enum ChestType
{
    Null,
    Staff,
    Recipe
}