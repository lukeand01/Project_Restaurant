using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Table assignedTable;
    Animator anim;
    Vector3 chairPos;
    //have to define what kind of chair this is 

    public void SetUp(Vector3 pos, Vector3 animPos)
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("PosX", animPos.x);
        anim.SetFloat("PosY", animPos.y);
        chairPos = pos;
    }

    public Vector3 GetSittingPos()
    {
        return chairPos;
    }
    
    
}
