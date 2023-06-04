using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargingUI : MonoBehaviour
{
    //we just get the stuff and change its 
   


    Transform holder;
    public Image chargingBar;
    [HideInInspector]public bool isAnimating;

    private void Awake()
    {
        holder = transform.GetChild(0);
    }

    public void TurnOff()
    {
        if (isAnimating)
        {
            //then we wait
            StartCoroutine(TurnOffProcess());
            return;
        }
        gameObject.SetActive(false);

    }

    IEnumerator TurnOffProcess()
    {
        while (isAnimating)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }


    public void HandleCharging(float current, float total)
    {
        chargingBar.fillAmount = current / total;
    }

    

    public void StartScaleAnimation()
    {
        StartCoroutine(ScaleAnimationProcess());
    }

    IEnumerator ScaleAnimationProcess()
    {
        isAnimating = true;

        while(holder.localScale.x < 1.6f)
        {
            holder.localScale += new Vector3(1, 1, 1);
            yield return new WaitForSeconds(0.008f);
        }

        yield return new WaitForSeconds(0.2f);

        while(holder.localScale.x > 1)
        {
            holder.localScale -= new Vector3(1, 1, 1);
            yield return new WaitForSeconds(0.008f);
        }

        isAnimating = false;
    }


}
