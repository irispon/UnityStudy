using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using WThread;

public class WTThreadTest : MonoBehaviour
{

    private WTWorker worker;
    public bool mode;
    public void Awake()
    {

        worker = new WTWorker(ChangeOriginal);

    }
    public void StartBtn()
    {
        worker.Start();
    }
    public void Stop()
    {
        worker.Stop();
    }
    public void Change()
    {
        mode = !mode;

        if (mode)
            worker.ChangeAction(ChangeInnerContext);
        else
            worker.ChangeAction(ChangeOriginal);

   
        
    }
    private void ChangeInnerContext()
    {
        Debug.Log("변경");
        Thread.Sleep(2000);
    }
    private void ChangeOriginal()
    {
        Debug.Log("변경 전");
        Thread.Sleep(2000);
    }
    public void OnDisable()
    {

           
    }

    public void OnDestroy()
    {
        worker.Dispose();
    }
}
