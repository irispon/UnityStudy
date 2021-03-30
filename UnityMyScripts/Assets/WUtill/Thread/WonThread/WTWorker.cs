using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace WThread
{
    public enum WTWorkerMode
    {
        //순환식, 틱 형태
        Cycle,Tick
    }
    public class WTWorker
    {
        Task child;
        bool isWork=true;
        bool isStop;
        EventWaitHandle childWait = new EventWaitHandle(true, EventResetMode.ManualReset);
        Action thread;
        int delay;
        
        Queue<Action> actions;

        private WTWorkerMode mode = WTWorkerMode.Cycle;
        public virtual void Work()
        {

            childWait.Reset();
            childWait.WaitOne();
            switch (mode)
            {
                case WTWorkerMode.Cycle:
                    while (isWork)
                    {
                        //  Debug.Log("test");
                        if (actions.Count > 0)
                            thread = actions.Dequeue();
                        thread?.Invoke();

                        Thread.Sleep(delay);
                        if (isStop == true)
                        {
                            childWait.Reset();
                            childWait.WaitOne();
                        }
                    }
                    break;
                case WTWorkerMode.Tick:
                    while (isWork)
                    {
                        //  Debug.Log("test");
                        if (actions.Count > 0)
                            thread = actions.Dequeue();
                             thread?.Invoke();

                       
                            childWait.Reset();
                            childWait.WaitOne();
                    }
                    break;
            }

            child.Dispose();
        }

        public WTWorker(Action act = null,int delay =0, WTWorkerMode mode = WTWorkerMode.Cycle)
        {
            actions = new Queue<Action>();
            this.delay = delay;
            this.mode = mode;

            if (act != null)
                actions.Enqueue(act);
            //        manager.EnQueueThread(this);
            child = new Task(Work);
            child.Start();

        }

        public void ChangeAction(Action action)
        {
     
            if(actions.Count<=0)
            {
                actions.Enqueue(action);
            }
            else
            {
                if (!actions.Peek().Equals(action))
                {
                    actions.Enqueue(action);
                }
                else
                {
                    Debug.Log("이미 존재하는 액션입니다.");
                }
                 
            }

        }
        public void Stop()
        {
            isStop = true;
        }
        public void Start()
        {
            isStop = false;
            childWait.Set();
        }
        public void Dispose()
        {
            Start();
            isWork = false;
            child.Dispose();
        }
    }

}
