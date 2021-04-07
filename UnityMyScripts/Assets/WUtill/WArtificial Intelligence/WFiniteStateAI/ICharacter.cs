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
        /// Ű ���� �ݵ��!
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
        /// Action�� Thread �󿡼� ���ư�. ���� ui�� �ǵ帱 �� ����!!
        /// </summary>
        public abstract void Action();
        /// <summary>
        /// corutine �󿡼� ���ư� behavior
        /// </summary>
        public abstract void Schedule();



    }

}
