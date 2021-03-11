using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDubleList<T>
{
    List<T> ActiveList;
    List<T> deActiveList;

    public WDubleList()
    {
        ActiveList = new List<T>();
        deActiveList = new List<T>();
    }

    public void Add(T item,bool isActive =true)
    {
        if (isActive)
        {
            ActiveList.Add(item);
        }
        else
        {
            deActiveList.Add(item);
        }

    }
}
