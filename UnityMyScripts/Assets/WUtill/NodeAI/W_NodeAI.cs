using System;
using System.Collections.Generic;
using UnityEngine;

namespace W_AI
{
    /// <summary>
    /// AI Value�� ��Ȳ�� ���缭 ����� ��.( ��忡 �Ѱ��ְ� ������ ������ �����.)
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
        public W_AITree _tree; //�� �κ��� ���Ŀ� ai������ ������ clone�� ���� �����ؼ� ����. ai������ ���������� ������ �׳� �״�� ����ϸ� �ɰ� ����.

        public Queue<W_AINodeBase> _emergencyQue = new Queue<W_AINodeBase>();//��� ��� ť
        public Queue<W_AINodeBase> _jobQue = new Queue<W_AINodeBase>();//�⺻ ť
        public Queue<W_AINodeBase> _tmpQue;//�ӽ� ����� ť  �ϴ� ���� ȣ��Ǵ� �κ��̶� update ������ ����


        public AIValue value;
        [HideInInspector]
        public float calcDistance = 0;
        [HideInInspector]
        //     private int count=0;
        public bool isContinue { get; private set; } = true;

        private W_AINodeBase tmpNode;//�ϴ� ���� ȣ��Ǵ� �κ��̶� update ������ ����
        private List<W_AINodeBase> tmpNodes;//�ϴ� ���� ȣ��Ǵ� �κ��̶� update ������ ����

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
                if (_emergencyQue.Count > 0)//��� ť�� �����ϸ�. ��� ť ���� �̿�
                {

                    _tmpQue = _emergencyQue;
                }
                else//���� ���� �Ϲ� ť �̿�. �켱 ����
                {

                    _tmpQue = _jobQue;
                }

                int tmpCount = _tmpQue.Count;
                for (int i = 0; i < tmpCount; i++)
                {
                    tmpNode = _tmpQue.Dequeue();
                    tmpNode.Begin(value);//�� �κ��� begin �ʱ�ȭ �����θ� ���� ��� �����ؾ����� ����� �ʿ��� ��.
                    if (tmpNode.Update() == false)//update�� false(�۾� �Ϸ�) �������� end ȣ��
                    {
                        tmpNodes = tmpNode.End(); //end�� ��� List ��ȯ
                        for (int j = 0; j < tmpNodes.Count; j++)
                        {   
                       
                            _tmpQue.Enqueue(tmpNodes[j]);//�� �޾ƿ� ������ �ش� ť�� ����(��� ť�� ���õ� �Ÿ� ��� ť��, ��� ť�� ������ �۾� ť��... �� �κ��� �� ���ؾ��Ұ� ����.
                        }
                    }
                    else
                    {
                        _tmpQue.Enqueue(tmpNode);//update�� ����ؼ� ������(ture)�̸� �ٽ� �۾� ť�� ���� 
                    }
                }
            }






        }

        public void SetAI(W_AITree tree)//���߿� AI Dictionary�� ���� ai�� �����ϸ� ������ ������... �ϴ��� �ּ����� ���ܵ�.
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

