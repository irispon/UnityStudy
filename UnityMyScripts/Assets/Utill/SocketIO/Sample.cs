using socket.io;
using UnityEngine;



public class Sample : MonoBehaviour
{
    void Start()
    {
        var serverUrl = "http://localhost:4444";

        // news 네임스페이스
        var news = Socket.Connect(serverUrl + "/news");
        news.On("connect", () => {
            news.Emit("woot");
        });
        news.On("a message", (string data) => {
            Debug.Log("news => " + data);
        });
        news.On("item", (string data) => {
            Debug.Log(data);
        });

        // chat 네임스페이스
        var chat = Socket.Connect(serverUrl + "/chat");
        chat.On("connect", () => {
            chat.Emit("hi~");
        });
        chat.On("a message", (string data) => {
            Debug.Log("chat => " + data);
        });
    }


}
