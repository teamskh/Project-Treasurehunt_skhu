using DataInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTM.Classes;

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
        Answer current = gameman.instance.CheckAnswer();
        Quiz Anslist = gameman.instance.FindQuiz();
        if (current != null)
        {
            Ban = current.Banswer;
            for (int i = 0; i < 4; i++)
                AnsTex[i].text = Anslist.List[i];
        }
             
    }
    
    public void Ans(int a)
    {
        Answer current = gameman.instance.CheckAnswer();
        for (int i =0;i<4;i++)
        {
            if(current.Ianswer == a&& a==i+1)
            {
                testscore += current.Score;
                ans[i].GetComponent<Renderer>().material = Mat1;
            }
            else
            {
                ans[i].GetComponent<Renderer>().material = Mat2;
            }
        }
    }
}
