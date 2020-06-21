using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using serializeDic;
using UnityEngine;
using TTM.Classes;
using TTM.Save;

namespace DataInfo
{
    
    [System.Serializable]
    public class QuizInfoDictionary : SerializableDictionary<string,QuizInfo>
    {
        protected QuizInfoDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public QuizInfoDictionary() { }

        public List<string> GetList()
        {
            List<string> list = new List<string>();
            foreach (var key in this.Keys)
            {
                list.Add(key);
            }
            return list;
        }

        public new string ToString() {
            string Info = "";
            Info += $"Items : {this.Count}\n";
            int i = 0;
            foreach(KeyValuePair<string,QuizInfo> vr in this)
            {
                Info += $"{i++}: {vr.Key} - {vr.Value}\n";
            }
            return Info;
        }
    }

    [System.Serializable]
    public class QuizDictionary : SerializableDictionary<string, Quiz>
    {
        public void GetList(QuizInfoDictionary basedictionary)
        {
            foreach (KeyValuePair<string, QuizInfo> k in basedictionary)
            {
                Quiz value = new Quiz();
                value.Str = k.Value.Str;
                value.Kind = k.Value.Kind;
                if (value.Kind == 1)
                {
                    value.List = new string[4];
                    for (int i = 0; i < 4; i++)
                    {
                        value.List[i] = k.Value.List[i];
                    }
                }
                this.Add(k.Key, value);
            }
        }
        public Quiz FindQuiz(string key)
        {
            Quiz quiz = new Quiz();
            if (!this.ContainsKey(key)) Debug.Log("Can't find Key");
            if (this.TryGetValue(key, out quiz))
            {
                return quiz;
            }
            else
            {
                Debug.Log("Can't Find Item");
                return null;
            }
        }
        protected QuizDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public QuizDictionary() { }
    }

    [System.Serializable]
    public class AnswerDictionary : SerializableDictionary<string, Answer>
    {
        public void GetList(QuizInfoDictionary basedictionary)
        {
            var keys = basedictionary.Keys;
            var values = basedictionary.Values;

            foreach (KeyValuePair<string, QuizInfo> k in basedictionary)
            {
                Answer value = new Answer();
                value.Score = k.Value.Score;
                value.Kind = k.Value.Kind;
                switch (value.Kind)
                {
                    case 0:
                        value.Banswer = k.Value.Banswer;
                        break;
                    case 1:
                        value.Ianswer = k.Value.Ianswer;
                        break;
                    case 2:
                        value.Wanswer = k.Value.Wanswer;
                        break;
                }

                this.Add(k.Key, value);
            }
        }

        private static bool LoadAnswers(out Answer answer,string key)
        {
            AnswerDictionary dic;
            if (JsonLoadSave.LoadAnswers(out dic))
            {
                if (dic.TryGetValue(key, out answer))
                {
                    Debug.Log($"Find Answer : {key}");
                    return true;
                }
                else
                    Debug.Log($"Can't Find Answer : {key}");
            }
            answer = null;
            return false;
        }
        public static Answer GetAnswer(string key)
        {
            Answer ans = new Answer();
            if (!LoadAnswers(out ans, key)){
                Debug.Log($"Can't Find Answer : {key}");
            }
            return ans;

        }
        protected AnswerDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public AnswerDictionary() { }
    }
}