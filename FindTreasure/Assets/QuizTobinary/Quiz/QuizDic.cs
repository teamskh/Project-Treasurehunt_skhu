﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using serializeDic;
using UnityScript.Scripting.Pipeline;
using System;

//저장용 변수 저장-로드용 함수 및 클래스

public class QuizDic : MonoBehaviour
{
    [SerializeField]
    QuizInfoDictionary m_titleQuiz;

    private string QuizdataPath;
    private string PlayerdataPath;
    private string ServerdataPath;

    private QuizDictionary m_QuizDicPlayer;
    private AnswerDictionary m_AnswerDic;

    #region Private Methods
    private void Initialized()
    {
        QuizdataPath = Application.persistentDataPath + "Quiz.dat";
        PlayerdataPath = Application.persistentDataPath + "PlayerQuiz.dat";
        ServerdataPath = Application.persistentDataPath + "ServerQuiz.dat";
    }

    //퀴즈 dictionary 변수 저장용 함수
    private void QuizSave()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(QuizdataPath);
        QuizInfoDictionary data = new QuizInfoDictionary();
        data.CopyFrom(m_titleQuiz);

        data.OnBeforeSerialize();
        bf.Serialize(file, data);
        file.Close();
    }

    //퀴즈 dictionary 변수 로드용 함수
    private void QuizLoad()
    {
        if (File.Exists(QuizdataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(QuizdataPath, FileMode.Open);
            m_titleQuiz = (QuizInfoDictionary)bf.Deserialize(file);
            m_titleQuiz.OnAfterDeserialize();
            file.Close();
        }
        else
        {
            m_titleQuiz = new QuizInfoDictionary();
        }

        return;
    }

    private void AddQuizPlayer(string title, QuizInfo quiz)
    {
        Quiz mQuiz = new Quiz();
        mQuiz.str = quiz.str;
        mQuiz.kind = quiz.kind;

        mQuiz.list = new string[4];

        if(mQuiz.kind == 1)
        {
            Array.Copy(quiz.list, mQuiz.list, 4);
        }

        m_QuizDicPlayer.Add(title, mQuiz);

        //변수 저장
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(PlayerdataPath);
        QuizDictionary data = new QuizDictionary();
        data.CopyFrom(m_QuizDicPlayer);

        data.OnBeforeSerialize();
        bf.Serialize(file, data);
        file.Close();
    }

    private void AddQuizAnswer(string title, QuizInfo quiz)
    {
        Answer ans = new Answer();
        ans.kind = quiz.kind;
        ans.score = quiz.score;
        switch (ans.kind)
        {
            case 0:
                ans.Banswer = quiz.Banswer;
                break;
            case 1:
                ans.Ianswer = quiz.Ianswer;
                break;
            case 2:
                ans.Wanswer = String.Copy(quiz.Wanswer);
                break;
        }

        m_AnswerDic.Add(title, ans);

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(ServerdataPath);
        AnswerDictionary data = new AnswerDictionary();
        data.CopyFrom(m_AnswerDic);

        data.OnBeforeSerialize();
        bf.Serialize(file, data);
        file.Close();
    }
    #endregion

    #region Public Methods
    public IDictionary<string,QuizInfo> QuizInfoDictionary
    {
        get { return m_titleQuiz; }
        set { m_titleQuiz.CopyFrom(value); }
    }

    public void AddQuiz(string title, QuizInfo quiz)
    {
        m_titleQuiz.Add(title, quiz);
        QuizSave();

        AddQuizPlayer(title, quiz);
        AddQuizAnswer(title, quiz);
    }

    #endregion

    private void Start()
    {
        Initialized();
        QuizLoad();
        
    }
}