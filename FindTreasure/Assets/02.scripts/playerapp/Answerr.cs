using DataInfo;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answerr : MonoBehaviour
{
    public GameObject an; // 클릭시 버튼 못 누르게
    public GameObject[] ans; // 답

    public Material Mat1;
    public Material Mat2;

    //public string t; //저장된 aa 테스트하기 위해서 aa 입력
    public bool Ban;

    public TextMesh[] AnsTex;

    public int testscore = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        QuizAnswerLoad script = GetComponent<QuizAnswerLoad>();
        if (script != null)
        {
            Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
            Quiz Anslist = GetComponent<PlayerQuizLoad>().m_QuizDictionary.FindQuiz(gameman.Instance.imageText);
            if (current != null)
            {
                Ban = current.Banswer;
                for(int i=0;i<4;i++)
                    AnsTex[i].text = Anslist.list[i];
            }
                
            
        }
        else
        {
            Debug.Log("Can't find scripts");
        }
    }
    
    public void Ansfirst()
    {
        Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
        if (current.Ianswer == 1)
        {
            testscore += current.score;
            ans[0].GetComponent<Renderer>().material = Mat1;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 2)
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat1;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 3)
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat1;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 4)
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat1;
        }

        an.SetActive(true);

    }
    public void Anssecond()
    {
        Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
        if (current.Ianswer == 1)
        {
            
            ans[0].GetComponent<Renderer>().material = Mat1;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 2)
        {
            testscore += current.score;
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat1;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 3)
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat1;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 4)
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat1;
        }
        an.SetActive(true);

    }
    public void Ansthird()
    {
        Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
        if (current.Ianswer == 1)
        {
            
            ans[0].GetComponent<Renderer>().material = Mat1;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 2)
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat1;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 3)
        {
            testscore += current.score;
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat1;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 4)
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat1;
        }
        an.SetActive(true);

    }
    public void Ansfour()
    {
        Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
        if (current.Ianswer == 1)
        {
            ans[0].GetComponent<Renderer>().material = Mat1;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 2)
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat1;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 3)
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat1;
            ans[3].GetComponent<Renderer>().material = Mat2;
        }
        else if (current.Ianswer == 4)
        {
            testscore += current.score;
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat2;
            ans[2].GetComponent<Renderer>().material = Mat2;
            ans[3].GetComponent<Renderer>().material = Mat1;
        }

        an.SetActive(true);
    }
    
}
