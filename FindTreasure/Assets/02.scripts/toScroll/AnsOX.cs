using DataInfo;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TTM.Classes;

public class AnsOX : Scroll
{
    public GameObject Dtouch; // 클릭시 버튼 못 누르게
    public GameObject[] ans; // 답

    public Material Mat1;
    public Material Mat2;

    //public string t; //저장된 aa 테스트하기 위해서 aa 입력
    public bool Ban;

    public int testscore = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        /*Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
        if (script != null)
        {
           
            if (current != null)
                Ban = current.Banswer;
        }
        else
        {
            Debug.Log("Can't find scripts");
        }*/
    }
    public new void Init(Quiz quiz)
    {
        base.Init(quiz);

    }


    public void chek()
    {
        Debug.Log("check");
    }

    public void AnsO()
    {
        Debug.Log("push");
        Answer current = gameman.instance.CheckAnswer();
        //t는 이미지를 인식했을때 이미지에 이름

        if (Ban == true)
        {
            testscore += current.Score;
            ans[0].GetComponent<Renderer>().material = Mat1;
            ans[1].GetComponent<Renderer>().material = Mat2;
        }


        else
        {
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat1;
        }

        Dtouch.SetActive(true);

    }
    public void AnsX()
    {
       // Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);

        if (Ban == false)
        {
            //testscore += current.Score;
            ans[0].GetComponent<Renderer>().material = Mat2;
            ans[1].GetComponent<Renderer>().material = Mat1;
        }
        else
        {
            ans[0].GetComponent<Renderer>().material = Mat1;
            ans[1].GetComponent<Renderer>().material = Mat2;
        }

        Dtouch.SetActive(true);

    }
}
