using FiniteState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WThread;

public class CharacterManager : MonoBehaviour
{
    /// <summary>
    /// ������ �����ϴ� �Ŵ���. ������ �з��� ���� �����ϴ� ��Ŀ���� �����Ѵ�.
    /// ���⼭ �����ؾ��� ���� �߰��� �� ���� ��������� �þ�� ������ �ʹ� ���� �߰��� ������ ���� ���ϸ� ����ų �� ����.
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
            Debug.Log("���� �׽�Ʈ");
            enumerator.Current.Dispose();
        }

    }

}
