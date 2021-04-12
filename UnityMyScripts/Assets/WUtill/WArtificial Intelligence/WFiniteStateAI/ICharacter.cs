using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WThread;

namespace FiniteState
{
    public abstract class ICharacter : MonoBehaviour
    {
        public lowEmotion _low;
        public State _currentState;
        /// <summary>
        /// 중요하지 않은 작업
        /// </summary>
        protected Queue<Action> lowQue = new Queue<Action>();
        /// <summary>
        /// 중요한 작업
        /// </summary>
        protected Queue<Action> middleQue = new Queue<Action>();
        /// <summary>
        /// 스레드 상에서 돌릴 작업.
        /// </summary>
        protected Queue<Action> ThreadAction = new Queue<Action>();
        /// <summary>
        /// lowQue에서 몇 틱 이후에 발동할 지 정하는 변수
        /// </summary>
        public int tick = 0;
        /// <summary>
        /// 해당 캐릭터가 어디에서 관리를 받는지에 대한 내용. 
        /// </summary>
        public string key = "default";
        public Faction faction;

        public virtual void Start()
        {
            lowQue = new Queue<Action>();
            middleQue = new Queue<Action>();

            CharacterManager.GET.AddUnit(this, true);
            _low = lowEmotion.Normal;
            _currentState = State.Idle;
        }
        /// <summary>
        /// Action은 Thread 상에서 돌아감. 따라서 ui를 건드릴 수 없음!!
        /// </summary>
        public virtual void Action()
        {   
            
            if (ThreadAction.Count > 0)
            {
                for (int i = 0; i < lowQue.Count; i++)
                {
                    ThreadAction.Dequeue().Invoke();
                }
               
            }
        }
        /// <summary>
        /// corutine 상에서 돌아갈 behavior
        /// </summary>
        public virtual void Schedule()
        {
            tick++;
            if(middleQue.Count>0)
            middleQue.Dequeue().Invoke();
            if (tick == 100)
            {
                for (int i = 0; i < lowQue.Count; i++)
                    lowQue.Dequeue().Invoke();

                tick = 0;
            }
        }

        public virtual void AddThreadAction(Action action)
        {
            ThreadAction.Enqueue(action);
        }

        private void OnDisable()
        {
            CharacterManager.GET.RemoveUnit(this);
        }
    }

}
