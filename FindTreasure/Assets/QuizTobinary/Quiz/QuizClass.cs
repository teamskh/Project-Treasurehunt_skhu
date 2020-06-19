using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using serializeDic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class QuizInfo
    {
        public string str;
        public int score;
        public int kind;
        public string[] list;
        public bool Banswer;
        public int Ianswer;
        public string Wanswer;
    }

    [System.Serializable]
    public class Quiz
    {
        public string str;
        public int kind;
        public string[] list;
    }

    [System.Serializable]
    public class Answer
    {
        public int kind;
        public int score;
        public bool Banswer;
        public int Ianswer;
        public string Wanswer;
    }



    [System.Serializable]
    public class QuizInfoDictionary : SerializableDictionary<string, QuizInfo>
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

    }

    [System.Serializable]
    public class QuizDictionary : SerializableDictionary<string, Quiz>
    {
        public void GetList(QuizInfoDictionary basedictionary)
        {
            foreach (KeyValuePair<string, QuizInfo> k in basedictionary)
            {
                Quiz value = new Quiz();
                value.str = k.Value.str;
                value.kind = k.Value.kind;
                if (value.kind == 1)
                {
                    value.list = new string[4];
                    for (int i = 0; i < 4; i++)
                    {
                        value.list[i] = k.Value.list[i];
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
                value.score = k.Value.score;
                value.kind = k.Value.kind;

                switch (value.kind)
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

        public Answer GetAnswer(string key)
        {
            Answer ans = new Answer();
            if (this.TryGetValue(key, out ans))
            {
                return ans;
            }
            else
                return null;
        }
        protected AnswerDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public AnswerDictionary() { }
    }
}