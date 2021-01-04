using System;
using System.Collections;
using System.Collections.Generic;
using socket.io;

using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp.Server;

namespace Assets.Utill.UI.Loading
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private string scene;
        [SerializeField]
        private List<Loader> loaders;
        [SerializeField]
        bool isLocal;
        // Start is called before the first frame update
        void Start()
        {


            LocalCheck();
            List<ILoader> iloaders = new List<ILoader>(loaders);
            LoadingManager.LoadScene(scene, loaders: iloaders);

        }

        void LocalCheck()
        {


            UnityWebRequest uwr = UnityWebRequest.Get(ServerHelper.SERVERPATH);

            uwr.SendWebRequest();

            while (!uwr.isDone)
            {
                new WaitForSeconds(0.1f);
                //나중에 변경해야될 코드.(로그인이 안되는 경우)
            }



            if (uwr.isNetworkError)
            {

                ServerHelper.SERVERPATH = "http://192.168.219.126:4444";
                Debug.Log("Error While Sending: " + ServerHelper.SERVERPATH);
            }
            else
            {
                Debug.Log("Received: " + uwr.downloadHandler.text);

                if (uwr.downloadHandler.text.Equals("true"))
                {

                }



            }



        }
    }
}