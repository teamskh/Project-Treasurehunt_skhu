using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class onClicks : MonoBehaviour
{
    [SerializeField]
    Text title;
    [SerializeField]
    Text quiz;
    [SerializeField]
    GameObject Contents;

    [SerializeField]
    GameObject TFPanel;

    [SerializeField]
    GameObject IPanel;

    [SerializeField] GameObject ipanel1;
    [SerializeField] GameObject ipanel2;

    [SerializeField]
    GameObject WPanel;

    [SerializeField] Text Word;

    [SerializeField] Text QuizInfo;
    
    private SerializeDic dic;
    private ScrollRect box;
    // Start is called before the first frame update
    void Start()
    {
        dic = gameObject.GetComponent<SerializeDic>();
        if(dic == null)
        { Debug.Log("dic is null"); }
        box = Contents.GetComponent<ScrollRect>();
        Changekind_TF();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Changekind_TF()
    {
        dic.ans.kind = 0;
        TFPanel.SetActive(true);
        IPanel.SetActive(false);
        WPanel.SetActive(false);
        box.content = TFPanel.GetComponent<RectTransform>();
    }

    public void Changekind_int()
    {
        dic.ans.kind = 1;
        IPanel.SetActive(true);
        ipanelInit();
        TFPanel.SetActive(false);
        WPanel.SetActive(false);
        box.content = IPanel.GetComponent<RectTransform>();
    }

    public void Changekind_W()
    {
        dic.ans.kind = 2;
        WPanel.SetActive(true);
        IPanel.SetActive(false);
        TFPanel.SetActive(false);
        box.content = WPanel.GetComponent<RectTransform>();
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
    
    public void setAnswer(bool b){ dic.ans.Banswer = b; }

    public void setAnswer(int i) { dic.ans.Ianswer = i; }

    public void OK()
    {
        dic.title = title.text;
        dic.ans.str = quiz.text;
        if(dic.ans.kind == 1) {
            GetComponent<makeNumber>().makelist();
        }
        else if (dic.ans.kind == 2){
            dic.ans.Wanswer = Word.text;
        }
        dic.setTitleQuiz();
        //QuizDebug();
    }

    void QuizDebug()
    {
        QuizInfo.text = "";
        QuizInfo.text += string.Format("Title: {0}\nQuiz: {1}\nKind: {2}\n", title.text, quiz.text,dic.ans.kind);
        switch (dic.ans.kind)
        {
            case 0:
                QuizInfo.text += string.Format("Answer: {0}\n", dic.ans.Banswer);
                break;
            case 1:
                QuizInfo.text += string.Format("Answer: {0}\n", dic.ans.Ianswer);
                break;
            case 2:
                QuizInfo.text += string.Format("Answer: {0}\n", dic.ans.Wanswer);
                break;
            default:
                break;
        }
    }
}
