#if UNITY_ANDROID
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTM.Save;
using DataInfo;
using TTM.Classes;
using BackEnd;

using GooglePlayGames;
using GooglePlayGames.BasicApi;

using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using TTM.Server;

public class gameman : GameDataFunction
{
    public AudioSource baaudio;
    public AudioSource sfaudio;
    public int score = 0;
    public string imageText; //문제,답 내용 결정

    public GameObject nicknamebar;

    public string userna;
    //페이지 이동시 저장될 유저이름
    //playerdic;
    //public bool chek = false;

    public string time;

    public bool isSuccess = false;
    BackendReturnObject bro = new BackendReturnObject();

    [SerializeField]
    private InputField NicknameInput;

    bool chek = false; //정상 로그인 - 컨텐츠 로드 완료
    
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
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
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
        //GoogleAuth();

        baaudio.volume = PlayerPrefs.GetFloat(PrefsString.baaudio, 1f);
        sfaudio.volume = PlayerPrefs.GetFloat(PrefsString.sfaudio, 1f);
        Screen.fullScreen = !Screen.fullScreen;

        isSuccess = LoginWithTheBackendToken();
        if (!isSuccess)
            if (RefreshTheBackendToken())
                isSuccess = LoginWithTheBackendToken();
        
    }

    private void Update()
    {
        if (isSuccess)
        {
            Debug.Log("-------------Update(SaveToken)-------------");
            BackendReturnObject saveToken = Backend.BMember.SaveToken(bro);
            if (saveToken.IsSuccess())
            {
                Debug.Log("로그인 성공");
                GetContentsByIndate(TableName.competitiondic);
                GetContentsByIndate(TableName.quizplayerdic);
                GetContentsByIndate(TableName.answerdic);
                chek = true;
            }
            else
            {
                Debug.Log("로그인 실패: " + saveToken.ToString());
            }
            isSuccess = false;
            bro.Clear();
        }
        if (chek) Usnick();
    }
    #endregion


    void Usnick() //닉네임이 존재(정상 가입)
    {
        Debug.Log("nicknuck");
        if (PlayerPrefs.HasKey(PrefsString.nickname))
        {
            Debug.Log("yes");
            SceneManager.LoadScene("02.Main");
        }
        else
        {
            nicknamebar.SetActive(true);
            Debug.Log("nono");
        }
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
                    return;
                }

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
            BackendReturnObject saveToken = Backend.BMember.SaveToken(BRO);
            if (saveToken.IsSuccess())
            {
                GetContentsByIndate(TableName.competitiondic);
                GetContentsByIndate(TableName.quizplayerdic);
                GetContentsByIndate(TableName.answerdic);
                chek = true;
            }
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

    public void CheckNicknameDuplication()
    {
        Debug.Log("-------------CheckNicknameDuplication-------------");
        if (InputFieldEmptyCheck(NicknameInput))
        {
            Debug.Log(Backend.BMember.CheckNicknameDuplication(NicknameInput.text).ToString());
        }
        else
        {
            Debug.Log("check NicknameInput");
        }
    }

    // 닉네임 생성 
    public void CreateNickname()
    {
        Debug.Log("-------------CreateNickname-------------");
        if (InputFieldEmptyCheck(NicknameInput))
        {
            Debug.Log(Backend.BMember.CreateNickname(NicknameInput.text).ToString());
        }
        else
        {
            Debug.Log("check NicknameInput");
            return;
        }

        if (CheckNickName() == false)
        {
            Debug.Log("닉네임은 한글, 영어, 숫자");
            return;
        }

        BackendReturnObject BRO = Backend.BMember.CreateNickname(NicknameInput.text);
        
        userna = NicknameInput.text.ToString();

        PlayerPrefs.SetString(PrefsString.nickname, userna);

        if (BRO.IsSuccess())
        {
            Debug.Log("닉네임 생성 완료");
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

    public void Logout()
    {
        Debug.Log("-------------Logout-------------");
        Debug.Log(Backend.BMember.Logout().ToString());
    }

    // 닉네임 변경
    public void UpdateNickname()
    {
        Debug.Log("-------------UpdateNickname-------------");
        if (InputFieldEmptyCheck(NicknameInput))
        {
            Debug.Log(Backend.BMember.CreateNickname(NicknameInput.text).ToString());
        }
        else
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

    #region 퀴즈용
    public Answer CheckAnswer()
    {
        Answer ans = AnswerDictionary.GetAnswer(imageText);
        return ans;
    }

    public Quiz FindQuiz() {
        return quizplayerdic.FindQuiz(imageText);
    }

    public void Load()
    {
        userna = PlayerPrefs.GetString(PrefsString.nickname);
    }

    public List<string> GetList()
    {
        return competdic.getCompetitionList();
    }
    #endregion
}

#endif