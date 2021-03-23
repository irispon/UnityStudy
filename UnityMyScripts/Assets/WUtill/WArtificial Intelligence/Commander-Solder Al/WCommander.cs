using CsAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsAI
{
    /// <summary>
    /// 지휘관의 포메이션
    /// </summary>
    public struct Pomation
    {

    }
    //Soldier와 Commander 간의 클래스 체인지 고려해야함.
    public class WCommander : MonoBehaviour
    {
        Personality person;

        public List<ISoldier> soldiers;
        public Strategy strategy;
        public void Awake()
        {
            soldiers = new List<ISoldier>();
        }

        public void Allocate(ISoldier soldier)
        {
            soldiers.Add(soldier);
        }
        
        public void SetAbiity(Personality abiity)
        {
            person = abiity;

        }
        public void SetStrategy(Strategy strategy)
        {
            this.strategy = strategy;
        }
    }

}
