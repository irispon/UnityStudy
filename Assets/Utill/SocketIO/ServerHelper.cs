using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHelper 
{

    public static string SERVERPATH = "http://122.38.89.43:4444";
    public static string LOCALPATH = "http://192.168.219.126:4444";//테스트용
 //   public static string DATABASEACESS = SERVERPATH + "/" + PostEvent.database.ToString();
    public static string DATABASEPATH()
    {

        string DATABASEACESS = SERVERPATH + "/" + PostEvent.database.ToString();
        return DATABASEACESS;
    }

    public static string LOGINPATH()
    {

        string LOGIN = SERVERPATH + "/" + PostEvent.login;
        return LOGIN;
    }
    public static string CREATEACCOUNT()
    {

        string account = SERVERPATH + "/" + PostEvent.account;
        return account;
    }

}

public enum SocketEvent
{
    chat, database, room
}

public enum PostEvent
{
    certificate, database, login,account,error,success,getinfo,setinfo,disconnect
}
public enum GetEvent
{
    profiles
}
public enum ServerEvent
{
    reconnect, connect, reconnecting, connectError, reconnectError, connectTimeOut, connection, disconnect
}
public enum ChatHelper
{
    Client,Message,Profile
}