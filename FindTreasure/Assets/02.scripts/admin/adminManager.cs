using DataInfo;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using TTM.Server;
using LitJson;
using TTM.Classes;

public class adminManager : GameDataFunction
{
    #region statics
    static adminManager instance;

    public static adminManager Instance
    {
        get { return instance; }
    }

    #endregion
    public static bool isSet = false;

    string id = "Admin";
    string pw = "toomuch";

    BackendReturnObject bro = new BackendReturnObject();
    bool isSuccess = false;
    Competition com = new Competition();

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
        
        Indate = null;
        
        Backend.Initialize(BRO =>
        {
            Debug.Log("Backend.Initialize " + BRO);
            // 성공
            if (BRO.IsSuccess())
            {
                Backend.BMember.CustomLogin(id, pw, callback =>
                {
                    Debug.Log("CustomLogin " + callback);
                    isSuccess = callback.IsSuccess();
                    bro = callback;
                });
            }
            // 실패
            else
            {
                Debug.LogError("Failed to initialize the backend");
            }
        });
    }
    // Update is called once per frame
    void Update()
    {
        if (isSuccess)
        {
            Debug.Log("-------------Update(SaveToken)-------------");
            BackendReturnObject saveToken = Backend.BMember.SaveToken(bro);
            if (saveToken.IsSuccess())
            {
                Debug.Log("로그인 성공");

                GetContentsByIndate(TableName.competitiondic);
                GetContentsByIndate(TableName.quizmadedic);
                GetContentsByIndate(TableName.quizplayerdic);
                GetContentsByIndate(TableName.answerdic);
                GetComponent<CompetDic>().isSet = true;
            }
            else
            {
                Debug.Log("로그인 실패: " + saveToken.ToString());
            }
            isSuccess = false;
            bro.Clear();
        }

    }

    public bool setComp(string key)
    {
        return competdic.TryGetValue(key,out  com);
    }

    #region Don't Need Now
    public void GetTableList()
    {
        Debug.Log("-----------------Get Table List-----------------");
        BackendReturnObject tablelist = Backend.GameInfo.GetTableList();
        Debug.Log(tablelist);

        if (tablelist.IsSuccess())
        {
            SetTable(tablelist.GetReturnValuetoJSON());
        }
    }

    static List<string> PublicTables = new List<string>();
    static List<string> PrivateTables = new List<string>();

    void SetTable(JsonData data)
    {
        JsonData publics = data["publicTables"];
        foreach (JsonData row in publics)
        {
            PublicTables.Add(row.ToString());
        }

        JsonData privates = data["privateTables"];
        foreach (JsonData row in privates)
        {
            PrivateTables.Add(row.ToString());
        }
    }
    #endregion


}
