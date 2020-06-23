using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DataInfo;
using TTM.Path;
using BackEnd;
using TTM.Classes;
using System.Text;

namespace TTM.Save {
    
    public static class JsonLoadSave
    {
        public static string CompetFileName = "compets";
        public static string QuizMadeFileName = "quizmadeof";
        public static string QuizsFileName = "quizz";
        public static string AnswersFileName = "answers";

        #region Save Json
        private static void CreateJsonFile(string createPath, string data)
        {
            FileStream stream = new FileStream(createPath, FileMode.Create);
            byte[] d = Encoding.UTF8.GetBytes(data);
            stream.Write(d, 0, data.Length);
            stream.Close();
        }

        private static void SaveFile(string path, string data)
        {
            //File.WriteAllText(path, data);
            CreateJsonFile(path, data);
            Debug.Log($"File Save : {path}");
        }
        /*
        public static void SaveCompetitions(CompetitionDictionary dic)
        {
            SaveFile(Address.GetComptSavePath(CompetFileName), JsonUtility.ToJson(dic));
        }

        public static void SaveQuizMade(QuizInfoDictionary dic)
        {
            SaveFile(Address.GetQuizMadeSavePath(QuizMadeFileName), JsonUtility.ToJson(dic));
        }*/

        public static void SaveQuizs(QuizDictionary dic)
        {
            SaveFile(Address.GetQuizSavePath(QuizsFileName), JsonUtility.ToJson(dic));
        }

        public static void SaveAnswers(AnswerDictionary dic)
        {
            SaveFile(Address.GetAnswerSavePath(AnswersFileName), JsonUtility.ToJson(dic));
        }
        #endregion


        #region Load Json

        private static string LoadJsonFile(string path)
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            stream.Close();
            return Encoding.UTF8.GetString(data);
        }

        private static bool Load(string path,out string data)
        {
            if (File.Exists(path))
            {
                data = LoadJsonFile(path);
                Debug.Log($"File Exists: {path}\nData: {data}");
                return true;
            }
            else {
                Debug.Log($"File doesn't Exist: {path}");
                data = null;
                return false; 
            }
        }
        /*
        public static bool LoadCompetitions(out CompetitionDictionary dic)
        {
            string data;
            if (Load(Address.GetComptSavePath(CompetFileName),out data))
            {
                //dic = JsonMapper.ToObject<CompetitionDictionary>(new JsonReader(data));
                dic = JsonUtility.FromJson<CompetitionDictionary>(data);
                Debug.Log("LoadCompetition Clear!");
                return true;
                
                
            }
            else
            {
                dic = new CompetitionDictionary();
                return false;
            }
        }

        public static bool LoadQuizMade(out QuizInfoDictionary dic)
        {
            string data;
            if (Load(Address.GetQuizMadeSavePath(QuizMadeFileName), out data))
            {
                dic = JsonUtility.FromJson<QuizInfoDictionary>(data);
                Debug.Log($"LoadQuizMade Clear!\n{dic}");
                return true;
            }
            else
            {
                dic = new QuizInfoDictionary();
                return false;
            }
        }*/

        public static bool LoadQuizs(out QuizDictionary dic)
        {
            string data;
            if (Load(Address.GetQuizSavePath(QuizsFileName), out data))
            {
                dic = JsonUtility.FromJson<QuizDictionary>(data);
                Debug.Log($"LoadQuizz Clear!\nItems: {dic.Count}");
                return true;
            }
            else
            {
                dic = new QuizDictionary();
                return false;
            }
        }

        public static bool LoadAnswers(out AnswerDictionary dic)
        {
            string data;
            if (Load(Address.GetAnswerSavePath(AnswersFileName), out data))
            {
                dic = JsonUtility.FromJson<AnswerDictionary>(data);
                Debug.Log("LoadCompetition Clear!");
                return true;
            }
            else
            {
                dic = new AnswerDictionary();
                return false;
            }
        }

        #endregion
        /*
        #region DownLoad Json
        
        public static void DownLoadCompetContents(string uri)
        {
            string teststorage = "test";
            BackendReturnObject returnObject = Backend.GameInfo.GetPrivateContents(teststorage);
            Debug.Log(returnObject);
            if (returnObject.IsSuccess())
            {
                //GetGameInfo(returnObject.GetReturnValuetoJSON(),"Competitions");
                Debug.Log(returnObject.GetReturnValuetoJSON()["rows"].Count);
            }
        }

        static void  GetGameInfo(JsonData returnData,string key)
        {
            if(returnData != null)
            {
                Debug.Log("returnvalue is not null");
                // for the rows 
                if (returnData.Keys.Contains(key))
                {
                    Debug.Log("returnvalue contains rows");
                    JsonData rows = returnData["rows"];
                    for (int i = 0; i < rows.Count; i++)
                    {
                        GetData(rows[i],key);
                    }
                }
                // for an row
                else if (returnData.Keys.Contains("row"))
                {
                    Debug.Log("returnvalue contains row");
                    JsonData row = returnData["row"];

                    GetData(row[0],key);
                }
            }
            else
            {
                Debug.Log("contents has no data");
            }
        }

        static void GetData(JsonData data, string key)
        {
            if (data.Keys.Contains(key)) {
                CompetitionDictionary dic;
                JsonData temp = data[key]["S"];
                dic = JsonUtility.FromJson<CompetitionDictionary>(temp.ToString());
                foreach (KeyValuePair<string, Competition> pair in dic) {
                    Debug.Log($"key: {pair.Key}");
                }
            }
        }
        
        #endregion*/
    }

    public static class PrefsString
    {
        public static string ID = "ID";
        public static string baaudio = "backvol";
        public static string sfaudio = "sfxvol";
        public static string nickname = "nickna";
        public static string CompetitionName = "Name";
        public static string PersonalScore = "Score";
        public static string LastTime = "Times";
        //public static string ;
    }
}
