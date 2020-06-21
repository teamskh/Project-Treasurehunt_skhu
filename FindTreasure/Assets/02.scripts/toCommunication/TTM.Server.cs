
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
                paramdic.Add(vr.Key, JsonMapper.ToJson(vr.Value));
                Debug.Log($"key: {vr.Key}, value: {JsonMapper.ToJson(vr.Value)}");
            }

            param.Add("competitiondic", paramdic);
            param.Add("date", System.DateTime.UtcNow.ToString());
            Debug.Log(param.ToString());

            GetTableList();

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
        public static void GetTableList()
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

        static void SetTable(JsonData data)
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

        static public void GetPublicContents()
        {
            Debug.Log("-----------------Get Public Contents-----------------");

            BackendReturnObject backendReturnObject = Backend.GameInfo.GetPublicContents(TableName.competitiondic, 20);
            Debug.Log(backendReturnObject);
        }

        // 게임정보 한개 가져옴
        static public void GetContentsByIndate()
        {
            JsonData json = Backend.GameInfo.GetPublicContents(TableName.competitiondic).GetReturnValuetoJSON();
            // 데이터가 존재하는지 확인
            if (json["rows"].Count > 0)
            {
                string Indate;
                Indate = json["rows"][0]["inDate"]["S"].ToString();
                Debug.Log("-----------------Get Contents By Indate (public)-----------------");

                BackendReturnObject contents = Backend.GameInfo.GetContentsByIndate(TableName.competitiondic, Indate);
                Debug.Log(contents);
                if (contents.IsSuccess())
                {
                    GetGameInfo(contents.GetReturnValuetoJSON());
                }
            }
            else
            {
                Debug.Log("there is no data");
            }
        }

        static void GetGameInfo(JsonData returnData)
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

        static void GetData(JsonData data)
        {

            string CompetitionKey = "competitiondic";

            // score 라는 key가 존재하는지 확인
            if (data.Keys.Contains(CompetitionKey))
            {
                var JsonDic = data[CompetitionKey]["M"];
                Debug.Log(JsonDic.ToJson());
                CompetitionDictionary dic = JsonMapper.ToObject<CompetitionDictionary>(new JsonReader(JsonDic.ToJson()));
                foreach (var key in dic.Keys) Debug.Log(key);
            }/*
            else
            {
                Debug.Log("there is no key " + scoreKey);
            }
            //Debug.Log("data.Keys.Contains(scoreKey" + data.Keys.Contains(scoreKey));
            // lunch 라는 key가 존재하는지 확인
            if (data.Keys.Contains(lunchKey))
            {
                JsonData lunch = data[lunchKey]["M"];
                var howmuchKey = "how much";
                var whenKey = "when";
                var whatKey = "what";

                if (lunch.Keys.Contains(howmuchKey) && lunch.Keys.Contains(whenKey) && lunch.Keys.Contains(whatKey))
                {
                    var howmuch = lunch[howmuchKey]["N"].ToString();
                    var when = lunch[whenKey]["S"].ToString();
                    var what = lunch[whatKey]["S"].ToString();

                    Debug.Log(when + " " + what + " " + howmuch);
                }
                else
                {
                    Debug.Log("there is no key (" + howmuchKey + " || " + whenKey + " || " + whatKey + ")");
                }
            }
            else
            {
                Debug.Log("there is no key " + lunchKey);
            }

            // list_string 라는 key가 존재하는지 확인
            if (data.Keys.Contains(listKey))
            {
                List<string> returnlist = new List<string>();
                JsonData list = data[listKey]["L"];
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
            else
            {
                Debug.Log("there is no key " + listKey);
            }*/
        }
    }


}
