
using BackEnd;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using LitJson;
using DataInfo;

namespace TTM.Server {

    public static class TableName
    {
        public static string competitions = "competitions";
        public static string competitiondic = "competitiondic";
        public static string quizmadedic = "quizmadedic";
        public static string quizplayerdic = "quizplayerdic";
        //public static string 
    }

    public static class JsonFormatter
    {
        #region Fomatter
        public static Param CompetitionFormatter(CompetitionDictionary dic, int version)
        {
            Param param = new Param();
            Dictionary<string, string> paramdic = new Dictionary<string, string>();
            foreach (KeyValuePair<string, Competition> vr in dic)
            {
                paramdic.Add(vr.Key, JsonMapper.ToJson(vr.Value));
                Debug.Log($"key: {vr.Key}, value: {JsonMapper.ToJson(vr.Value)}");
            }

            param.Add(TableName.competitiondic, paramdic);
            param.Add("version", version);
            Debug.Log(param.ToString());

            return param;
        }

        public static Param QuizMadeFormatter(QuizInfoDictionary dic, int version)
        {
            Param param = new Param();
            Dictionary<string, string> paramdic = new Dictionary<string, string>();
            foreach (KeyValuePair<string, QuizInfo> vr in dic)
            {
                paramdic.Add(vr.Key, JsonMapper.ToJson(vr.Value));
                Debug.Log($"key: {vr.Key}, value: {JsonMapper.ToJson(vr.Value)}");
            }

            param.Add(TableName.quizmadedic, paramdic);
            param.Add("version", version);
            Debug.Log(param.ToString());

            return param;
        }

        public static Param QuizPlayerFormatter(QuizDictionary dic, int version)
        {
            Param param = new Param();
            Dictionary<string, string> paramdic = new Dictionary<string, string>();
            foreach (KeyValuePair<string, Quiz> vr in dic)
            {
                paramdic.Add(vr.Key, JsonMapper.ToJson(vr.Value));
                Debug.Log($"key: {vr.Key}, value: {JsonMapper.ToJson(vr.Value)}");
            }

            param.Add(TableName.quizplayerdic, paramdic);
            param.Add("version", version);
            Debug.Log(param.ToString());

            return param;
        }
        #endregion

    }

    public static class ServerFunc
    {
        static string QIndate;
        static string CIndate;
        static string PIndate;
        static string Indate;

        public static string InDate(string tablename, string indate = null, bool IsSet = true)
        {
            if (IsSet)
            {
                Indate = indate;
                SetIndate(tablename);
                return null;
            }
            else
            {
                GetIndate(tablename);
                return Indate;
            }
        }
        static void GetIndate(string tablename)
        {
            if (TableName.competitiondic == tablename)
                Indate = CIndate;
            else if (TableName.quizmadedic == tablename)
                Indate = QIndate;
            else if (TableName.quizplayerdic == tablename)
                Indate = PIndate;
        }

        static void SetIndate(string tablename)
        {
            if (TableName.competitiondic == tablename)
                CIndate = Indate;
            else if (TableName.quizmadedic == tablename)
                QIndate = Indate;
            else if (TableName.quizplayerdic == tablename)
                PIndate = Indate;
        }
        #region Server Funcs
        public static void DataInsert(Param param, string tablename)
        {
            BackendReturnObject insert = Backend.GameInfo.Insert(tablename, param);

            Debug.Log(insert.ToString());
            if (insert.IsSuccess())
            {
                Indate = insert.GetInDate();
                SetIndate(tablename);
                Debug.Log("indate : " + Indate);
            }
            else Indate = null;
        }

        public static void DataUpdate(Param param, string tablename)
        {
            GetIndate(tablename);
            BackendReturnObject update = Backend.GameInfo.Update(tablename, Indate, param);

            Debug.Log(update.ToString());
            if (update.IsSuccess())
            {
                Debug.Log("indate : " + Indate);
            }
        }

        #endregion
    }
   

    public class GameDataFunction : MonoBehaviour { 
        protected string Indate;

        protected CompetitionDictionary competdic;
        protected int Cversion;
        protected bool CIsUpdate;

        protected QuizInfoDictionary quizdic;
        protected int Qversion;
        protected bool QIsUpdate;

        protected QuizDictionary quizplayerdic;
        protected int Pversion;
        protected bool PIsUpdate;

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

                if (tablename == TableName.quizmadedic)
                    quizdic = new QuizInfoDictionary();
                Debug.Log("there is no data");
            }
        }

        void GetGameInfo(JsonData returnData)
        {
            // ReturnValue가 존재하고, 데이터가 있는지 확인
            if (returnData != null)
            {
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
                CIsUpdate = true;
                var JsonDic = data[CompetitionKey]["M"];
                Debug.Log(JsonDic.ToJson());
                competdic = JsonMapper.ToObject<CompetitionDictionary>(new JsonReader(JsonDic.ToJson()));

                foreach (var key in competdic.Keys) Debug.Log(key);

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

        public CompetitionDictionary CallCompetDic() { return competdic; }

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

        public QuizInfoDictionary CallQuizmadeDic() { return quizdic; }
        
        public void QuizMadeCommunication(QuizInfoDictionary dic)
        {
            Qversion++;
            Param p = JsonFormatter.QuizMadeFormatter(dic, Qversion);
            if (!QIsUpdate)
            {
                ServerFunc.DataInsert(p, TableName.quizmadedic);
            }
            else
            {
                ServerFunc.DataUpdate(p, TableName.quizmadedic);
            }
        }

        public QuizDictionary CallQuizplayerDic() { return quizplayerdic; }

        public void QuizPlayerCommunication(QuizDictionary dic) {
            Pversion++;
            Param p = JsonFormatter.QuizPlayerFormatter(dic, Pversion);
            if (!PIsUpdate)
            {
                ServerFunc.DataInsert(p, TableName.quizplayerdic);
            }
            else
            {
                ServerFunc.DataUpdate(p, TableName.quizplayerdic);
            }
        }
        #endregion

    }


}
