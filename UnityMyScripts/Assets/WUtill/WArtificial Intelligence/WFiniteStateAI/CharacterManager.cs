using FiniteState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WThread;
using System.Threading;
using System;
/// <summary>
/// 유닛을 관리하는 매니저. 유닛의 분류에 따라 관리하는 워커들이 증가한다.
/// 여기서 유의해야할 점은 유닛 분류를 추가를 할 수록 쓰레드수가 늘어나기 때문에 너무 많은 추가는 오히려 성능 저하를 일으킬 수 있을 것 같다.
/// </summary>
/// 

namespace Manager
{
    /// <summary>
    /// Thread와 Corutine 간의 동기화를 위해 만든 클래스
    /// </summary>
    public class UnitCustomWait : CustomYieldInstruction
    {
        public bool isContinue;
        public override bool keepWaiting
        {
            get
            {
                return !isContinue;
            }
        }

    }


    /// <summary>
    /// 캐릭터들을 관리하는 매니저
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager GET { get; private set; }


        /// <summary>
        /// Thread를 관리하는 클래스로 코루틴이 작동할 때는 멈춰있고. 코루틴이 멈춰있을 때 작동하는 방식을 통해 절차적인 형태로 구현하였다. 
        /// </summary>
        public class stWorker
        {
           public EventWaitHandle threadWait = new EventWaitHandle(true, EventResetMode.ManualReset);
           public EventWaitHandle corutineWait = new EventWaitHandle(true, EventResetMode.ManualReset);
        

            public List<ICharacter> characters = new List<ICharacter>();
            public List<ICharacter> tmpCharacters = new List<ICharacter>();
            public WTWorker worker;
            public UnitCustomWait wait = new UnitCustomWait();

            public IEnumerator schedule;
            public stWorker(int tick=0)
            {
                worker = new WTWorker(() => {

                    if (tmpCharacters.Count > 0)
                    {
                        for (int i = 0; i < tmpCharacters.Count; i++)
                        {
                            characters.Add(tmpCharacters[i]);
                            Debug.Log("유닛 추가");
                        }
                        tmpCharacters.Clear();
                    }
                    threadWait.Reset();
                    for (int i = 0; i < characters.Count; i++)
                    {
                  //      Debug.Log("action char" + characters.Count);
                        characters[i].Action();

                    }
                    wait.isContinue = true;
                    //   corutineWait.Set();
                    threadWait.Reset();
                    threadWait.WaitOne();
                }, tick);
                worker.Stop();
            }
            public void Add(ICharacter character)
            {
                tmpCharacters.Add(character);//임시 슬롯에 저장
            }
            public void Remove(ICharacter character)
            {

                characters.Remove(character);
            }
            public void Dispose()
            {


                worker.Dispose();

            }
            public void Start()
            {
                worker.Start();
                
            }
            public void SetSchedule(IEnumerator schedule)
            {
                this.schedule = schedule;
            }
            internal void Stop()
            {
                worker.Stop();
            }
            public void SetTick(int tick)
            {
                worker.SetTick(tick);
            }
        }


        private Dictionary<string, stWorker> units;
        public void Awake()
        {
            if (GET == null)
                GET = this;
            units = new Dictionary<string, stWorker>();

            //  units.Add("default", new stWorker());


        }
        public bool AddUnit(string key, ICharacter character, bool isNewable = false)
        {
            stWorker worker;

            if (units.ContainsKey(key) == false)
            {
                if (isNewable == false)
                    return false;
                else
                {
                    worker = new stWorker();
                    units.Add(key, worker);
                    IEnumerator schedule = Schedule(worker);
                    StartCoroutine(schedule);
                    worker.SetSchedule(schedule);
                }


            }

            units[key].Add(character);

            return true;

        }
        public void StartManaging(string key)
        {

            if (units.ContainsKey(key))
            {
                units[key].Start();
                StartCoroutine(units[key].schedule);
            }
            else
            {
                Debug.LogError("해당 키 값이 없습니다!");
            }
        }
        public void StopManaging(string key)
        {

            if (units.ContainsKey(key))
            {
                units[key].Stop();
                StopCoroutine(units[key].schedule);

            }
            else
            {
                Debug.LogError("해당 키 값이 없습니다!");
            }
        }
        public void SetTick(string key,int tick)
        {
            if (units.ContainsKey(key))
            {
                units[key].SetTick(tick);

            }
            else
            {
                Debug.LogError("해당 키 값이 없습니다!");
            }
        }
        public IEnumerator Schedule(stWorker worker)
        {

            for (; ; )
            {
       
                //  corutineWait.Reset();
                for (int i = 0; i < worker.characters.Count; i++)
                {
                    worker.characters[i].Schedule();
                }
                worker.threadWait.Set();
                worker.wait.isContinue = false;
                //  corutineWait.WaitOne();
                yield return worker.wait;
            }
        }

        private void OnDestroy()
        {
            IEnumerator<stWorker> enumerator = units.Values.GetEnumerator();

            for (; enumerator.MoveNext();)
            {
                Debug.Log("해제 테스트");
                enumerator.Current.Dispose();
            }

        }

    }




}
