using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Text UI 사용
using UnityEngine.UI;
// 구글 플레이 연동
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayManager : MonoBehaviour
{
    bool bWait = false;
    public Text text;

    void Awake()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        text.text = "no Login";
    }
    void Start()
    {

    }
    void Update()
    {

    }

    public void OnLogin()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool bSuccess) =>
            {
                if (bSuccess)
                {
                    
                    //유저 이름 저장
                    

                    Debug.Log("Success : " + Social.localUser.userName+Social.localUser.id);
                    gameman.instance.userna = Social.localUser.userName;
                    //text.text = gameman.instance.userna;
                    gameObject.GetComponent<scenechange>().changeFirstScene();
                }
                else
                {
                    Debug.Log("Fall");
                    text.text = "Fail";
                }
            });
        }
    }

    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        text.text = "Logout";
    }
}