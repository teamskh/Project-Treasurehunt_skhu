
using BackEnd;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using LitJson;
using DataInfo;

namespace TTM.Server{
    
    public static class TableName
    {
        public static string competitions = "competitions";
        public static string competitiondic = "competitiondic";
        public static string quizmadedic = "quizmadedic";
        //public static string 
    }

    public static class JsonFormatter
    {
        #region Fomatter
        public static Param CompetitionFormatter(CompetitionDictionary dic,int version)
        {
            Param param = new Param();
            Dictionary<string, string> paramdic = new Dictionary<string, string>();
            foreach(KeyValuePair<string,Competition> vr in dic)
            {
                paramdic.Add(vr.Key, JsonMapper.ToJson(vr.Value));
                Debug.Log($"key: {vr.Key}, value: {JsonMapper.ToJson(vr.Value)}");
            }

            param.Add("competitiondic", paramdic);
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

            param.Add("competitiondic", paramdic);
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
        static string Indate;

        public static string InDate(string tablename,string indate = null, bool IsSet = true)
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
        }
        
        static void SetIndate(string tablename)
        {
            if (TableName.competitiondic == tablename)
                CIndate = Indate;
            else if (TableName.quizmadedic == tablename)
                QIndate = Indate;
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


}
