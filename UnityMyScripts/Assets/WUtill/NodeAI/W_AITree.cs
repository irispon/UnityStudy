using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace W_AI
{

    [CreateAssetMenu(fileName = "W_AITree", menuName = "AI/W_AITree", order =0)]
    public class W_AITree : ScriptableObject
    {

        public bool isLoop = true;
        public bool isClone = false;
        public List<W_AINodeBase> nodes = new List<W_AINodeBase>();
        public Dictionary<int, W_AINodeBase> tmpDictionary = new Dictionary<int, W_AINodeBase>();


        //복사 과정을 거쳐야함.(scriptable object를 사용하는 개체끼리는 데이터가 공유되는 문제가 있음)
        //그냥 복사를 해서도 안됨. 그 이유는 순환될 경우 무한 루프를 돌기 때문임.
        public virtual W_AITree Clone()
        {
            W_AITree clone = Instantiate(this);
            clone.isLoop = true;
            clone.isClone = true;
            clone.tmpDictionary = new Dictionary<int, W_AINodeBase>();


            List<W_AINodeBase> tmpNodes = new List<W_AINodeBase>();
           for(int i = 0; i < nodes.Count; i++)
            {
                nodes[i].code = i; //임시 코드값 부여.
                tmpNodes.Add(nodes[i].Clone());
                clone.tmpDictionary.Add(tmpNodes[i].code, tmpNodes[i]);
            }

            clone.nodes = tmpNodes;


            for(int i =0; i < clone.nodes.Count; i++)
            {
                
                for(int j = 0; j < clone.nodes[i].trueActionNodes.Count; j++)
                {
                    if (clone.nodes[i].trueActionNodes[j].isClone == false)
                    {
                        clone.nodes[i].trueActionNodes[j] = clone.tmpDictionary[clone.nodes[i].trueActionNodes[j].code];
                    }


                }

                for (int j = 0; j < clone.nodes[i].falseActionNodes.Count; j++)
                {
                    if (clone.nodes[i].falseActionNodes[j].isClone == false)
                    {
                        clone.nodes[i].falseActionNodes[j] = clone.tmpDictionary[clone.nodes[i].falseActionNodes[j].code];
                    }
                }
            }

            return clone; 
            
        }

    }
}

