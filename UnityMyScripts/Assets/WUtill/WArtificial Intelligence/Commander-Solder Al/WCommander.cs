using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsAi
{
   public enum eCommand
   {
        STOP,ATTACK=1<<1,MOVE=1<<2,SEARCH=1<<3
   }
   public enum eTarget
   {
       ENEMY,ENEMIES,FACTION
   }
   public struct Personality
   {
        public float determination;//결정력
        public float passion;//열정
        public float fear;//겁

        public float inteligent;//똑똑한 정도 =>지성. 어떤 사건에 대하여 판단하는 요인이 늘어남.
        public float planed;//계획적인 정도=> 미래를 예측하고 계획적으로 하려고 하는 팩터
        public float sensual;//감각적으로 하는 정도 => 계획에 대한 확실성이 부족할 때 그 때 그 때 판단함.
        public float reckless;//무모한 정도 => 계획에 대한 확실성이 부족할 때 그대로 시도함.

        public float leadership;//지도력 통솔력 => 부족하면 명령이 제대로 전달이 안됨. 
        public float potential;//성장 가능성. 이에 따라 성장이 달라짐.
        public float sight;//시야 => 시야에 따라서 체크하는 요인 변경.


    }
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
    }

}
