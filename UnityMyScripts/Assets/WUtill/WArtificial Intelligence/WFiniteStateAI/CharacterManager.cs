using FiniteState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WThread;
using System.Threading;
using System;
/// <summary>
/// ������ �����ϴ� �Ŵ���. ������ �з��� ���� �����ϴ� ��Ŀ���� �����Ѵ�.
/// ���⼭ �����ؾ��� ���� ���� �з��� �߰��� �� ���� ��������� �þ�� ������ �ʹ� ���� �߰��� ������ ���� ���ϸ� ����ų �� ���� �� ����.
/// </summary>
/// 

namespace Manager
{
    /// <summary>
    /// Thread�� Corutine ���� ����ȭ�� ���� ���� Ŭ����
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
    /// ĳ���͵��� �����ϴ� �Ŵ���
    /// </summary>
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager GET { get; private set; }


        /// <summary>
        /// Thread�� �����ϴ� Ŭ������ �ڷ�ƾ�� �۵��� ���� �����ְ�. �ڷ�ƾ�� �������� �� �۵��ϴ� ����� ���� �������� ���·� �����Ͽ���. 
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
                            Debug.Log("���� �߰�");
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
                tmpCharacters.Add(character);//�ӽ� ���Կ� ����
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
                Debug.LogError("�ش� Ű ���� �����ϴ�!");
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
                Debug.LogError("�ش� Ű ���� �����ϴ�!");
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
                Debug.LogError("�ش� Ű ���� �����ϴ�!");
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
                Debug.Log("���� �׽�Ʈ");
                enumerator.Current.Dispose();
            }

        }

    }




}
