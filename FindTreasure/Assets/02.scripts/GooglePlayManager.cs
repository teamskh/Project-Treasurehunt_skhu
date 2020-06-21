using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Text UI 사용
using UnityEngine.UI;
// 구글 플레이 연동
using GooglePlayGames;
using GooglePlayGames.BasicApi;

using BackEnd;
using static BackEnd.BackendAsyncClass;

using System.Text.RegularExpressions;


#if UNITY_ANDROID

public class GooglePlayManager : MonoBehaviour
{
    public bool isSuccess = false;
    BackendReturnObject bro = new BackendReturnObject();

    [SerializeField]
    private InputField NicknameInput;

    // Use this for initialization
    void Start()
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

        /*
        if (!Backend.Utils.GetGoogleHash().Equals(""))
            Debug.Log(Backend.Utils.GetGoogleHash());
        */

    }

    public void BackEndInit() //뒤끝 초기화
    {
        Debug.Log(Backend.Utils.GetServerTime());
    }

    void Update()
    {
        // if (isSuccess)
        // {
        //     Debug.Log("-------------Update(SaveToken)-------------");
        //     BackendReturnObject saveToken = Backend.BMember.SaveToken(bro);
        //     if (saveToken.IsSuccess())
        //     {
        //         Debug.Log("로그인 성공");
        //     }
        //     else
        //     {
        //         Debug.Log("로그인 실패: " + saveToken.ToString());
        //     }
        //     isSuccess = false;
        //     bro.Clear();
        // }
    }

    #region Login Methods
    public void GPGSLogin()
    {
        Debug.Log("-------------GPGS-------------");
        GoogleLogin(false, false);
    }

    public void UpdateFederationEmail()
    {
        Debug.Log("-------------UpdateFederationEmail-------------");
        BackendReturnObject bro = Backend.BMember.UpdateFederationEmail(GetTokens(), FederationType.Google);
        Debug.Log(bro);
    }

    public void AGPGSLogin()
    {
        Debug.Log("-------------A GPGS-------------");
        GoogleLogin(true, false);
    }

    public void ChangeCustomToFederation()
    {
        Debug.Log("-------------ChangeCustomToFederation(GPGS)-------------");
        GoogleLogin(false, true);
    }

    public void AChangeCustomToFederation()
    {
        Debug.Log("-------------AChangeCustomToFederation(GPGS)-------------");
        GoogleLogin(true, true);
    }
    #endregion

    #region 실질 Methods
    private void GoogleLogin(bool async, bool changeFed)
    {
        // 이미 로그인 된 경우
        if (Social.localUser.authenticated == true)
        {
            BackendAuthorize(async, changeFed);
        }
        else
        {
            Social.localUser.Authenticate((success, errorMessage) =>
            {
                if (success)
                {
                    // 로그인 성공 -> 뒤끝 서버에 획득한 구글 토큰으로 가입요청
                    BackendAuthorize(async, changeFed);
                }
                else
                {
                    // 로그인 실패
                    Debug.Log("Login failed for some reason\n" + errorMessage);
                }
            });
        }
    }

    private void BackendAuthorize(bool async, bool changeFed)
    {
        // 커스텀 -> 페더레이션 변경
        if (changeFed)
        {
            // 비동기
            if (async)
            {
                BackendAsync(Backend.BMember.ChangeCustomToFederation, GetTokens(), FederationType.Google, isComplete =>
                {
                    Debug.Log(isComplete.ToString());
                });
            }
            // 동기
            else
            {
                BackendReturnObject BRO = Backend.BMember.ChangeCustomToFederation(GetTokens(), FederationType.Google);
                Debug.Log(BRO);
            }
        }
        // 페더레이션 인증
        else
        {
            // 비동기
            if (async)
            {
                // AuthorizeFederation 대신 AuthorizeFederationAsync 사용
                BackendAsync(Backend.BMember.AuthorizeFederationAsync, GetTokens(), FederationType.Google, "gpgs", callback =>
                {
                    Debug.Log(callback);
                    
                });
            }
            // 동기
            else
            {
                BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google, "gpgs");
                Debug.Log(BRO);
            }
        }
    }

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

    #region Before Method
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
                    gameObject.GetComponent<scenechange>().changeMainScene();
                    PlayerPrefs.SetString("id", Social.localUser.userName);
                }
                else
                {
                    Debug.Log("Fail");
                }
            });
        }
    }

    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
    #endregion

    #region Logout Signout
    // 서버에서 뒤끝 access_token과 refresh_token을 삭제
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
        /*if (InputFieldEmptyCheck(NicknameInput))
        {
            Debug.Log(Backend.BMember.CreateNickname(NicknameInput.text).ToString());
        }
        else
        {
            Debug.Log("check NicknameInput");
            return;
        }*/

        if (CheckNickName() == false)
        {
            Debug.Log("닉네임은 한글, 영어, 숫자");
            return;
        }

        BackendReturnObject BRO = Backend.BMember.CreateNickname(NicknameInput.text);

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

    // 닉네임 변경
    public void UpdateNickname()
    {
        Debug.Log("-------------UpdateNickname-------------");
        /*if (InputFieldEmptyCheck(NicknameInput))
        {
            Debug.Log(Backend.BMember.CreateNickname(NicknameInput.text).ToString());
        }
        else
        {
            Debug.Log("check NicknameInput");
            return;
        }
        */

        if(CheckNickName() == false)
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
    #endregion

    #region Token Auth

    // 기기에 저장된 뒤끝 AccessToken으로 로그인 (페더레이션, 커스텀 회원가입 또는 로그인 이후에 시도 가능)
    public void LoginWithTheBackendToken()
    {
        Debug.Log("-------------LoginWithTheBackendToken-------------");
        Debug.Log(Backend.BMember.LoginWithTheBackendToken().ToString());
    }

    public void ALoginWithTheBackendToken()
    {
        Debug.Log("-------------ALoginWithTheBackendToken-------------");
        // LoginWithTheBackendToken 대신 LoginWithTheBackendTokenAsync 사용
        BackendAsync(Backend.BMember.LoginWithTheBackendTokenAsync, isComplete =>
        {
            // 성공시 - Update() 문에서 토큰 저장
            Debug.Log(isComplete.ToString());
            isSuccess = isComplete.IsSuccess();
            bro = isComplete;
        });
    }


    //뒤끝 RefreshToken 을 통해 뒤끝 AccessToken 을 재발급 받습니다
    public void RefreshTheBackendToken()
    {
        Debug.Log("-------------RefreshTheBackendToken-------------");
        Debug.Log(Backend.BMember.RefreshTheBackendToken().ToString());
    }

    public void ARefreshTheBackendToken()
    {
        Debug.Log("-------------ARefreshTheBackendToken-------------");
        // RefreshTheBackendToken 대신 RefreshTheBackendTokenAsync 사용
        BackendAsync(Backend.BMember.RefreshTheBackendTokenAsync, isComplete =>
        {
            // 성공시 - Update() 문에서 토큰 저장
            Debug.Log(isComplete.ToString());
            isSuccess = isComplete.IsSuccess();
            bro = isComplete;
        });
    }

    #endregion
}

#endif