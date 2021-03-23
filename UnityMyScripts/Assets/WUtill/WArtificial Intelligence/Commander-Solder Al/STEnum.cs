using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsAI
{
    public enum eCommand
    {
        STOP, ATTACK = 1 << 1, MOVE = 1 << 2, SEARCH = 1 << 3
    }
    public enum eTarget
    {
       NONE,HIGH=1<<1,ROW =1<<2, ENEMY=1<<3, ENEMIES=1<<4, FACTION=1<<5,ALLIE=1<<6,FRIEND=1<<7,RANGE=1<<8
    }
    public enum eStrategyStandard
    {
        STANDARD,UNIQUE,RUSH
    }
    public enum eState
    {
        SEARCH=1<<1,ENCOUNT=1<<2,ENGAGE=1<<3,INFERIORITY=1<<4,ISOLATE=1<<5//불리 유리=>규모
    }
    public enum eCondition
    {
        NOT=1<<1,RANGE=1<<2, INFERIORITY=1<<3,ISOLATE,COVER=1<<4,CASTING,MP=1<<5,HP=1<<6, Figure=1<<7, OUT=1<<8,IN=1<<9
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
    public class Think
    {
        public eCommand command;
        public eTarget target;
        public eStrategyStandard strategy;
        public eState state;//판단
        public eCondition condition;

        public Think next;
       
    }

}