# AssetBundle



## 에셋 번들?

![img](https://blog.kakaocdn.net/dn/3abYV/btqzqdxLHLR/kL5jkrYpg2WjIn85DRxUk1/img.jpg)

에셋 번들은 에셋들의 묶음으로 본다. 에셋번들을 이용하면 프로그램 실행 중 동적 로드를 할 수 있으며 콘텐츠 다운로드 등에 사용한다.



## 에셋 번들로 묶는 방법



프리팹을 만든 후 각각의 프리팹의 에셋번들 이름을 지정한다.

프로젝트뷰 프리팹 선택 -> 인스펙터 에셋번들 드롭다운 클릭 -> New 클릭 -> 에셋번들 이름 입력



![img](https://blog.kakaocdn.net/dn/k81PA/btqzmk6RzaV/FKbkr4fZWKkgnXwwg3xccK/img.png)



관련 스크립트

```c#
using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles";

        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
```



BuildBuildPipeline.BuildAssetBundles(string outputPath, BuildAssetBundleOptions assetbundleOptions, BuildTarget targetPlatform) 인자 설명

- string outputPath : 에셋번들 생성 경로
- BuildAssetBundleOptions assetbundleOptions : 에셋번들 빌드 옵션
- BuildTarget targetPlatform : 빌드 대상 플랫폼



## 에셋 번들 로드



![img](https://blog.kakaocdn.net/dn/cpW0fE/btqzpW49FVJ/rGNlYoExB2AZTjC22U2e11/img.jpg)





### AssetBundle.LoadFromMemoryAsync(byte[] binary)

AssetBundle.LoadFromMemoryAsync(byte[] binary) 함수는 파일의 바이트 배열을 읽어서 에셋번들을 비동기 방식으로 로드함.

```c#
using System.Collections;
using System.IO;
using UnityEngine;

public class LoadAssetBundlesFromMemory : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        string path = "Assets/AssetBundles/bundle";

        StartCoroutine(LoadFromMemoryAsync(path));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadFromMemoryAsync(string path)
    {
        AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));

        yield return request;

        AssetBundle bundle = request.assetBundle;
        var prefab = bundle.LoadAsset<GameObject>("Stone");
        Instantiate(prefab);

        prefab = bundle.LoadAsset<GameObject>("Brick");
        Instantiate(prefab);
    }
}
```

 9 : 로드할 에셋번들 경로

11 : 에셋번들로드 코루틴 시작

22 : 경로로부터 모든 바이트를 읽어서 메모리에 비동기로 로드

24 : 바이트를 모두 읽을 때까지 대기

27 : 에셋번들 클래스로 Stone 게임오브젝트 로드

28 : Stone 게임오브젝트 생성

30 : 에셋번들 클래스로 Brick 게임오브젝트 로드

31 : Brick 게임오브젝트 생성



### AssetBundle.LoadFromFile(string path)

**AssetBundle.LoadFromFile(string path)** 함수는 디스크에서 직접 에셋번들을 로드. 압축되지 않은 것을 로드할 때 유용함.



```c#
using System.IO;
using UnityEngine;

public class LoadAssetBundleFromFile : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath + "/AssetBundles", "bundle"));

        if (bundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }

        var prefab = bundle.LoadAsset<GameObject>("Stone");
        Instantiate(prefab);

        prefab = bundle.LoadAsset<GameObject>("Brick");
        Instantiate(prefab);
    }
}
```

 9 : 파일로부터 에셋번들 로드

17 : 에셋번들 클래스로 Stone 게임오브젝트 로드

18 : Stone 게임오브젝트 생성

20 : 에셋번들 클래스로 Brick 게임오브젝트 로드

21 : Brick 게임오브젝트 생성





### UnityWebRequest 클래스 사용

UnityWebRequest 클래스는 에셋번들을 처리하는 함수를 가지고 있음.

에셋번드을 로드하려면 UnityWebRequest.GetAssetBundle을 사용해서 웹 요청을 만들고 요청이 완료되면 요청 오브젝트를 DownloadHandlerAssetBundle.GetContent(UnityWebRequest)로 전달하고 GetContet()를 사용해서 에셋번들을 로드가 가능.

#### 동기 방식

```c#
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAssetBundlesFromUnityWebRequest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadAssetBundle());
	}
	
    IEnumerator LoadAssetBundle()
    {
        string uri = "file:///" + Application.dataPath + "/AssetBundles/bundle";
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri);

        yield return request.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        var prefab = bundle.LoadAsset<GameObject>("Stone");
        Instantiate(prefab);

        prefab = bundle.LoadAsset<GameObject>("Brick");
        Instantiate(prefab);
    }
}
```



#### 비동기 방식

```c#
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAssetBundlesFromUnityWebRequestAsync : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadAssetBundleAsync());
	}
	
    IEnumerator LoadAssetBundleAsync()
    {
        string uri = "file:///" + Application.dataPath + "/AssetBundles/bundle";
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri);

        yield return request.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        AssetBundleRequest bundleRequest = bundle.LoadAssetAsync<GameObject>("Stone");

        yield return bundleRequest;

        var prefab = bundleRequest.asset;
        Instantiate(prefab);

        bundleRequest = bundle.LoadAssetAsync<GameObject>("Brick");

        yield return bundleRequest;

        prefab = bundleRequest.asset;
        Instantiate(prefab);
    }
}
```

### 로드된 에셋번들 관리



에셋번들을 로드하고 나면 로드된 에셋번들을 언로드해야 합니다.

에셋번들을 언로드하지 않거나 잘못 언로드하면 메모리 중복 혹은 에셋이 누락되는 등의 상황 발생.

(누수 주의해야할 듯)

에셋번들을 언로드하는 시점을 잘 설정해야 합니다. 에셋번들을 언로드 하기위해서는 **AssetBundle.Unload(bool)**을 호출해야함.



```c#
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UnloadTrue : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadAssetBundleUnloadTrue());
    }
    
    IEnumerator LoadAssetBundleUnloadTrue()
    {
        string uri = "file:///" + Application.dataPath + "/AssetBundles/bundle";
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri);

        yield return request.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        GameObject[] prefabs = bundle.LoadAllAssets<GameObject>();

        foreach(GameObject prefab in prefabs)
            Instantiate(prefab);

        yield return new WaitForSeconds(0.01f);

        bundle.Unload(true);

    }
}
```