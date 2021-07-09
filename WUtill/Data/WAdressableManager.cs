using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

/// <summary>
/// 어드레서블 관련 코드 만들 예정
/// </summary>
public class WAdressableManager : MonoBehaviour
{
    private AsyncOperationHandle downHandle;
    public void OnClick_BundleDown()
    {
        downHandle = Addressables.DownloadDependenciesAsync("Key");
        downHandle.Completed +=
            (AsyncOperationHandle Handle) =>
            {
                Addressables.Release(Handle);
            };
    }
    public void OnClick_BundleDelete()
    {
        Addressables.ClearDependencyCacheAsync("Key");
    }
    
    public void OnClick_CheckSize()
    {
        Addressables.GetDownloadSizeAsync("Key").Completed +=
             (AsyncOperationHandle<long> SizeHandle) =>
             {
                 string sizeText = string.Concat(SizeHandle.Result, " byte");
                // assetInfoText3.text = sizeText;
                 Addressables.Release(SizeHandle);
             };
    }


    private List<AudioClip> audios = new List<AudioClip>();
    private AsyncOperationHandle<IList<AudioClip>> loadHandle;
    public void OnClick_LoadAssets()
    {
    //    loadHandle = Addressables.LoadAssetsAsync<AudioClip>("Label", (result) => { audios.Add(result); });
        loadHandle.Completed += (result) =>
        {
         //   audios.AddRange(result.Result);
            SetList();
        };
    }

    private void SetList()
    {
        for (int i = 0; i < audios.Count; ++i)
        {
            GameObject obj = null;//나중에 추가 필요.
            int selfNum = i;
          obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                GetComponent<AudioSource>().clip = audios[selfNum];
                GetComponent<AudioSource>().Play();
            });
       //     obj.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = audios[selfNum].name;
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
