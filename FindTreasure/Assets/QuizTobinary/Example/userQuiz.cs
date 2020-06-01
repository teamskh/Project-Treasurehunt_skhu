using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using UnityEngine;
using System.Security.Permissions;
using System.Linq;

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
    public class Contest
    {
        public bool Mode;
        public int MaxMember;
        public string Password;
        public DateTime StartTime;
        public DateTime EndTime;
        public string info;
        public int Userword;
    }

    [System.Serializable]
    public class QuizInfoDictionary : SerializableDictionary<string, QuizInfo>
    {
        protected QuizInfoDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public QuizInfoDictionary() { }
    }

    [System.Serializable]
    public class ContestDictionary : SerializableDictionary<string, Contest>
    {
        protected ContestDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public ContestDictionary() { }
        public List<string> getContestList()
        {
            List<string> vs = new List<string>();

            foreach (string k in this.Keys)
            {
                vs.Add(k);
            }

            return vs;
        }
    }

    public class QuizDictionary : SerializableDictionary<string, Quiz>
    {
        public void GetList(QuizInfoDictionary basedictionary)
        {
            var keys = basedictionary.Keys;
            var values = basedictionary.Values;

            foreach (KeyValuePair<string, QuizInfo> k in basedictionary)
            {
                Quiz value = new Quiz();
                value.str = k.Value.str;
                value.kind = k.Value.kind;
                value.list = new String[4];
                for (int j = 0; j < 4; j++)
                {
                    value.list[j] = k.Value.list[j];
                }

                this.Add(k.Key, value);
            }
        }
    }

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
    }

}