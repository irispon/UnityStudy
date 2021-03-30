using FiniteState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WThread;

public class CharacterManager : MonoBehaviour
{
    /// <summary>
    /// 유닛을 관리하는 매니저. 유닛의 분류에 따라 관리하는 워커들이 증가한다.
    /// 여기서 유의해야할 점은 추가를 할 수록 쓰레드수가 늘어나기 때문에 너무 많은 추가는 오히려 성능 저하를 일으킬 수 있음.
    /// </summary>
    class stWorker
    {
        public stWorker()
        {
            characters = new List<ICharacter>();
            worker = new WTWorker(()=> { 
               for(int i = 0; i < characters.Count; i++)
               {
                    characters[i].Action();


               }
                    
            },1);
            worker.Stop();

        }

        public void Add(ICharacter character)
        {

            characters.Add(character);
            if (characters.Count== 1)
            {
                worker.Start();
            }

        }
        public void Dispose()
        {
            worker.Dispose();
        }

        public List<ICharacter> characters;
        public WTWorker worker;
    }
    private Dictionary<string, stWorker> units;
    public void Awake()
    {
        units = new Dictionary<string, stWorker>();
        stWorker worker = new stWorker();

        units.Add("default", new stWorker());
        
    
    }
    public bool AddUnit(string key,ICharacter character, bool isNewable=false)
    {
        if (units.ContainsKey(key)==false)
        {
            if (isNewable == false)
                return false;
            else
                units.Add(key, new stWorker());

        }

        units[key].Add(character);
        return true;
        
    }

    private void OnDestroy()
    {
        IEnumerator<stWorker> enumerator = units.Values.GetEnumerator();

        for(; enumerator.MoveNext();)
        {
            Debug.Log("해제 테스트");
            enumerator.Current.Dispose();
        }

    }

}
