using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W_AI
{
    //ai���� ����� �������� ����

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
        /// ai�� ����Ȼ �� ����� ��ũ��Ʈ �Դϴ�.
        /// </summary>
        /// <returns></returns>
        public virtual void Begin(AIValue value)
        {

                _value = value;
                isUpdate = true;


        }
        /// <summary>
        /// AI�� ������Ʈ �� �� ����� ��ũ��Ʈ �Դϴ�.
        /// </summary>
        /// <returns></returns>
        public abstract bool Update();

        /// <summary>
        /// AI�� ���� �� ����Ǵ� ��ũ��Ʈ �Դϴ�. 
        /// </summary>
        /// <returns></returns>
        public virtual List<W_AINodeBase> End()
        {
            if (condition == true)//����� true�� trueActionNodes �� �۾� ť�� ����
            {
           //    Debug.Log("��ǥ ����");
           
            
                return trueActionNodes;
            }
            else//����� false�� falseActionNodes�� �۾� ť�� ����
            {
               // Debug.Log("��ǥ ����");
                return falseActionNodes;
            }
         
        }
        /// <summary>
        /// ai�� �ٽ� ������ �� ����Ǵ� ��ũ��Ʈ �Դϴ�.
        /// </summary>
        public virtual void Stop()
        {

        }
        /// <summary>
        ///ai�� �ٽ� �������� �� ����Ǵ� ��ũ��Ʈ �Դϴ�. 
        /// </summary>
        public virtual void ReStart()
        {

        } 
        /// <summary>
        /// ��� �ʱ�ȭ �Լ� �Դϴ�.
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


