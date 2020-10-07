using System;
using BackEnd;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TTM.Classes;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static bool isLogin = false;
    public static bool LoginKind = false;

    Dictionary<string,PlayerGameLog> Log = new Dictionary<string, PlayerGameLog>();
    Dictionary<int, string> Answers = new Dictionary<int, string>();
    List<ShortInfo> shortInfos = new List<ShortInfo>();
    List<Recodes.Recode> recodes = new List<Recodes.Recode>();
    public int CurComp;
    public List<string> clearlist;

    static event Action Save;
    static event Action Load;

    string userCode;
    public int score;

#region Singleton
    static Player instance;

    public static Player Instance
    {
        get
        {
            Save();
            return instance;
        }
    }

    private void Awake()
    {
        Load = () => Debug.Log("Load() : Before Login");
        Save = () => Debug.Log("Save() : Before Login");
        if (instance == null)
        {
            instance = this;
            Load();
        }
    }
#endregion

    private void AfterLogin()
    {
        userCode = PlayerPrefs.GetString("usercode");
        Save = () => Log.Save(userCode);//shortInfos.Save(userCode);
        Save += () => recodes.Save(userCode, "recode");

        Load = () => shortInfos.Load(userCode);
        Load += () => recodes.Load(userCode, "recode");
        
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
        score = 0;
        clearlist = new List<string>();
    }

    void Update( )
    {
        
    }

    public void UpdateUserCompets(ShortInfo shortInfo)
    {
        foreach(var Short in shortInfos)
        {
            if(Short.ConName == shortInfo.ConName)
            {
                Short.UpdateStartTime(shortInfo.StartTime);
                Short.UpdateEndingTime(shortInfo.EndingTime);
                return;
            }
        }

        shortInfos.Add(shortInfo);
    }

    void FinishCompets(string competname)
    {
        shortInfos.Remove(shortInfos.Find(competname));
        PlayerGameLog item;
        Log.TryGetValue(competname, out item);
        if (item != null)
        {
            recodes.Add(item.Summary());
            Log.Remove(competname);
        }
        Save();
    }
#region Answers
        
    private IEnumerator CheckAns(string name,string ans)
    {
        int code = -1;
        PlayerContents.Instance.FindQ(name, out code);
        if (code >= 0)
            Answers.Add(code, ans);
        yield return null;

        foreach (KeyValuePair<int, string> pair in Answers) {
            int score = PlayerContents.Instance.CheckAnswer(pair);
            if (score < 0)
            {
                Debug.Log("Wrong");
            }
            else 
            {
                Answers.Remove(pair.Key);
                this.score += score;
                ReadScore.CallUpdate();
                
            }
            clearlist.Add(name);
            PlayerContents.Instance.repackageLib();
            TrackedImageInfoManager.CallDestroy(name);
        }
    }
    public void CheckAnswer(string name, string ans)
    {
        StartCoroutine(CheckAns(name, ans));
    }
#endregion
}
