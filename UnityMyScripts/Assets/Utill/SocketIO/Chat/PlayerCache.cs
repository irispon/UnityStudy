using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCache : SingletonObject<PlayerCache>
{
    public string id;
    public string token;
    public string userName = "NoName";
    public string profile;
    public Sprite sprite;

    public override void Init()
    {
        DontDestroyOnLoad(this);
    }
    /// <summary>
    /// token을 받아오는 메서드입니다. 설정이 아닙니다.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    public void Set(string id, string token)
    {
        this.id = id;
        this.token = token;
        //여기서 플레이어 정보 요청 id,token
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("token", token);
        Debug.Log("업데이트 요청" + id+" "+token);
        GetUserData(Request.AsyncPostRequest(ServerHelper.SERVERPATH + "/" + PostEvent.getinfo, form));//포스트 설계 바꿔야할듯
        
    }
    public void UpdateInfo()
    {

        WWWForm form = new WWWForm();
        form.AddField("profile", profile);
        form.AddField("name", userName);
        form.AddField("id", id);
        form.AddField("token", token);
        Request.PostSend(ServerHelper.SERVERPATH + "/" + PostEvent.setinfo, form);

      
        // player 프로파일 유저네임 서버에 업데이트

    }
    public void SetProfile(string path,Sprite sprite =null)
    {
        
        this.profile = path;
        if (sprite == null)
        {
            try
            {
                this.sprite = Resources.Load<Sprite>(profile);
            }
            catch (Exception e)
            {
                Debug.Log("리소스 로딩 오류" + e);
            }

        }
        else
        {
            this.sprite = sprite;
        }

        UpdateInfo();
    }
    public void SetName(string name)
    {

        this.userName = name;
        UpdateInfo();
    }
    private void OnApplicationQuit()
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("id", id);
        UpdateInfo();
        Request.PostSend(ServerHelper.SERVERPATH + "/" + PostEvent.disconnect, form);

        //     Task<string> task=  Post.PostRequest(ServerHelper.SERVERPATH + "/" + "disconnect", form);
        Debug.Log("어플리케이션 종료");

    }

    public async void GetUserData(Task<string> userData)
    {
        string data = await userData;
        JObject jObject = JObject.Parse(data);
        id = jObject["id"].ToString();
        userName = jObject["name"].ToString();
        profile = jObject["profile"].ToString();
        try
        {
            this.sprite = Resources.Load<Sprite>(profile);
        }
        catch (Exception e)
        {
            Debug.Log("리소스 로딩 오류"+e);
        }

    }

}
