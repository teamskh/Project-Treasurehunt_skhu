using BackEnd;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool isLogin = false;
    public static bool LoginKind = false;

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
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
