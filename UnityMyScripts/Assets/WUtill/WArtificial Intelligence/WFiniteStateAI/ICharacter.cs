using Manager;
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
        protected Queue<Action> lowQue;
        protected Queue<Action> middleQue;
        /// <summary>
        /// 키 설정 반드시!
        /// </summary>
        public string key="default";


        public virtual void Start()
        {
            lowQue = new Queue<Action>();
            middleQue = new Queue<Action>();
             CharacterManager.GET.AddUnit(key, this, true);
            _low = lowEmotion.Normal;
            _currentState = State.Idle;
        }
        /// <summary>
        /// Action은 Thread 상에서 돌아감. 따라서 ui를 건드릴 수 없음!!
        /// </summary>
        public abstract void Action();
        /// <summary>
        /// corutine 상에서 돌아갈 behavior
        /// </summary>
        public abstract void Schedule();



    }

}
