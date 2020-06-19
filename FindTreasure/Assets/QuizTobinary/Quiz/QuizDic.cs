using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
using System;
using TTM.Classes;
using TTM.Save;

//저장용 변수 저장-로드용 함수 및 클래스

public class QuizDic : MonoBehaviour
{
    [SerializeField]
    QuizInfoDictionary m_titleQuiz;

    private QuizDictionary m_QuizDicPlayer;
    private AnswerDictionary m_AnswerDic;

    #region Private Methods

    private void AddQuizPlayer(string title, QuizInfo quiz)
    {
        Quiz mQuiz = new Quiz();
        mQuiz.Str = quiz.Str;
        mQuiz.Kind = quiz.Kind;

        mQuiz.List = new string[4];

        if(mQuiz.Kind == 1){
            Array.Copy(quiz.List, mQuiz.List, 4);
        }

        m_QuizDicPlayer.Add(title, mQuiz);

        JsonLoadSave.SaveQuizs(m_QuizDicPlayer);
        Debug.Log("Making Files");
    }

    private void DelQuizPlayer(string key)
    {
        m_QuizDicPlayer.Remove(key);
        JsonLoadSave.SaveQuizs(m_QuizDicPlayer);
    }

    private void AddQuizAnswer(string title, QuizInfo quiz)
    {
        Answer ans = new Answer();
        ans.Kind = quiz.Kind;
        ans.Score = quiz.Score;
        switch (ans.Kind)
        {
            case 0:
                ans.Banswer = quiz.Banswer;
                break;
            case 1:
                ans.Ianswer = quiz.Ianswer;
                break;
            case 2:
                ans.Wanswer = quiz.Wanswer;
                break;
        }

        m_AnswerDic.Add(title, ans);
        JsonLoadSave.SaveAnswers(m_AnswerDic);
    }

    private void DelQuizAnswer(string key)
    {
        m_AnswerDic.Remove(key);
        JsonLoadSave.SaveAnswers(m_AnswerDic);
    }

    #endregion

    #region Public Methods
    public IDictionary<string,QuizInfo> QuizInfoDictionary
    {
        get { return m_titleQuiz; }
        set { m_titleQuiz.CopyFrom(value); }
    }
    public IDictionary<string, Quiz> QuizDictionary
    {
        get { return m_QuizDicPlayer; }
        set { m_QuizDicPlayer.CopyFrom(value); }
    }
    public IDictionary<string, Answer> AnswerDictionary
    {
        get { return m_AnswerDic; }
        set { m_AnswerDic.CopyFrom(value); }
    }

    public void AddQuiz(string title, QuizInfo quiz)
    {
        m_titleQuiz.Add(title, quiz);
        JsonLoadSave.SaveQuizMade(m_titleQuiz);

        AddQuizPlayer(title, quiz);
        AddQuizAnswer(title, quiz);
    }

    public void DeleteQuiz(string key)
    {
        if (m_titleQuiz.Remove(key))
        {
            JsonLoadSave.SaveQuizMade(m_titleQuiz);
            DelQuizPlayer(key);
            DelQuizAnswer(key);
#if UNITY_EDITOR
            Debug.Log("Detele Item Key: " + key);
#endif
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Can't Remove Item Key: " + key);
#endif
        }
    }

#endregion

    private void Start()
    {
        JsonLoadSave.LoadQuizMade(out m_titleQuiz);
        JsonLoadSave.LoadQuizs(out m_QuizDicPlayer);
        JsonLoadSave.LoadAnswers(out m_AnswerDic);
    }
    
    public List<string> GetQuizList()//추가 
    {
        return m_titleQuiz.GetList();
    }
}
