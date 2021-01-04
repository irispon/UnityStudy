using socket.io;
using TMPro;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class ChatClient : MonoBehaviour
{
    InputField inputText;
    Socket chat;
    ChatManager manager;
    Action<string> messageTarget;
    PlayerCache playerInfo;
    // Start is called before the first frame update
    void Start()
    {
        messageTarget = s => ReciveMessage(s);
        manager = ChatManager.GetInstance();
        inputText = manager.inputText;
        chat = Socket.Connect(ServerHelper.SERVERPATH + "/"+SocketEvent.chat.ToString());
        playerInfo = PlayerCache.GetInstance();
        chat.On("connect", () => {
            Debug.Log("채팅 서버 접속"+ chat.IsConnected);
            chat.On(SocketEvent.chat.ToString(), messageTarget);

        });
         

    }
    //버튼
    public void SendMessage()
    {
       
        if (chat.IsConnected)
        {
            JObject message = new JObject();

           
            message.Add(ChatHelper.Client.ToString(), playerInfo.userName);
            message.Add(ChatHelper.Message.ToString(),inputText.text);
            message.Add(ChatHelper.Profile.ToString(), playerInfo.profile);
            inputText.text = "";
       //     Debug.Log(message);
            
            chat.EmitJson(SocketEvent.chat.ToString(), message.ToString());



        }

    }
  
    public void ReciveMessage(string message)
    {
        manager.ReciveMessage(message);

    }

}

