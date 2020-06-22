using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

using DataInfo;

using TTM.Classes;
using TTM.Save;

public class scrolling : MonoBehaviour
{
    public GameObject sco; // 스크롤UI
    
    public float DestryTime = 5.0f; //스크롤 사라지기 전까지 시간
    float timer; // 스크롤 내용 뜨기 전 로딩 시간

    public GameObject[] chek; // ox / 객관식 / 주관식 중 맞는거 active
    public int kk; //ox / 객관식 / 주관식 중 맞는거 번호로 구분 0~2

    Text qtxt;
    protected Quiz quiz;
    
    public bool Ban; //ox 답확인
    public Text[] AnsTex; //객관식 내용 바뀌기
    public InputField inT; //주관식 답 입력
    public Text prinTex; //주관식답
    public int testscore = 0; //점수

    public GameObject[] btn; //버튼 오브젝트 없애기 다시 못눌리게


    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.8)
        {
            //Action
            for (int i = 0; i < 3; i++)
            {
                Answer current = gameman.Instance.CheckAnswer();
                if (i == current.Kind)
                { //i 값이 스크롤내용 구분하는거
                    chek[i].SetActive(true);

                    //update에 넣은 이유는 문제들이 모두 setactive(false)되어있기때문에 못 찾는다

                    foreach (var item in GetComponentsInChildren<Text>())
                    {
                        if (item.gameObject.tag == "STR")
                        {
                            qtxt = item;
                            break;
                        }
                    }

                    if (i == 1)
                    {
                        Quiz Anslist = gameman.Instance.FindQuiz();
                        if (current != null)
                        {
                            Ban = current.Banswer;
                            for (int j = 0; j < 4; j++)
                                AnsTex[j].text = Anslist.List[j]; //AnsTex 글자
                        }
                    }
                }
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
        Answer current = gameman.Instance.CheckAnswer();
        for (int i = 0; i < 2; i++)
        {
            if ((a == 0)&&(Ban==true))
            {
                //점수추가
                testscore += current.Score;
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
        Answer current = gameman.Instance.CheckAnswer();
        for (int i = 0; i < 4; i++)
        {
            if ((current.Ianswer == a) && (a==i+1))
            {
                testscore += current.Score; //점수추가
                GameObject.Find(a.ToString()).GetComponent<Image>().color = new Color(0, 1, 0);
                //Debug.Log("no");
            }
            else if (current.Ianswer == a)
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
        Answer current = FindAnswer(gameman.Instance.imageText);

        if (inT.text == current.Wanswer) //글자가 같다면
        {
            testscore += current.Score; //점수추가
            prinTex.text = current.Wanswer;
            prinTex.GetComponent<Text>().color = new Color(0, 1, 0);
        }
        else
        {
            prinTex.text = current.Wanswer;
            prinTex.GetComponent<Text>().color = new Color(1, 0, 0);
        }
    }

    private Answer FindAnswer(string key)
    {
        Answer answer = new Answer();

        AnswerDictionary dic;

        if (JsonLoadSave.LoadAnswers(out dic))
        {
            Debug.Log("AnswerJson Exist");
            if (dic.TryGetValue(key, out answer))
            {
                Debug.Log($"Key: {key} Exist");
            }
            else Debug.Log($"Key: {key} Doesn't Exist");
        }
        else
        {
            Debug.Log("AnswerJson Doesn't Exist");
        }
        answer.GetAns();

        return answer;
    }
}
