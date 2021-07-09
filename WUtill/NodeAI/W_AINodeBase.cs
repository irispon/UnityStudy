using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W_AI
{
    //ai에서 사용할 정보들을 저장

    public abstract class W_AINodeBase : ScriptableObject
    {
        public AIValue _value = null;
        public bool isUpdate  = false;
        public List<W_AINodeBase> falseActionNodes= new List<W_AINodeBase>();
        public List<W_AINodeBase> trueActionNodes = new List<W_AINodeBase>();
        protected bool condition;
        
        public bool isClone=false;

        public int code;
        /// <summary>
        /// ai가 실행횔 때 사용할 스크립트 입니다.
        /// </summary>
        /// <returns></returns>
        public virtual void Begin(AIValue value)
        {

                _value = value;
                isUpdate = true;


        }
        /// <summary>
        /// AI가 업데이트 될 때 사용할 스크립트 입니다.
        /// </summary>
        /// <returns></returns>
        public abstract bool Update();

        /// <summary>
        /// AI가 끝날 때 실행되는 스크립트 입니다. 
        /// </summary>
        /// <returns></returns>
        public virtual List<W_AINodeBase> End()
        {
            if (condition == true)//출력이 true면 trueActionNodes 을 작업 큐에 넣음
            {
           //    Debug.Log("목표 도착");
           
            
                return trueActionNodes;
            }
            else//출력이 false면 falseActionNodes를 작업 큐에 넣음
            {
               // Debug.Log("목표 실패");
                return falseActionNodes;
            }
         
        }
        /// <summary>
        /// ai가 다시 시작할 때 실행되는 스크립트 입니다.
        /// </summary>
        public virtual void Stop()
        {

        }
        /// <summary>
        ///ai가 다시 시작했을 때 실행되는 스크립트 입니다. 
        /// </summary>
        public virtual void ReStart()
        {

        } 
        /// <summary>
        /// 노드 초기화 함수 입니다.
        /// </summary>
        public virtual void Init()
        {
            isUpdate = false;
        }
        public virtual W_AINodeBase Clone()
        {

               W_AINodeBase node = Instantiate(this);
                node.isClone = true;
                node.code = code;
                return node;

        }

    }
}


