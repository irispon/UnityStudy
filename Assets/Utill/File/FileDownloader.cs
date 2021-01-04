using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileDownloader : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(DownloadFileRoutine());
    }

    public IEnumerator DownloadFileRoutine()
    {
        UnityWebRequest request = UnityWebRequest.Get("url");
        yield return request.Send();

        if (request.isNetworkError)
        {
            Debug.Log("DownloadError");
        }
        else
        {
            SaveFile(request.downloadHandler.data);
        }
    }

    public void SaveFile(byte[] bytes)
    {
        string destination = Application.persistentDataPath + "/fileName";

        if (File.Exists(destination))
        {
            File.Delete(destination);
        }

        File.WriteAllBytes(destination, bytes);
    }
}
