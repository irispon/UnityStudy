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
       public Action action;
       public Action onUIThread;
        private WThreadManager manager = WThreadManager.GET;
       Task child = null;
       bool isWork=true;
       EventWaitHandle childWait = new EventWaitHandle(true, EventResetMode.ManualReset);
       EventWaitHandle mainWait = new EventWaitHandle(true, EventResetMode.ManualReset);
      

       public virtual void Work()
        {
            childWait.Reset();
            childWait.WaitOne();
            while (isWork)
            {
                childWait.Reset();
                if (action != null)
                    action();


                mainWait.Set();
                childWait.WaitOne();
            }
            child.Dispose();
        }

        public void Awake()
        {
            child = new Task(Work);
            child.Start();
        }
        private void Update()
        {
            mainWait.WaitOne();
            mainWait.Reset();
            if (onUIThread != null)
                onUIThread();
            childWait.Set();
        }
        public void OnDestroy()
        {
            childWait.Set();
            isWork = false;
          
        }
    }
}

