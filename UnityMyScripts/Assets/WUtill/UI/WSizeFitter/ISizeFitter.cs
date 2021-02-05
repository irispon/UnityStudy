using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SizeManager에 등록해서 사용하는 함수로 XsizeManager에게 등록해서 사용하거나 인스펙터 상에서 등록해서 사용하면 됨. 에디터상에서 확인하고 싶으면 인스펙터 상에서 등록.
public abstract class ISizeFitter:MonoBehaviour
{
    public abstract void Fit();
}
