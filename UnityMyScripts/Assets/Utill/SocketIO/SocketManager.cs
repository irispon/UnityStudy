using socket.io;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager :SingletonObject<SocketManager>
{
    Dictionary<SocketEvent, Socket> sockets;

    public override void Init()
    {
        sockets = new Dictionary<SocketEvent, Socket>();
        DontDestroyOnLoad(this);
    }
    public Socket GetSocket(SocketEvent socketEvent)
    {
        Socket socket;


        if (sockets.ContainsKey(socketEvent))
        {

            return sockets[socketEvent];
        }
        else
        {
            socket = Socket.Connect(ServerHelper.SERVERPATH + "/" + socketEvent.ToString());
            sockets.Add(socketEvent, socket);
        }
   
     
        return socket;
    }


    public void Clear()
    {
        foreach(Socket socket in sockets.Values)
        {
            Destroy(socket.gameObject);
        }

    }
    public void Close(Socket socket)
    {
        socket.Emit("disconnect");
        Destroy(socket.gameObject);
    }
    public void Close(SocketEvent @event)
    {
        if (sockets.ContainsKey(@event))
        {
            Close(sockets[@event]);
        }
    
    }
}
