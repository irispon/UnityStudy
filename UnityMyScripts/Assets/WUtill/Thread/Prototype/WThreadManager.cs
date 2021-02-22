using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace prototype
{
    //타임 스레드
    //블록킹 스레드
    public class WThreadManager  : MonoBehaviour
    {
        private Queue<Action> longTasks;
        private Queue<Action> shortTasks;
        private Task[] threads;
        private bool enableThread;

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }
        void Init()
        {
            longTasks = new Queue<Action>();
            shortTasks = new Queue<Action>();
            if (Environment.ProcessorCount > 3)
            {
                enableThread = true;
                threads = new Task[(int)(Environment.ProcessorCount*0.75)];
                Debug.Log((int)(Environment.ProcessorCount * 0.75));
            }
            else
            {
                enableThread = false;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


    }

}

