using BackEnd;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Player : MonoBehaviour
{
    public static bool isLogin = false;
    public static bool LoginKind = false;
    string path = "Assets/Resources/Log/{0}.dat";
    List<PlayerGameLog> Log = new List<PlayerGameLog>();
    string user;

    private void OnEnable()
    {
        //로그인 이후
        //유저 코드를 user에 저장
        path = string.Format(path, user);
        LoadPlayerLog(); 
    }
    /*
    public static bool AdminLogin()
    {
        if (isLogin)
        {
            BackendReturnObject bro = new BackendReturnObject();
            bro = Backend.BMember.CustomLogin("Admin", "toomuch");
            if (bro.IsSuccess())
            {
                LoginKind = true;
                return true;
            }
        }
        return false;
    }

    public static bool PlayerLogin()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
            .Builder()
            .RequestServerAuthCode(false)
            .RequestIdToken()
            .RequestEmail()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        
        while(PlayGamesPlatform.Instance.localUser.authenticated == false)
        { 
            Social.localUser.Authenticate(success =>
            {   
                if (success == false)
                {
                    Debug.Log("구글 로그인 실패");
                    return;
                }
            });
        }

        BackendReturnObject bro = new BackendReturnObject();
        bro = Backend.BMember.AuthorizeFederation(
            PlayGamesPlatform.Instance.GetIdToken(), FederationType.Google);
        if (bro.IsSuccess())
        {
            LoginKind = false;
            return true;
        }
        else
            return false;
    }*/
    void Start()
    {
    }

    void Update()
    {
        
    }

    void LoadPlayerLog()
    {
        Stream rs = new FileStream(path, FileMode.OpenOrCreate);
        BinaryFormatter deserializer = new BinaryFormatter();

        Log = (List<PlayerGameLog>)deserializer.Deserialize(rs);
        rs.Close();
    }

    public void Save()
    {
        Stream ws = new FileStream(path, FileMode.Truncate);
        BinaryFormatter serializer = new BinaryFormatter();

        serializer.Serialize(ws, Log);
        ws.Close();
    }
}
