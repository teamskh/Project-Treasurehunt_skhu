using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTM.Classes;


public class Answerr : MonoBehaviour
{
    public GameObject Dtouch; // 클릭시 버튼 못 누르게
    public GameObject[] ans; // 답

    public Material Mat1;
    public Material Mat2;

    //public string t; //저장된 aa 테스트하기 위해서 aa 입력
    public bool Ban;
    int a;

    public TextMesh[] AnsTex;

    public int testscore = 0;

    private string num; //태그 번호

    // Start is called before the first frame update
    void Start()
    {
        Answer current = gameman.Instance.CheckAnswer();
        Quiz Anslist = gameman.Instance.FindQuiz();
        if (current != null)
        {
            
            Ban = current.Banswer;
            for (int i = 0; i < 4; i++)
                AnsTex[i].text = Anslist.List[i];
        }
             
    }

    void Update()
    {
        Answer current = gameman.Instance.CheckAnswer();
        for (int i =0;i<4;i++)
        {
            if(current.Ianswer == a && a==i+1)
            {
                testscore += current.Score;
                ans[i].GetComponent<Renderer>().material = Mat1;
            }
            else
            {
                //t는 이미지를 인식했을때 이미지에 이름
                /*if (Input.GetTouch(0).phase == TouchPhase.Began)    // 딱 처음 터치 할때 발생한다
                {
                    for (int i = 0; i < 4; i++)
                    {
                        num = i.ToString();
                        if (hit.collider.CompareTag(num))
                        {
                            if(current.Ianswer == i)
                            {
                                testscore += current.score;
                                ans[i].GetComponent<Renderer>().material = Mat1;
                            }
                            else
                            {
                                ans[i].GetComponent<Renderer>().material = Mat2;
                            }
                        }
                    }
                    Dtouch.SetActive(true);
                }*/
            }
        }
    }
}
