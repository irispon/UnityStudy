using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace WThread
{
    //타임 스레드-event invok 스레드 <타임 스레드 같은 경우는 코루틴 처리가 좋을거 같기도 함.
    //블록킹 스레드
    //뭐커 스레드.

    public class WThreadManager  : MonoBehaviour
    {
        public int WorkerNumber;
        public int BlockingNumber;
        public int TimeNumber;

        public static Queue<WorkerThread> threadQueue;
        public static Queue<Action> high;
        public static Queue<Action> low;
        public static WThreadManager GET { get ; private set; }
        // Start is called before the first frame update
        private void Awake()
        {

            if (GET == null)
                GET = this;
            else
                Destroy(this);

            threadQueue = new Queue<WorkerThread>();
        }
        public void EnQueueThread(WorkerThread thread)
        {
            thread.thread = null;
            thread.update = null;
            threadQueue.Enqueue(thread);

        }
        public WorkerThread DeQueueThread(Action work =null,Action ui=null,Action schedule=null)
        {
            WorkerThread thread=null;
            if (threadQueue.Count > 0)
            {
                thread = threadQueue.Dequeue();
                thread.thread = work;
                thread.update = ui;
                thread.schedule = schedule;
            }

            return thread;
        }
        void Start()
        {
           
            Init();
        }
        void Init()
        {
            high = new Queue<Action>();
            low = new Queue<Action>();
        }
        public void AddSchesule(Action action,bool isHigh=false)
        {
            if (isHigh)
            {
                high.Enqueue(action);
            }
            else
            {
                low.Enqueue(action);
            }
        }
        // Update is called once per frame
        void Update()
        {
            
        }


    }

}

