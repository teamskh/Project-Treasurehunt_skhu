using DataInfo;
using System;
using UnityEngine;
using UnityEngine.UI;
using TTM.Classes;

public class onClicks : MonoBehaviour
{
    [SerializeField]
    Text title;
    [SerializeField]
    Text quiz;

    [SerializeField]
    GameObject TFPanel;

    [SerializeField]
    GameObject IPanel;

    [SerializeField] GameObject ipanel1;
    [SerializeField] GameObject ipanel2;

    [SerializeField]
    GameObject WPanel;

    [SerializeField] Text Word;

    [SerializeField] Text Quizlog;
    
    private QuizDic dic;

    #region Private Variable
    private QuizInfo mQuiz;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        dic = gameObject.AddComponent<QuizDic>();
        mQuiz = new QuizInfo();

        Changekind_TF();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Changekind_TF()
    {
        mQuiz.Kind = 0;
        TFPanel.SetActive(true);
        IPanel.SetActive(false);
        WPanel.SetActive(false);
    }

    public void Changekind_int()
    {
        mQuiz.Kind = 1;
        IPanel.SetActive(true);
        ipanelInit();
        TFPanel.SetActive(false);
        WPanel.SetActive(false);
    }

    public void Changekind_W()
    {
        mQuiz.Kind = 2;
        WPanel.SetActive(true);
        IPanel.SetActive(false);
        TFPanel.SetActive(false);
    }
    public void IpanelChange()
    {
        ipanel1.SetActive(false);
        ipanel2.SetActive(true);
    }
    
    void ipanelInit()
    {
        ipanel1.SetActive(true);
        ipanel2.SetActive(false);
    }
    
    public void setAnswer(bool b){ mQuiz.Banswer = b; }

    public void setAnswer(int i) { mQuiz.Ianswer = i; }

    public void OK()
    {
        var mtitle = title.text;
        mQuiz.Str = quiz.text;

        if(mQuiz.Kind == 1) {
            mQuiz.List = new string[4];
            Array.Copy(GetComponent<makeNumber>().makeslist(), mQuiz.List, 4);
        }
        else if (mQuiz.Kind == 2){
            mQuiz.Wanswer = Word.text;
        }
        dic.AddQuiz(mtitle, mQuiz);
        if (Quizlog != null)
            QuizDebug();

        mQuiz = new QuizInfo();
    }

    void QuizDebug()
    {
        Quizlog.text = "";
        Quizlog.text += string.Format("Title: {0}\nQuiz: {1}\nKind: {2}\n", title.text, quiz.text, mQuiz.Kind);
        switch (mQuiz.Kind)
        {
            case 0:
                Quizlog.text += string.Format("Answer: {0}\n", mQuiz.Banswer);
                break;
            case 1:
                Quizlog.text += string.Format("Answer: {0}\n", mQuiz.Ianswer);
                break;
            case 2:
                Quizlog.text += string.Format("Answer: {0}\n", mQuiz.Wanswer);
                break;
            default:
                break;
        }
    }
}
