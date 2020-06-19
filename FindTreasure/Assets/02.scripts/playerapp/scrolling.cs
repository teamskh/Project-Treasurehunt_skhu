using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DataInfo;
using UnityEngine.UI;

public class scrolling : MonoBehaviour
{
    public GameObject sco; // 스크롤
    public GameObject[] chek; // ox / 객관식 / 주관식 선택
    public float DestryTime = 5.0f; //스크롤 사라지기 전까지 시간

    float timer; // 시간
    double waitingTime; // 스크롤 내용 뜰 때 시간
    
    public GameObject[] btn; //버튼 오브젝트 없애기 다시 못눌리게
    public bool Ban; //ox확인
    public InputField inT; //답 입력
    public Text prinTex; //답
    public int kk;

    void Start()
    {
        timer = 0;
        waitingTime = 0.8;

        /*PlayerQuizLoad script1 = GetComponent<PlayerQuizLoad>();
        if (script1 != null)
        {
            Quiz current1 = GetComponent<PlayerQuizLoad>().m_QuizDictionary.FindQuiz("ㅏ");
            if(current1 !=null)
                Debug.Log("Can't find scripts");
            //뭘 적어야할까
        }
        QuizAnswerLoad script = GetComponent<QuizAnswerLoad>();
        if (script != null)
        {
            Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
            if (current != null)
                Ban = current.Banswer;
        }
        else
        {
            Debug.Log("Can't find scripts");
        }*/
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            //Action
            for (int i = 0; i < 3; i++)
            {
                if (i == kk) //i 값이 스크롤 구분하는거
                    chek[i].SetActive(true);
                else
                    chek[i].SetActive(false);
            }
                    
            timer = 0;
        }
    }

    public void dest()
    {
        Destroy(sco, DestryTime);
    }

    public void touch(int a)
    {
        //Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
        for (int i = 0; i < 2; i++)
        {
            if ((a == 0)&&(Ban==true)) 
            {
                //점수추가
                GameObject.Find("changeO").GetComponent<Image>().color = new Color(0, 1, 0);
                GameObject.Find("changeX").GetComponent<Image>().color = new Color(1, 0, 0);
            }
            else
            {
                GameObject.Find("changeO").GetComponent<Image>().color = new Color(1, 0, 0);
                GameObject.Find("changeX").GetComponent<Image>().color = new Color(0, 1, 0);
            }
        }
        btn[0].SetActive(false);
        btn[1].SetActive(false);
    }
    
    public void list(int a)
    {
        //Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
        for (int i = 0; i < 4; i++)
        {
            if ((a == 3) && (i == 3))
            {
                GameObject.Find(a.ToString()).GetComponent<Image>().color = new Color(0, 1, 0);
                //점수추가
                Debug.Log("no");
            }
            else if (i == 3)
            {
                GameObject.Find(i.ToString()).GetComponent<Image>().color = new Color(0, 1, 0);
            }
            else
                GameObject.Find(i.ToString()).GetComponent<Image>().color = new Color(1, 0, 0);
        }
        for(int i = 2; i < 6; i++)
        {
            btn[i].SetActive(false);
        }
    }

    public void inp()
    {
        //Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
        if (inT.text == "Test") //글자가 같다면
        {
            prinTex.text = "Test";
            prinTex.GetComponent<Text>().color = new Color(0, 1, 0);
            //점수추가
        }
        else
        {
            prinTex.text = "Test";
            prinTex.GetComponent<Text>().color = new Color(1, 0, 0);
        }
    }
}
