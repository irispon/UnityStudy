using Manager;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using WThread;

public class WTThreadTest : MonoBehaviour
{

    private CharacterManager manager ;
    public bool mode;
    public TestCharacter prefab;
    int i=0;
    public void Awake()
    {

        manager = CharacterManager.GET;
        
    }
    public void StartBtn()
    {
        Debug.Log(manager == null);
        manager.StartManaging("default");

        //manager.SetTick("default");
    }
    public void Stop()
    {
        manager.StopManaging("default");
    }
    public void Change()
    {
    }

    public void AddUnit()
    {
        TestCharacter pref = Instantiate(prefab);
        pref.name = name + (i++);
         pref.transform.position = new Vector3(Random.Range(580, 650), Random.Range(327, 350), Random.Range(-400,-350));

    }
}
