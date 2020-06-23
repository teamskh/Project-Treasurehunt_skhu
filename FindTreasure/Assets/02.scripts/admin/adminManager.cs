using DataInfo;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using TTM.Server;
using LitJson;
using TTM.Classes;

public class adminManager : MonoBehaviour
{
    #region statics
    static adminManager instance;

    public static adminManager Instance
    {
        get { return instance; }
    }

    #endregion
    public static bool isSet = false;

    CompetitionDictionary competdic;
    QuizInfoDictionary quizdic;
    string id = "Admin";
    string pw = "toomuch";

    BackendReturnObject bro = new BackendReturnObject();
    bool isSuccess = false;
    string Indate;

    int Cversion;
    int Qversion;
    bool CIsUpdate = false;
    bool QIsUpdate = false;

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
    /*
    public void init()
    {
        dic = new CompetitionDictionary();
        Competition comp = new Competition();
        comp.Mode = false;
        comp.Password = "1234";
        dic.Add("Test", comp);
        Debug.Log(comp.ToString());
        JsonFormatter.CompetitionFormatter(dic);
    }*/

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
            }
            else
            {
                Debug.Log("로그인 실패: " + saveToken.ToString());
            }
            isSuccess = false;
            bro.Clear();
        }
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

    public void GetPublicContents()
    {
        Debug.Log("-----------------Get Public Contents-----------------");

        BackendReturnObject backendReturnObject = Backend.GameInfo.GetPublicContents(TableName.competitiondic, 20);
        Debug.Log(backendReturnObject);
    }


    // 게임정보 한개 가져옴
    public void GetContentsByIndate(string tablename)
    {
        JsonData json = Backend.GameInfo.GetPublicContents(tablename).GetReturnValuetoJSON();
        // 데이터가 존재하는지 확인
        if (json["rows"].Count > 0)
        {
            Indate = json["rows"][0]["inDate"]["S"].ToString();
            Debug.Log("-----------------Get Contents By Indate (public)-----------------");

            ServerFunc.InDate(tablename, Indate);

            BackendReturnObject contents = Backend.GameInfo.GetContentsByIndate(tablename, Indate);
            Debug.Log(contents);
            if (contents.IsSuccess())
            {
                GetGameInfo(contents.GetReturnValuetoJSON());
            }
        }
        else
        {
            if (tablename == TableName.competitiondic) 
                competdic = new CompetitionDictionary();
            else if (tablename == TableName.quizmadedic)
                quizdic = new QuizInfoDictionary();
            Debug.Log("there is no data");
        }
    }

    void GetGameInfo(JsonData returnData)
    {
        // ReturnValue가 존재하고, 데이터가 있는지 확인
        if (returnData != null)
        {
            CIsUpdate = true;
            Debug.Log("returnvalue is not null");
            // for the rows 
            if (returnData.Keys.Contains("rows"))
            {
                Debug.Log("returnvalue contains rows");
                JsonData rows = returnData["rows"];

                for (int i = 0; i < rows.Count; i++)
                {
                    GetData(rows[i]);
                }
            }
            // for an row
            else if (returnData.Keys.Contains("row"))
            {
                Debug.Log("returnvalue contains row");
                JsonData row = returnData["row"];

                GetData(row[0]);
            }
        }
        else
        {
            Debug.Log("contents has no data");
        }
    }

    void GetData(JsonData data)
    {

        string CompetitionKey = "competitiondic";
        string QuizMadeKey = "quizmadedic";
        string VersionKey = "version";

        if (data.Keys.Contains(CompetitionKey))
        {
            var JsonDic = data[CompetitionKey]["M"];
            Debug.Log(JsonDic.ToJson());
            
            var dic = JsonMapper.ToObject<Dictionary<string,string>>(new JsonReader(JsonDic.ToJson()));

            foreach(KeyValuePair<string,string> pair in dic)
            {
                Debug.Log(pair.Key + ":" + pair.Value);
                Competition com = JsonMapper.ToObject<Competition>(new JsonReader(pair.Value));
                competdic.Add(pair.Key, com);
            }

            if (data.Keys.Contains(VersionKey))
            {
                var jsonVer = data[VersionKey]["N"];

                Cversion = JsonMapper.ToObject<int>(new JsonReader(jsonVer.ToJson()));
            }
        }
        else
        {
            Debug.Log("there is no key " + CompetitionKey);
        }

        if (data.Keys.Contains(QuizMadeKey))
        {
            QIsUpdate = true;
            var JsonDic = data[QuizMadeKey]["M"];
            Debug.Log(JsonDic.ToJson());
            quizdic = JsonMapper.ToObject<QuizInfoDictionary>(new JsonReader(JsonDic.ToJson()));
            foreach (var key in quizdic.Keys) Debug.Log(key);

            if (data.Keys.Contains(VersionKey))
            {
                var jsonVer = data[VersionKey]["N"];

                Cversion = JsonMapper.ToObject<int>(new JsonReader(jsonVer.ToJson()));
            }
        }
        else
        {
            Debug.Log("there is no key " + QuizMadeKey);
        }

    }

    #region Call Methods

    public void CompetitionCommunication(CompetitionDictionary dic)
    {
        competdic = dic;
        Cversion++;
        Param p = JsonFormatter.CompetitionFormatter(dic, Cversion);
        if (!CIsUpdate)
        {
            ServerFunc.DataInsert(p, TableName.competitiondic);
        }
        else
        {
            ServerFunc.DataUpdate(p, TableName.competitiondic);
        }
    }

    public CompetitionDictionary CallCompetDic() { return competdic; }

    public QuizInfoDictionary CallQuizmadeDic() { return quizdic; }
    public void QuizMadeCommunication(QuizInfoDictionary dic)
    {
        Qversion++;
        Param p = JsonFormatter.QuizMadeFormatter(dic, Qversion);
        if (QIsUpdate)
        {
            ServerFunc.DataInsert(p, TableName.competitiondic);
        }
        else
        {
            ServerFunc.DataUpdate(p, TableName.competitiondic);
        }
    }

    #endregion

}
