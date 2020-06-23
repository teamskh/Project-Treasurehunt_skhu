
using BackEnd;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using LitJson;
using DataInfo;
using UnityEditor.UIElements;
using System.Collections;
using System;

namespace TTM.Server {

    public static class TableName
    {
        public static string competitions = "competitions";
        public static string competitiondic = "competitiondic";
        public static string quizmadedic = "quizmadedic";
        public static string quizplayerdic = "quizplayerdic";
        public static string answerdic = "answerdic";
        //public static string 
    }

    public static class JsonFormatter
    {
        #region Fomatter
        public static Param CompetitionFormatter(CompetitionDictionary dic, int version)
        {
            Param param = new Param();
            Dictionary<string, string> paramdic = new Dictionary<string, string>();
            List<string> list = new List<string>();

            foreach (KeyValuePair<string, Competition> vr in dic)
            {
                paramdic.Add(vr.Key, JsonMapper.ToJson(vr.Value));
                list.Add(vr.Key);
                Debug.Log($"key: {vr.Key}, value: {JsonMapper.ToJson(vr.Value)}");
            }

            param.Add(TableName.competitiondic, paramdic);
            param.Add("rowkeys", list.ToArray());
            param.Add("version", version);
            Debug.Log(param.ToString());

            return param;
        }

        public static Param QuizMadeFormatter(QuizInfoDictionary dic, int version)
        {
            Param param = new Param();
            Dictionary<string, string> paramdic = new Dictionary<string, string>();
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, QuizInfo> vr in dic)
            {
                paramdic.Add(vr.Key, JsonMapper.ToJson(vr.Value));
                list.Add(vr.Key);
                Debug.Log($"key: {vr.Key}, value: {JsonMapper.ToJson(vr.Value)}");
            }

            param.Add(TableName.quizmadedic, paramdic);
            param.Add("rowkeys", list.ToArray());
            param.Add("version", version);
            Debug.Log(param.ToString());

            return param;
        }

        public static Param QuizPlayerFormatter(QuizDictionary dic, int version)
        {
            Param param = new Param();
            Dictionary<string, string> paramdic = new Dictionary<string, string>();
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, Quiz> vr in dic)
            {
                paramdic.Add(vr.Key, JsonMapper.ToJson(vr.Value));
                list.Add(vr.Key);
                Debug.Log($"key: {vr.Key}, value: {JsonMapper.ToJson(vr.Value)}");
            }

            param.Add(TableName.quizplayerdic, paramdic);
            param.Add("rowkeys", list.ToArray());
            param.Add("version", version);
            Debug.Log(param.ToString());

            return param;
        }

        public static Param AnswerFormatter(AnswerDictionary dic , int version)
        {

            Param param = new Param();
            Dictionary<string, string> paramdic = new Dictionary<string, string>();
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, Answer> vr in dic)
            {
                paramdic.Add(vr.Key, JsonMapper.ToJson(vr.Value));
                list.Add(vr.Key);
                Debug.Log($"key: {vr.Key}, value: {JsonMapper.ToJson(vr.Value)}");
            }

            param.Add(TableName.answerdic, paramdic);
            param.Add("rowkeys", list.ToArray());
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

        protected AnswerDictionary answerdic;
        protected int Aversion;
        protected bool AIsUpdate;

        #region Load Data
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

            string CompetitionKey = TableName.competitiondic;
            string QuizMadeKey = TableName.quizmadedic;
            string QuizPlayerKey = TableName.quizplayerdic;
            string AnswerKey = TableName.answerdic;
            string VersionKey = "version";

            List<string> returnlist = new List<string>();
            if (data.Keys.Contains("rowkeys"))
            {
                JsonData list = data["rowkeys"]["L"];
                var listCount = list.Count;
                if (listCount > 0)
                {
                    for (int j = 0; j < listCount; j++)
                    {
                        var listdata = list[j]["S"].ToString();
                        returnlist.Add(listdata);
                    }
                    Debug.Log(JsonMapper.ToJson(returnlist));
                }
                else
                {
                    Debug.Log("list has no data");
                }
            }

            int version;


            if (data.Keys.Contains(VersionKey))
            {
                var jsonVer = data[VersionKey]["N"].ToString();

               version = int.Parse(jsonVer);
            }
            else { version = 0; }


            if (data.Keys.Contains(CompetitionKey))
            {
                CIsUpdate = true;
                var JsonDic = data[CompetitionKey]["M"];
                Debug.Log(JsonDic.ToJson());

                foreach (var key in returnlist)
                {
                    string jsonclass = JsonDic[key]["S"].ToString();
                    competdic.Add(key, JsonMapper.ToObject<Competition>(new JsonReader(jsonclass)));
                }

                Cversion = version;
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

                foreach (var key in returnlist)
                {
                    string jsonclass = JsonDic[key]["S"].ToString();
                    quizdic.Add(key, JsonMapper.ToObject<QuizInfo>(new JsonReader(jsonclass)));
                }

                Qversion = version;
            }
            else
            {
                Debug.Log("there is no key " + QuizMadeKey);
            }

            if (data.Keys.Contains(QuizPlayerKey))
            {
                PIsUpdate = true;
                var JsonDic = data[QuizPlayerKey]["M"];
                Debug.Log(JsonDic.ToJson());

                foreach (var key in returnlist)
                {
                    string jsonclass = JsonDic[key]["S"].ToString();
                    quizplayerdic.Add(key, JsonMapper.ToObject<Quiz>(new JsonReader(jsonclass)));
                }

                Pversion = version;
            }
            else
            {
                Debug.Log("there is no key " + QuizPlayerKey);
            }

            if (data.Keys.Contains(AnswerKey))
            {
                AIsUpdate = true;
                var JsonDic = data[AnswerKey]["M"];
                Debug.Log(JsonDic.ToJson());

                foreach (var key in returnlist)
                {
                    string jsonclass = JsonDic[key]["S"].ToString();
                    answerdic.Add(key, JsonMapper.ToObject<Answer>(new JsonReader(jsonclass)));
                }

                Aversion = version;
            }
            else
            {
                Debug.Log("there is no key " + QuizPlayerKey);
            }

        }

        #endregion

        #region Call Methods

        public CompetitionDictionary CallCompetDic() { return competdic; }

        protected void CompetitionCommunication(CompetitionDictionary dic)
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
        
        protected void QuizMadeCommunication(QuizInfoDictionary dic)
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

        protected void QuizPlayerCommunication(QuizDictionary dic) {
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
        public AnswerDictionary CallAnswerDic() { return answerdic; }

        protected void AnswerCommunication(AnswerDictionary dic)
        {
            Aversion++;
            Param p = JsonFormatter.AnswerFormatter(dic, Pversion);
            if (!AIsUpdate)
            {
                ServerFunc.DataInsert(p, TableName.answerdic);
            }
            else
            {
                ServerFunc.DataUpdate(p, TableName.answerdic);
            }
        }
        #endregion

    }


}
