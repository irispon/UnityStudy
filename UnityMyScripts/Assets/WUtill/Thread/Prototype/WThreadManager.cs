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
    //타임 스레드
    //블록킹 스레드
    public class WThreadManager  : MonoBehaviour
    {
        private Queue<Action> longTasks;
        private Queue<Action> shortTasks;
        public static Queue<WorkerThread> threadQue;
        public static WThreadManager GET { get ; private set; }
        // Start is called before the first frame update
        private void Awake()
        {

            if (GET == null)
                GET = this;
            else
                Destroy(this);

            threadQue = new Queue<WorkerThread>();
        }
        public void EnQueThread(WorkerThread thread)
        {
            thread.action = null;
            thread.onUIThread = null;
            threadQue.Enqueue(thread);

        }
        public WorkerThread DeQueThread(Action work =null,Action ui=null)
        {
            WorkerThread thread=null;
            if (threadQue.Count > 0)
            {
                thread = threadQue.Dequeue();
                thread.action = work;
                thread.onUIThread = ui;
            }

            return thread;
        }
        void Start()
        {
           
            Init();
        }
        void Init()
        {
            longTasks = new Queue<Action>();
            shortTasks = new Queue<Action>();
        }

        // Update is called once per frame
        void Update()
        {

        }


    }

}

