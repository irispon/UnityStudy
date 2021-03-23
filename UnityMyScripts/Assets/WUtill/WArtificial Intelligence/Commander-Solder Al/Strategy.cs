using CsAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsAI
{
    //조건=> 결과 조건 결과....
    //루프?
    //루프를 통해 순회를 해야하나?
    //그러면 비 효율적인거 같음.
    //카테고리를 나눠야됨.
    //전투에는 전투. 생활에는 생활. 이머전시, 우선순위 기타 등등....등등등등등등등등...

    public class Strategy
    {
        static Dictionary<string, Strategy> stratigies = new Dictionary<string, Strategy>();
        Dictionary<eState, Think> thinks;
        public Strategy Creat(string key)
        {
            stratigies.Add(key, this);
            return this;
        }
        


    }

}
