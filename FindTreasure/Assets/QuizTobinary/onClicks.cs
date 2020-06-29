using DataInfo;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    #region Instance
    public static CompetDic instance;//추가
    #endregion

    public Text text;
    public Slider slider;
    public Toggle toggle_s;
    public static string Ttitle;
    string key;
    public InputField input1;
    public InputField input2;
    // Start is called before the first frame update
    void Start()
    {
        dic = gameObject.GetComponent<QuizDic>();
        mQuiz = new QuizInfo();

        Changekind_TF();
        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (QuizAddSceneManager.ButtonClick)
        {
            case 0:
                Changekind_TF();
                break;
            case 1:
                Changekind_int();
                break;
            case 2:
                Changekind_W();
                break;
        }

        switch (mQuiz.Kind)
        {
            case 0:
                Changekind_TF();
                /*
                key = scenechange.Qname;
                if (adminManager.Instance.CallQuizmadeDic().TryGetValue(key, out mQuiz))
                {
                    Debug.Log(mQuiz.Banswer);
                    input1.text = key;
                    input2.text= mQuiz.Str;
                    text.text = mQuiz.Score.ToString();
                    if (mQuiz.Banswer == true)
                    {
                        mQuiz.Banswer = true;
                    }
                    if(mQuiz.Banswer == false)
                    {
                        mQuiz.Banswer = false;
                    }
                }*/
                break;
            case 1:
                Changekind_int();
                /*
                key = scenechange.Qname;
                if (adminManager.Instance.CallQuizmadeDic().TryGetValue(key, out mQuiz))
                {
                    input1.text = key;
                    input2.text = mQuiz.Str;
                    text.text = mQuiz.Score.ToString();
                    for(int i=0; i<4; i++)
                    {
                        ipanel1.transform.GetChild(i).transform.GetComponent<InputField>().text = mQuiz.List[i];
                    }
                }*/
                break;
            case 2:
                Changekind_W();
                /*
                key = scenechange.Qname;
                if (adminManager.Instance.CallQuizmadeDic().TryGetValue(key, out mQuiz))
                {
                    input1.text = key;
                    input2.text = mQuiz.Str;
                    text.text = mQuiz.Score.ToString();
                    Word.text = mQuiz.Wanswer;
                }*/
                break;
        }
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

    public void SliderChange(float f)
    {
        slider.value = f;
        text.text = ((int)f).ToString();
    }

    public void HighLevelQ()
    {
        if (toggle_s.isOn)
        {
            text.text = "30";
            slider.interactable = false;
        }
        else
        {
            SliderChange(slider.value);
            slider.interactable = true;
        }
        Debug.Log(text.text);
    }
    public void OK()
    {
        var mtitle = title.text;
        mQuiz.Str = quiz.text;
        mQuiz.Score = int.Parse(text.text);
        if (mQuiz.Kind == 1) {
            mQuiz.List = new string[4];
            Array.Copy(GetComponent<makeNumber>().makeslist(), mQuiz.List, 4);
        }
        else if (mQuiz.Kind == 2){
            mQuiz.Wanswer = Word.text;
        }
        /*
        if (title.text == key)
        {
            dic.DeleteQuiz(key);
        }
        else
        {
            dic.AddQuiz(mtitle, mQuiz);
        }*/
        dic.AddQuiz(mtitle, mQuiz);
        if (Quizlog != null)
            QuizDebug();
        Ttitle = mtitle;
        QuizList.number = 1;
        SceneManager.LoadScene("QuizMenu");
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
