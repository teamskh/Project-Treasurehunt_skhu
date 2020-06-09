using DataInfo;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnsIn : MonoBehaviour
{
    //public string t; //저장된 aa 테스트하기 위해서 aa 입력
    public TextMesh In; //문제 가져오려고
    public Text Out; //캔버스에 문제 출력하기 위해

    public Text Mtext;
    
    public TextMesh Tans;
    public TextMesh Fans;

    public GameObject Inp; //입력 버튼
    public int testscore = 0;

    public void Inpu()
    {
        Out.text = In.text;
    }

    public void AnsW()
    {
        Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
        
        Inp.SetActive(false);

        if (Mtext.text == current.Wanswer)
        {
            testscore += current.score;
            Tans.text = current.Wanswer;
            Tans.gameObject.SetActive(true);
        }
        else
        {
            Fans.text = current.Wanswer;
            Fans.gameObject.SetActive(true);
        }
            
    }
}
