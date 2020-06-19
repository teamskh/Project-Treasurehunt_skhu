using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using LitJson;
using BackEnd;

public class ServerSetting : MonoBehaviour
{

    string id, pw, etc;
    string nickName; // 닉네임
    string userInDateScore;
    string userInDatePurchase;
    private string ScoreTableName = "score", ScoreColumnName = "score";
    private string PurchastTableName = "purchase", PurchaseColumnName = "purchase";
    internal List<Notice> noticeList = new List<Notice>();


    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        // [.net4][il2cpp] 사용 시 필수 사용
        Backend.Initialize(() =>
        {
            // 초기화 성공한 경우 실행
            if (Backend.IsInitialized)
            {
#if UNITY_ANDROID
                // ----- GPGS -----
                PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
                    .Builder()
                    .RequestServerAuthCode(false)
                    .RequestEmail()
                    .RequestIdToken()
                    .Build();

                //커스텀된 정보로 GPGS 초기화
                PlayGamesPlatform.InitializeInstance(config);
                PlayGamesPlatform.DebugLogEnabled = true;
                //GPGS 시작.
                PlayGamesPlatform.Activate();
#endif
            }
            // 초기화 실패한 경우 실행
            else
            {

            }
        });

        
    }
    /*
    public void GetUserInfo()
    {
        Backend.BMember.GetUserInfo(userInfoBro =>
        {
            if (userInfoBro.IsSuccess())
            {
                JsonData Userdata = userInfoBro.GetReturnValuetoJSON()["row"];
                JsonData nicknameJson = Userdata["nickname"];

                // 닉네임 여부를 확인
                if (nicknameJson != null)
                {
                    // nickName 변수에 닉네임 저장
                    nickName = nicknameJson.ToString();
                    Debug.Log("NickName is " + nickName);
                    DispatcherAction(BackEndUIManager.instance.CloseAll);

                    // 닉네임이 존재할 시에만 채팅서버에 접속
                    DispatcherAction(BackEndChatManager.instance.EnterChatServerInUserInfo);
                }
                else
                {
                    Debug.Log("NickName is null");
                    DispatcherAction(() => BackEndUIManager.instance.ShowNickNameUI(false));
                }
            }
            else
            {
                ShowMessage("[X]유저 정보 가져오기 실패");
                Debug.Log("[X]유저 정보 가져오기 실패 - " + userInfoBro);
            }
        });
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Notice
{
    internal string Title { get; set; }          // 공지사항 제목
    internal string Content { get; set; }        // 공지사항 내용
    internal string ImageKey { get; set; }       // 공지사항 이미지 (존재할 경우)
    internal string LinkButtonName { get; set; } // 공지사항 버튼 (누르면 공지사항 UI를 띄우기 위한)
    internal string LinkUrl { get; set; }        // 공지사항 URL (존재할 경우)
}
