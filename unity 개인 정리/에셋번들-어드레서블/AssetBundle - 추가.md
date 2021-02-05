# 캐싱

에셋번들 다운로드에는 캐싱, 논캐싱 2가지 방법이 존재함. 캐싱이란 이미 출시한 게임에 새로운 컨텐츠를 제공할 때 제공하는 DLC(Downlodable Contents)를 구현하거나, 대규모 게임에서 자주 업데이트가 발생할 경우 실행 파일과 에셋번들을 조합해 사용하는데 이 때마다 새롭게 네트워크에서 다운 받으면 네트워크의 속도 저하가 발생함. 그래서 아래 그림처럼 한번 다운로드 받은 파일은 하드디스크에 저장(캐싱)하고, 두 번째 부터는 네트워크가 아닌 하드디스크에서 바로 로드하는 것이 캐싱.

![img](https://t1.daumcdn.net/cfile/tistory/27598E44583B795C2E)

# 에셋번들 다운로드

**Non-caching :** 새 WWW object(ScriptRef : WWW.WWW.html)을 만드는 것으로 할 수 있습니다. 에셋 번들은 로컬 저장 장치 Unity의 Cache 폴더에 캐시되지 않습니다.

**Caching :**  [WWW.LoadFromCacheOrDownload](ScriptRef : WWW.LoadFromCacheOrDownload.html)를 호출. 에셋 번들은 로컬 저장 장치 Unity의 Cache 폴더에 캐시됨.



**Caching 코드**

```c#
using System;
using UnityEngine;
using System.Collections;

public class CachingLoadExample : MonoBehaviour {
    public string BundleURL;
    public string AssetName;
    public int version;

    void Start() {
        StartCoroutine (DownloadAndCache());
    }

    IEnumerator DownloadAndCache (){
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

// Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache

        using(WWW www = WWW.LoadFromCacheOrDownload (BundleURL, version)){
            yield return www;
            if (www.error != null)
                throw new Exception("WWW download had an error:" + www.error);
            AssetBundle bundle = www.assetBundle;
            if (AssetName == "")
                Instantiate(bundle.mainAsset);
            else
                Instantiate(bundle.LoadAsset(AssetName));
                    // Unload the AssetBundles compressed contents to conserve memory
                    bundle.Unload(false);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
```

**에셋번들 로드**

```c#
// C# Example
// Loading an Asset from disk instead of loading from an AssetBundle
// when running in the Editor
using System.Collections;
using UnityEngine;

class LoadAssetFromAssetBundle : MonoBehaviour
{
    public Object Obj;

    public IEnumerator DownloadAssetBundle<T>(string asset, string url, int version) where T : Object {
        Obj = null;

#if UNITY_EDITOR
        Obj = Resources.LoadAssetAtPath("Assets/" + asset, typeof(T));
        yield return null;

#else
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        // Start the download
        using(WWW www = WWW.LoadFromCacheOrDownload (url, version)){
            yield return www;
            if (www.error != null)
                        throw new Exception("WWW download:" + www.error);
            AssetBundle assetBundle = www.assetBundle;
            Obj = assetBundle.LoadAsset(asset, typeof(T));
            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)

#endif
    }
}
```