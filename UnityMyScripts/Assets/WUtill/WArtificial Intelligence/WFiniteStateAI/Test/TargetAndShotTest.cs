using FiniteState;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������ �׽�Ʈ�� ���� Ŭ�����Դϴ�. Ÿ���� ���ϰ� ������ hit��Ű�ų� Ư�� �̻����� ������ �����Դϴ�.
/// </summary>
/// 

public class TargetAndShotTest : ICharacter
{

    WThread.WaitForSecondsRealtime seconds = new WThread.WaitForSecondsRealtime(2);
    /*���� �ؾ��� �� => ���� �ð� �����ߴٰ� �����ϱ� */
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
