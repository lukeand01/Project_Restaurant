using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchable 
{
    public void ReceiveTouch(int touches);
    public void ReceiveHoldTouch();
    //certains char might have different reactions to holding, double touching.


}
