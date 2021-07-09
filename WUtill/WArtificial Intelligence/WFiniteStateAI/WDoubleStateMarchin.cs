using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 넣어야 될 기능. 인공지능 툴, tool to xml, xml to script, tool to script
/// </summary>
 namespace FiniteState
{  
    public enum lowEmotion
    {
        Normal,Confidence,Anger,Fear
    }
    public enum State
    {
        Idle=1<<1, Search=1<<2, Engage=1<<3
    }

    abstract class WDoubleStateMarchin
    {
  

    }

}

