using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WThread;

namespace FiniteState
{
    public abstract class ICharacter: MonoBehaviour
    {
        public lowEmotion _low;
        public State _currentState;
        Queue<Action> lowQue;
        Queue<Action> middleQue;
      
        public void Awake()
        {
            lowQue = new Queue<Action>();
            middleQue = new Queue<Action>();
            _low = lowEmotion.Normal;
            _currentState = State.Idle;

        }

        /// <summary>
        /// Action은 Thread 상에서 돌아감. 따라서 ui를 건드릴 수 없음!!
        /// </summary>
        public void Action()
        {

        }

     



    }

}
