using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TTM.Classes;
using TTM.Save;

public class AnsIn : MonoBehaviour
{
    public GameObject Dtouch; // 클릭시 버튼 못 누르게
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

    void Update()
    {
        
        Answer current = FindAnswer(gameman.Instance.imageText);
        
        Inp.SetActive(false);

        if (Mtext.text == current.Wanswer)
        {
            testscore += current.Score;
            Tans.text = current.Wanswer;
            Tans.gameObject.SetActive(true);
        }
        else
        {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로 받아도 되겠지.
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
            RaycastHit hit;    // 정보 저장할 구조체 만들고
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
            {
                //t는 이미지를 인식했을때 이미지에 이름
                if (Input.GetTouch(0).phase == TouchPhase.Began)    // 딱 처음 터치 할때 발생한다
                {
                    if (hit.collider.CompareTag("in"))
                    {
                        Inp.SetActive(false);

                        if (Mtext.text == current.Wanswer)
                        {
                            testscore += current.Score;
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
                    Dtouch.SetActive(true);
            }
        }
    }

    private Answer FindAnswer(string key)
    {
        Answer answer = new Answer();
        /*
        AnswerDictionary dic;

        if(false)
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
        //answer.GetAns();
        */
        return answer;
    }
}
