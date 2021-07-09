using System.Collections;
using System.IO;
using UnityEngine;

public class LoadAssetBundlesFromMemory : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        string path = "Assets/AssetBundles/bundle";

        StartCoroutine(LoadFromMemoryAsync(path));
    }
    IEnumerator LoadFromMemoryAsync(string path)
    {
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));

        yield return request;

        AssetBundle bundle = request.assetBundle;
      //  var prefab = bundle.LoadAsset<GameObject>("Stone");
     //   Instantiate(prefab);

     //   prefab = bundle.LoadAsset<GameObject>("Brick");
    //    Instantiate(prefab);
    }
}