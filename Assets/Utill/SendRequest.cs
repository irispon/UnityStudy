using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendRequest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetRequest(ServerHelper.SERVERPATH+"/"+SocketEvent.database.ToString()));
    }

    IEnumerator GetRequest(string uri)
    {
        WWWForm wWForm = new WWWForm();
        wWForm.AddField("sql", "SELECT * from profileresource");
        UnityWebRequest uwr = UnityWebRequest.Post(uri, wWForm);
        
     
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            JArray jObject = JArray.Parse(uwr.downloadHandler.text);
            Debug.Log(jObject);
            

        }
    }

}
