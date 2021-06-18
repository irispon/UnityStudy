using System;
using System.Collections.Generic;
using UnityEngine;

namespace W_AI
{
    /// <summary>
    /// AI Value는 상황에 맞춰서 만들면 됨.( 노드에 넘겨주고 공유할 정보를 기술함.)
    /// </summary>
    [Serializable]
    public class AIValue
    {
       
        public Vector3 targetGround = Vector3.negativeInfinity;
        public GameObject targetObject;

    }

    public class W_NodeAI : MonoBehaviour
    {
        [Header("[AI Node Asset]")]
        public W_AITree _tree; //이 부분은 추후에 ai변경이 있으면 clone을 통해 복사해서 쓰고. ai변경이 내부적으로 없으면 그냥 그대로 사용하면 될거 같다.

        public Queue<W_AINodeBase> _emergencyQue = new Queue<W_AINodeBase>();//긴급 사용 큐
        public Queue<W_AINodeBase> _jobQue = new Queue<W_AINodeBase>();//기본 큐
        public Queue<W_AINodeBase> _tmpQue;//임시 저장용 큐  일단 많이 호출되는 부분이라 update 밖으로 뺐음


        public AIValue value;
        [HideInInspector]
        public float calcDistance = 0;
        [HideInInspector]
        //     private int count=0;
        public bool isContinue { get; private set; } = true;

        private W_AINodeBase tmpNode;//일단 많이 호출되는 부분이라 update 밖으로 뺐음
        private List<W_AINodeBase> tmpNodes;//일단 많이 호출되는 부분이라 update 밖으로 뺐음

        //      private bool isEmergency = false;

        private void Awake()
        {
            value = new AIValue();
            _emergencyQue = new Queue<W_AINodeBase>();

        }
        private void Start()
        {
            if (_tree != null)
            {
                SetAI(_tree);
            }


        }

        void Update()
        {
            if (isContinue == true)
            {
                if (_emergencyQue.Count > 0)//긴급 큐가 존재하면. 긴급 큐 부터 이용
                {

                    _tmpQue = _emergencyQue;
                }
                else//없을 때는 일반 큐 이용. 우선 순위
                {

                    _tmpQue = _jobQue;
                }

                int tmpCount = _tmpQue.Count;
                for (int i = 0; i < tmpCount; i++)
                {
                    tmpNode = _tmpQue.Dequeue();
                    tmpNode.Begin(value);//이 부분을 begin 초기화 용으로만 쓸지 계속 실행해야할지 고민좀 필요할 듯.
                    if (tmpNode.Update() == false)//update가 false(작업 완료) 끝났으면 end 호출
                    {
                        tmpNodes = tmpNode.End(); //end는 노드 List 반환
                        for (int j = 0; j < tmpNodes.Count; j++)
                        {   
                       
                            _tmpQue.Enqueue(tmpNodes[j]);//이 받아온 노드들을 해당 큐에 넣음(긴급 큐에 관련된 거면 긴급 큐에, 긴급 큐가 끝나면 작업 큐에... 이 부분은 수 정해야할거 같음.
                        }
                    }
                    else
                    {
                        _tmpQue.Enqueue(tmpNode);//update가 계속해서 실행중(ture)이면 다시 작업 큐에 넣음 
                    }
                }
            }






        }

        public void SetAI(W_AITree tree)//나중에 AI Dictionary를 만들어서 ai를 관리하면 좋을거 같은데... 일단은 주석으로 남겨둠.
        {
            _jobQue.Clear();
            _tree = tree.Clone();
            _jobQue.Enqueue(_tree.nodes[0]);


            //            isContinue = false;

        }
        public void SetEmergency(W_AITree tree)
        {

            _emergencyQue.Clear();
            _emergencyQue.Enqueue(tree.Clone().nodes[0]);
        }



        public void Stop()
        {
            isContinue = false;
            _jobQue.Clear();
            _emergencyQue.Clear();
        }
        public void Continue()
        {
            isContinue = true;
            _jobQue.Clear();
            if(_tree!=null)
            _jobQue.Enqueue(_tree.nodes[0]);
        }

    }
}

