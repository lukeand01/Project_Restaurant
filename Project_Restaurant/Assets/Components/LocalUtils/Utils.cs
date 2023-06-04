using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    
    public static string GetName()
    {
        string[] possibleNames = { "Thomas", "Clara", "Ivania", "Pollo", "Lola", "Ahri", "Leticia", "Haru", "Pedro", "Joao", "Peter", "Lucas", "Daniel" };


        return possibleNames[Random.Range(0, possibleNames.Length)];
    }


}
