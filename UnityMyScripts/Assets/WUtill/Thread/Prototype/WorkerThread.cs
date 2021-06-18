using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace WThread
{
    public class WorkerThread : MonoBehaviour
    {
        /// <summary>
        /// 스레드 작업
        /// </summary>
       public Action thread;
       public Action update;
       public Queue<WThreadData> datas;
       public Action schedule;//이건 삭제 해야할 수도 있음.
        private WThreadManager manager;
       Task child = null;
       bool isWork=true;
       EventWaitHandle childWait = new EventWaitHandle(true, EventResetMode.ManualReset);
       EventWaitHandle mainWait = new EventWaitHandle(true, EventResetMode.ManualReset);
        public int number;
      
       public virtual void Work()
        {
            childWait.Reset();
            childWait.WaitOne();
            while (isWork)
            {
                childWait.Reset();
                thread?.Invoke();
                if (datas != null) ;
                   
                mainWait.Set();
                childWait.WaitOne();
            }
            child.Dispose();
        }

        public void Awake()
        {
            manager = WThreadManager.GET;
    //        manager.EnQueueThread(this);
            child = new Task(Work);
            child.Start();
            
        }
        private void Update()
        {
            mainWait.WaitOne();
            mainWait.Reset();
            update?.Invoke();
            childWait.Set();
        }
        public void OnDestroy()
        {
            childWait.Set();
            isWork = false;
          
        }
    }
}

