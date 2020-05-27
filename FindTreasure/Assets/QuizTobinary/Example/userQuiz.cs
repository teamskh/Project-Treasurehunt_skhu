using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class Answer
    {
        public string str;
        public int kind;
        public string[] list;
        public bool Banswer;
        public int Ianswer;
        public string Wanswer;
    }

    [System.Serializable]
    public class TitleQuizDictionary : SerializableDictionary<string, Answer> {
        protected TitleQuizDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public TitleQuizDictionary() { }
    }
}