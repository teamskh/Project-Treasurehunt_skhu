using DataInfo;
using System;
using UnityEngine;
using UnityEngine.UI;

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
        dic = gameObject.GetComponent<QuizDic>();
        if(dic == null)
        { Debug.Log("dic is null"); }
        mQuiz = new QuizInfo();

        Changekind_TF();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Changekind_TF()
    {
        mQuiz.kind = 0;
        TFPanel.SetActive(true);
        IPanel.SetActive(false);
        WPanel.SetActive(false);
    }

    public void Changekind_int()
    {
        mQuiz.kind = 1;
        IPanel.SetActive(true);
        ipanelInit();
        TFPanel.SetActive(false);
        WPanel.SetActive(false);
    }

    public void Changekind_W()
    {
        mQuiz.kind = 2;
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
        mQuiz.str = quiz.text;

        if(mQuiz.kind == 1) {
            Array.Copy(GetComponent<makeNumber>().makeslist(), mQuiz.list, 4);
        }
        else if (mQuiz.kind == 2){
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
        Quizlog.text += string.Format("Title: {0}\nQuiz: {1}\nKind: {2}\n", title.text, quiz.text, mQuiz.kind);
        switch (mQuiz.kind)
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
