
using BackEnd;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using LitJson;

namespace TTM.Server{
    
    public static class TableName
    {
        public static string competitions = "competitions";
        public static string competitiondic = "competitiondic";
        //public static string 
    }

    public static class JsonFormatter
    {
        public static void CompetitionFormatter(CompetitionDictionary dic)
        {
            Param param = new Param();
            Dictionary<string, string> paramdic = new Dictionary<string, string>();
            foreach(KeyValuePair<string,Competition> vr in dic)
            {
                paramdic.Add(vr.Key, JsonUtility.ToJson(vr.Value));
                Debug.Log($"key: {vr.Key}, value: {JsonUtility.ToJson(vr.Value)}");
            }

            param.Add("competitiondic", paramdic);
            param.Add("date", System.DateTime.UtcNow.ToString());
            Debug.Log(param.ToString());

            BackendReturnObject list = Backend.GameInfo.GetTableList();
            Debug.Log(list.GetStatusCode());
            if (list.IsSuccess())
            {
                JsonData d = list.GetReturnValuetoJSON();
                foreach(JsonData row in d["publicTables"]) { Debug.Log(row.ToString()); }
            }

            BackendReturnObject insert = Backend.GameInfo.Insert(TableName.competitiondic, param);
            if (insert.IsServerError())
            {
                Debug.Log("Sever Error");
            }
            Debug.Log(insert.ToString());
            if (insert.IsSuccess())
            {
                string Indate = insert.GetInDate();
                Debug.Log("indate : " + Indate);
            }
        }
    }
}
