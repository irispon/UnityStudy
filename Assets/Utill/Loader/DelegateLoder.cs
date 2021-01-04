using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateLoder :ILoader
{
    bool done= false;
    Action action;
    string context;
    public void DelegateLoader(string context,Action action)
    {
        this.context = context;
        this.action = action;
    }
    public void Clear()
    {
       
    }

    public string GetContext()
    {
        return context;
    }

    public bool IsDone()
    {
        return done;
    }

    public void Load()
    {
        action();
    }


}
