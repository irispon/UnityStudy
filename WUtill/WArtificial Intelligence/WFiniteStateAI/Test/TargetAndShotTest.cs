using FiniteState;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 스레드 테스트를 위한 클래스입니다. 타겟을 정하고 공격을 hit시키거나 특정 미사일을 날리는 예제입니다.
/// </summary>
/// 

public class TargetAndShotTest : ICharacter
{

    WThread.WaitForSecondsRealtime seconds = new WThread.WaitForSecondsRealtime(2);
    /*내가 해야할 것 => 실제 시간 지연했다가 실행하기 */
    public Faction faction = Faction.Enemy;
    Vector3 target;

    public override void Schedule()
    {

        if (seconds.KeepWaiting())
        {
            Debug.Log("search");
            middleQue.Enqueue(() =>
            {
               

            });
        }
        
        base.Schedule();
    }


}
