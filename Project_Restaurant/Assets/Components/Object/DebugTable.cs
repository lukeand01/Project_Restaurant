using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTable : MonoBehaviour
{
    public int startingChairs;
    Table table;
    private void Start()
    {
        table = GetComponent<Table>();
        for (int i = 0; i < startingChairs; i++)
        {
            table.AddChair();
        }
    }



}
