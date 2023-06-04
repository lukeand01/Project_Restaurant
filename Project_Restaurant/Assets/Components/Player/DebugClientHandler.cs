using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugClientHandler : MonoBehaviour
{
    //we use this to spawn clients.
    [SerializeField] Client clientTemplate;
    [SerializeField] Transform startingPos;
    [SerializeField] bool spawnAtStart;
    private void Start()
    {
       if(spawnAtStart) DebugSpawnClient();
    }

    [ContextMenu("DEBUGSPAWNCLIENT")]
    public void DebugSpawnClient()
    {
        Client newObject = Instantiate(clientTemplate, startingPos.position, Quaternion.identity);
        newObject.name = Guid.NewGuid().ToString();
        newObject.SetUpClient();
    }


}
