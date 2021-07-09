using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class XListItemPrefab<Data> : MonoBehaviour
{
    public virtual void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);

        if (isActive == true)
        {
            BeginActive();
        }
        else
        {
            EndActive();
        }

  
    }
    public virtual void EndActive()
    {
  
    }
    public virtual void BeginActive()
    {
        
    }
    public abstract void Setup(Data data);

    public virtual float GetItemHeight()
    {

        return GetComponent<RectTransform>().sizeDelta.y;
    }
}
