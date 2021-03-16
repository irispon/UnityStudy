using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoldier
{
    void Attack();
    void Move(Transform transform);
    void Search();
    
}
