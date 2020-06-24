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
    //QuizInfoDictionary m_titleQuiz { get; set; }
    QuizInfoDictionary m_titleQuiz = new QuizInfoDictionary();

    private QuizDictionary m_QuizDicPlayer = new DataInfo.QuizDictionary();
    private AnswerDictionary m_AnswerDic = new DataInfo.AnswerDictionary();


    private void Update()
    {
           
    }
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

        adminManager.Instance.QuizPlayerCommunication(m_QuizDicPlayer);
        Debug.Log("Making Files");
    }

    private void DelQuizPlayer(string key)
    {
        m_QuizDicPlayer.Remove(key);
        adminManager.Instance.QuizPlayerCommunication(m_QuizDicPlayer);
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
        adminManager.Instance.AnswerCommunication(m_AnswerDic);
    }

    private void DelQuizAnswer(string key)
    {
        m_AnswerDic.Remove(key);
        adminManager.Instance.AnswerCommunication(m_AnswerDic);
    }

    #endregion

    #region Public Methods
    
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
        adminManager.Instance.QuizMadeCommunication(m_titleQuiz);

        AddQuizPlayer(title, quiz);
        AddQuizAnswer(title, quiz);
    }

    public void DeleteQuiz(string key)
    {
        if (m_titleQuiz.Remove(key))
        {
            adminManager.Instance.QuizMadeCommunication(m_titleQuiz);
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
        m_titleQuiz.CopyFrom(adminManager.Instance.CallQuizmadeDic());

        m_QuizDicPlayer.CopyFrom(adminManager.Instance.CallQuizplayerDic());
        m_AnswerDic.CopyFrom(adminManager.Instance.CallAnswerDic());
        if (GetComponent<QuizList>() != null)
            GetComponent<QuizList>().LoadQuiz();
    }
    
    public List<string> GetQuizList()//추가 
    {
        if (m_titleQuiz == null) return null;
        return m_titleQuiz.GetList();
    }
}
