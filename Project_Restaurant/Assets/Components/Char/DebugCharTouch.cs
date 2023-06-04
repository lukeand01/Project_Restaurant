using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCharTouch : MonoBehaviour
{
    [Separator("DEBUG")]
    [SerializeField] bool colorWhenTouched;

    [Separator("MOVEMENT")]
    [SerializeField] Transform target;

    CharBase myBase;
    SpriteRenderer rend;

    private void Awake()
    {
        myBase = GetComponent<CharBase>();
        myBase.EventTouch += TouchDebug;
        myBase.EventSetUp += SetUpComplete;
        rend = myBase.gameObject.GetComponent<SpriteRenderer>();
    }

    void SetUpComplete()
    {
        if (target != null) myBase.MoveToTransform(target, Vector2.zero);
    }


    #region TOUCH
    void TouchDebug(int count)
    {
        if (count == 1) rend.color = Color.green;
        if (count == 2) rend.color = Color.blue;

        StopAllCoroutines();
        StartCoroutine(TouchProcess());
    }

    IEnumerator TouchProcess()
    {
        yield return new WaitForSeconds(0.2f);
        rend.color = Color.white;
    }
    #endregion

    #region MOVEMENT



    #endregion

}
