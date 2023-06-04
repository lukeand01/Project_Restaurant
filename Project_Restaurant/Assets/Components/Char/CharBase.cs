using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharBase : MonoBehaviour, ITouchable
{
    //this is what every char has.

    //so we touch stuff and check with raycast to see if it hit something

    public StaffClass staff;

    [Separator("CHARBASE")]
    SpriteRenderer rend;

    [Separator("UI")]
    public ChargingUI chargingUI;
    //emotions.

    public string currentTask { get; protected set; }
    public void ChangeTask(string newTask)
    {
        currentTask = newTask;
    }
    


    #region EVENTS
    public event Action<int> EventTouch;
    public void OnTouch(int touchCount) => EventTouch?.Invoke(touchCount);

    public event Action EventSetUp;
    public void OnSetUp() => EventSetUp?.Invoke();

    //we call this whenever we arrived in the place and assign when we start moving.
    public event Action EventArrived;
    public void OnArrived() => EventArrived?.Invoke();

    #endregion

    protected MyPathfind pathfind;

    //
    //because we will create this somwehere and drag to this fella.
    private void Awake()
    {
        
    }



    public virtual void SetUp(StaffClass staff)
    {
        //i dont set up it here but somewhere else.
        this.staff = staff;
        pathfind = MyPathfind.instance;
        rend = GetComponent<SpriteRenderer>();
        SetupAnimation();
        OnSetUp();
    }

    public void ReceiveTouch(int touches)
    {
        //this receives how many it was touched. the individual decides what to do with them.
        if (touches == 1) GetDescription();
        if (touches == 2) Debug.Log("two touches");

        OnTouch(touches);
    }

    public void ReceiveHoldTouch()
    {
        //i dont know if i will use it.
    }

    public virtual void GetDescription()
    {
        //

    }



    #region MOVEMENT
    //

    List<MyNode> currentPath = new();
    bool stopped;
    

    public void MoveToTransform(Transform target, Vector2 idleAnimation)
    {
        if(pathfind == null)
        {
            Debug.Log("there was no pathfind");
        }
        List<MyNode> pathList = pathfind.GetPathThroughVector(transform.position, target.position);
        currentPath = pathList;
        StartCoroutine(MoveProcess(currentPath));
    }

    public void MoveToVector(Vector3 target)
    {

        List<MyNode> pathlist = pathfind.GetPathThroughVector(transform.position, target);
        currentPath = pathlist;
        StartCoroutine(MoveProcess(pathlist));
    }


    public void StopMovement()
    {
        //need to delete every path already walked.
        IdleAnimation(Vector2.down);
        StopAllCoroutines();
    }

    public bool isWalking;
    IEnumerator MoveProcess(List<MyNode> pathList)
    {
        isWalking = true;
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < pathList.Count; i++)
        {
            //we calculate the vector and tell teh animator what to do.
            MoveAnimation(GetDir(pathList[i].transform.position));


            while(Time.deltaTime > 0.1)
            {
                yield return null;
            }

            while (transform.position != pathList[i].transform.position)
            {
                // Calculate the step to move based on the moveSpeed and deltaTime
                float step = 3.3f * Time.deltaTime;

                // Move the object towards the target position using Lerp
                //transform.position = Vector3.Lerp(transform.position, pathList[i].transform.position, step);
                transform.position = Vector3.MoveTowards(transform.position, pathList[i].transform.position, step);
                yield return null;
            }
        }

        isWalking = false;
        OnArrived();
    }


    Vector2 GetDir(Vector2 target)
    {
        return (target - new Vector2(transform.position.x, transform.position.y)).normalized;
    }

    #endregion


    #region ANIMATION
    Animator anim;
    void SetupAnimation()
    {
        anim = GetComponent<Animator>();
    }

    public void IdleAnimation(Vector2 dir)
    {
        anim.SetBool("IsMoving", false);
        anim.SetFloat("PosX", dir.x);
        anim.SetFloat("PosY", dir.y);
    }
    void MoveAnimation(Vector2 dir)
    {
        anim.SetBool("IsMoving", true);
        anim.SetFloat("PosX", dir.x);
        anim.SetFloat("PosY", dir.y);
    }


    #endregion
}
