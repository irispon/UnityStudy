using FiniteState;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCahracter : ICharacter
{
    int act = 1;
    int schedule = 1;
    public Vector3 vector;
    public Queue<System.Action> ThreadAction = new Queue<System.Action>();
    int tick=0;
    /// <summary>
    /// Thread 상에서 돌아가는 Action이다.
    /// </summary>
    public override void Action()
    {

        System.Random random = new System.Random(System.DateTime.Now.Second);
        act++;
        vector = new Vector3(random.Next(-2,2), random.Next(-2,2), random.Next(-2,2));
        middleQue.Enqueue(() => {

            transform.position = Vector3.MoveTowards(transform.position, transform.position + vector, 0.002f);
        });
        if (ThreadAction.Count > 0)
        {
            for(int i = 0; i < lowQue.Count; i++)
            {
                ThreadAction.Dequeue().Invoke();
            }
     
        }
    }

    /// <summary>
    /// 코루틴에서 돌아갈 작업들(ui 작업)을 돌린다. middleQue와 lowQue를 통해 따로 작업을 넣어줄 수 있다.
    /// </summary>
    public override void Schedule()
    {
        tick++;

        
        middleQue.Dequeue().Invoke();
        // Debug.Log("schedule  ***** "+name+"   :"+schedule);
        if (tick == 100)
        {
            for (int i = 0; i < lowQue.Count; i++)
                lowQue.Dequeue().Invoke();

            tick = 0;
        }


        // Debug.Log("Vector"+ vector+"move  "+ transform.position);
        schedule++;



    }
    /// <summary>
    /// 스레드에서 돌릴 작업들을 넣어둔다.
    /// </summary>
    /// <param name="action"></param>
    public virtual void AddThreadAction(Action action)
    {
        ThreadAction.Enqueue(action);
    }

}

