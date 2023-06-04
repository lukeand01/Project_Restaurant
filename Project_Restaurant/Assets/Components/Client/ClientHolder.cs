using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ClientHolder : ScriptableObject
{
    //each type has graphic
    //each has a maxbuy and minbuy, for chosing the thing. clients cannot go to a restaurant where it cannot buy the min plate.
    //

    //reputation allows you to certain stuff.
    //reputation dicates the min and max of the client.
    //teh values of the 

    //we set here the values.

    //process
    //gamehandler decides a client should spawn
    //we check what we can have.
    //we try to get someone from this type of clietn who can buy this food.
    //if there is no one we can in this range then we tell the game to keep rolling till someone



    public List<ClientHolderClass> clientHolderList = new();


    //for now we will simply spawn a base one that has its money changed only.
    //i mean not even that.

    public Client GetClient(ClientType type, int minRecipe)
    {
        



        return null;
    }


}

//how this works?
//its all about chance. the values are added and a percetage is made.

[System.Serializable]
public class ClientHolderClass
{
    public string clientClassName;
    public Client clientPrefab;
    public ClientType clientType;
    public int reputationMin; //the min reputation for this character to be thought of.

    
}

public enum ClientType
{
    Civilian,
    Magical,
    Gangster,
    Alien
}