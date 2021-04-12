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
        /// �߿����� ���� �۾�
        /// </summary>
        protected Queue<Action> lowQue = new Queue<Action>();
        /// <summary>
        /// �߿��� �۾�
        /// </summary>
        protected Queue<Action> middleQue = new Queue<Action>();
        /// <summary>
        /// ������ �󿡼� ���� �۾�.
        /// </summary>
        protected Queue<Action> ThreadAction = new Queue<Action>();
        /// <summary>
        /// lowQue���� �� ƽ ���Ŀ� �ߵ��� �� ���ϴ� ����
        /// </summary>
        public int tick = 0;
        /// <summary>
        /// �ش� ĳ���Ͱ� ��𿡼� ������ �޴����� ���� ����. 
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
        /// Action�� Thread �󿡼� ���ư�. ���� ui�� �ǵ帱 �� ����!!
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
        /// corutine �󿡼� ���ư� behavior
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
