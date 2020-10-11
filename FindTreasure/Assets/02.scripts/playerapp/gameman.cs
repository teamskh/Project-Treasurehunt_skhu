#if UNITY_ANDROID
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTM.Save;
using TTM.Classes;
using BackEnd;

using GooglePlayGames;
using GooglePlayGames.BasicApi;

using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

using System;
using LitJson;

public class gameman : MonoBehaviour
{
    public AudioSource baaudio;
    public AudioSource sfaudio;
    public string imageText; //문제,답 내용 결정

    public GameObject nicknamebar;

    public string userna;

    public string time;

    public bool isSuccess = false;

    [SerializeField]
    private InputField NicknameInput;

    public DateTime endtime; //서버에 있는 종료 시간
    public string endingTime; //최대 점수 도달시 시간
    public bool loadRankChek;
    public DateTime Opentime;
    //public bool che = false; //종료 시간 버튼 눌렸는지 확인 쓸모 없는 걸로 확인

    public int EndScore; //서버에 있는 대회 점수 얘로 결정
    public int score = 0;  //얘도 빼도 될듯

    public GameObject LoginB;
    public string conName;

    static gameman instance;
    public static gameman Instance
    {
        get
        {
            return instance;
        }
    }

    #region Monobehavior Methods

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        Debug.Log("ACCESS_TOKEN : " + PlayerPrefs.HasKey("access_token"));

    }

    private void Start()
    {
        if (!Backend.IsInitialized)
        {
            //뒤끝 초기화
            Backend.Initialize(BackEndInit);
        }
        else
        {
            BackEndInit();
        }

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
            .Builder()
            .RequestServerAuthCode(false)
            .RequestIdToken()
            .RequestEmail()
            .Build();
        //커스텀된 정보로 GPGS 초기화
        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        //GPGS 시작.
        PlayGamesPlatform.Activate();
        GoogleAuth();

        baaudio.volume = PlayerPrefs.GetFloat(PrefsString.baaudio, 1f);
        sfaudio.volume = PlayerPrefs.GetFloat(PrefsString.sfaudio, 1f);
        Screen.fullScreen = !Screen.fullScreen;

    }

    private void Update()
    {
    }
    #endregion
    
    private bool AutoLogin()
    {
        BackendReturnObject obj = Backend.BMember.CheckUserInBackend(GetTokens(), FederationType.Google);
        if (obj.IsSuccess())
        {
            Debug.Log("CheckUser : " + obj.ToString());
            if (obj.GetStatusCode() == "200") { 
                Debug.Log("Is Our User");

                PlayerPrefs.DeleteKey("access_token");

                obj.Clear();
                obj = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google);

                if (obj.IsSuccess())
                {
                    Debug.Log("Auto Login");
                    Usnick();
                }
                else return false;

                return true;
            }
        }

        return false;
    }

    void Usnick() //닉네임이 존재(정상 가입)
    {
        Debug.Log("nicknuck"); //nickname check 함수 진입 표시
        BackendReturnObject obj = Backend.BMember.GetUserInfo();
        if (obj.IsSuccess())
        {
            JsonData data = obj.GetReturnValuetoJSON();

            var type = data["row"]["subscriptionType"];
            Debug.Log(type);

            if (type.ToString() == "customSignUp")
            {
                OnClickGPGSLogin();
                return;
            }
            

            var nickname = data["row"]["nickname"];
            if (nickname == null)
            {
                Debug.Log("User Nickname Did't exisit");
                nicknamebar.SetActive(true);
                PlayerPrefs.DeleteKey(PrefsString.nickname);
            }
            else
            {
                Debug.Log("User Data : " + nickname.ToString());
                userna = nickname.ToString();
                //Player.Instance.AfterLogin(data["row"]["inDate"].ToString());
                MoveMain();
            }
        }
    }

    //닉네임 등록 마치면 씬 전환
    private void MoveMain()
    {
        PlayerPrefs.SetString(PrefsString.nickname, userna);
        SceneManager.LoadScene("02.Main");
    }

    public void BackEndInit() //뒤끝 초기화
    {
        Debug.Log(Backend.Utils.GetServerTime());
    }

    #region 실질 Methods
    private void GoogleAuth()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            Social.localUser.Authenticate(success => {
                if (success == false)
                {
                    Debug.Log("구글 로그인 실패");
                    return; //구글 로그인 실패 시 로그인 거절
                }

                //자동 로그인 함수 (구글 로그인 성공시 실행)
                if (AutoLogin()) Debug.Log("AutoLogin Function Success!!");
                else if (LoginB != null) LoginB.SetActive(true);

                //로그인 성공
                Debug.Log("GetIdToken - " + PlayGamesPlatform.Instance.GetIdToken());
                Debug.Log("Email - " + ((PlayGamesLocalUser)Social.localUser).Email);
                Debug.Log("GoogleId - " + Social.localUser.id);
                Debug.Log("UsetName - " + Social.localUser.userName);
                Debug.Log("UserName - " + PlayGamesPlatform.Instance.GetUserDisplayName());


            });
        }
    }

    // 구글 토큰 받아옴
    private string GetTokens()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // 유저 토큰 받기 첫번째 방법
            string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            // 두번째 방법
            // string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
            Debug.Log(_IDtoken);
            return _IDtoken;
        }
        else
        {
            Debug.Log("접속되어있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
            //재시도
            GoogleAuth();
            return null;
        }
    }

    //구글토큰으로 뒤끝서버 로그인하기 동기방식
    public void OnClickGPGSLogin()
    {
        BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs로 만든 계정");
        if (BRO.IsSuccess())
        {
            Debug.Log("구글 토큰으로 뒤끝서버 로그인 성공 - 동기 방식-");
            //nickname Check from Server
            Usnick();
        }
        else
        {
            switch (BRO.GetStatusCode())
            {
                case "200":
                    Debug.Log("이미 회원가입된 회원");
                    break;
                case "403":
                    Debug.Log("차단된 사용자 입니다. 차단 사유 : " + BRO.GetErrorCode());
                    break;
                default:
                    Debug.Log("서버 공통 에러 발생" + BRO.GetMessage());
                    break;
            }
        }
    }
    #endregion

    #region NickName
    //한글,영어, 숫자만 입력 가능하게
    private bool CheckNickName()
    {
        return Regex.IsMatch(NicknameInput.text, "^[0-9a-zA-z가-힣]*$");
    }

    bool InputFieldEmptyCheck(InputField inputField)
    {
        return inputField != null && !string.IsNullOrEmpty(inputField.text);
    }

    // 닉네임 생성 
    public void CreateNickname()
    {
        Debug.Log("-------------CreateNickname-------------");
        if (InputFieldEmptyCheck(NicknameInput))
        {
            if (CheckNickName() == false)
            {
                Debug.Log("닉네임은 한글, 영어, 숫자");
                return;
            }
            BackendReturnObject BRO = Backend.BMember.CreateNickname(NicknameInput.text);

            if (BRO.IsSuccess())
            {
                Debug.Log("닉네임 생성 완료");
                userna = NicknameInput.text.ToString();
                //Scene 전환
                MoveMain();
            }
            else
            {
                switch (BRO.GetStatusCode())
                {
                    case "409":
                        Debug.Log("이미 중복된 닉네임");
                        break;
                    case "400":
                        if (BRO.GetMessage().Contains("too long"))
                            Debug.Log("20자 이상의 닉네임");
                        else if (BRO.GetMessage().Contains("blank")) Debug.Log("닉네임에 앞/뒤 공백");
                        break;
                    default:
                        Debug.Log("서버 공통 에러 발생: " + BRO.GetErrorCode());
                        break;
                }

            }
        }
        else
        {
            Debug.Log("check NicknameInput");
            return;
        }


    }

    // 닉네임 변경
    public void UpdateNickname()
    {
        Debug.Log("-------------UpdateNickname-------------");
        if (!InputFieldEmptyCheck(NicknameInput))
        {
            Debug.Log("check NicknameInput");
            return;
        }

        if (CheckNickName() == false)
        {
            Debug.Log("닉네임은 한글, 영어, 숫자");
            return;
        }

        BackendReturnObject BRO = Backend.BMember.UpdateNickname(NicknameInput.text); //닉네임 변경

        if (BRO.IsSuccess())
        {
            Debug.Log("닉네임 변경 완료");
        }
        else
        {
            switch (BRO.GetStatusCode())
            {
                case "409":
                    Debug.Log("이미 중복된 닉네임");
                    break;
                case "400":
                    if (BRO.GetMessage().Contains("too long"))
                        Debug.Log("20자 이상의 닉네임");
                    else if (BRO.GetMessage().Contains("blank")) Debug.Log("닉네임에 앞/뒤 공백");
                    break;
                default:
                    Debug.Log("서버 공통 에러 발생: " + BRO.GetErrorCode());
                    break;
            }
        }
    }

    //랭킹 닉네임 사용
    public void InitRank()
    {
        Param lunch = new Param();
        lunch.Add("id", Backend.BMember.CreateNickname(NicknameInput.text).ToString());
        lunch.Add("score", 0);
        BackendReturnObject BRO = Backend.GameSchemaInfo.Insert("rank", lunch);

        if (BRO.IsSuccess())
        {
            Debug.Log("성공");
        }
        else
        {
            if (BRO.IsSuccess())
            {
                Debug.Log("성공공");
            }
            else
            {
                switch (BRO.GetStatusCode())
                {
                    case "404":
                        Debug.Log("존재하지 않는 테이블이름");
                        break;
                    case "412":
                        Debug.Log("비활성화 된 테이블");
                        break;
                    default:
                        Debug.Log("서버 공통 에러 발생" + BRO.GetMessage());
                        break;
                }
            }
        }
    }
    #endregion


    #region Logout Signout
    // 로그아웃
    public void Logout()
    {
        Debug.Log("-------------Logout-------------");
        Debug.Log(Backend.BMember.Logout().ToString());
    }

    // 회원 탈퇴 
    public void SignOut()
    {
        Debug.Log("-------------SignOut-------------");
        Debug.Log(Backend.BMember.SignOut("탈퇴 사유").ToString());
    }
    #endregion

    #region Token Auth

    // 기기에 저장된 뒤끝 AccessToken으로 로그인 (페더레이션, 커스텀 회원가입 또는 로그인 이후에 시도 가능)
    public bool LoginWithTheBackendToken()
    {
        Debug.Log("-------------LoginWithTheBackendToken-------------");
        BackendReturnObject returnObject = Backend.BMember.LoginWithTheBackendToken();
        Debug.Log(returnObject.ToString());
        return returnObject.IsSuccess();
    }


    //뒤끝 RefreshToken 을 통해 뒤끝 AccessToken 을 재발급 받습니다
    public bool RefreshTheBackendToken()
    {
        Debug.Log("-------------RefreshTheBackendToken-------------");
        BackendReturnObject returnObject = Backend.BMember.RefreshTheBackendToken();
        Debug.Log(returnObject.ToString());
        return returnObject.IsSuccess();
    }

    #endregion

 
}

#endif