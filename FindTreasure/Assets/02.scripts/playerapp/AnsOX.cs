using DataInfo;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnsOX : MonoBehaviour
{
    public GameObject Dtouch; // 클릭시 버튼 못 누르게
    public GameObject[] ans; // 답

    public Material Mat1;
    public Material Mat2;

    //public string t; //저장된 aa 테스트하기 위해서 aa 입력
    public bool Ban;

    public int testscore = 0;

    public TextMesh te;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        QuizAnswerLoad script = GetComponent<QuizAnswerLoad>();
        if (script != null)
        {
            Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
            if (current != null)
                Ban = current.Banswer;
            else
                te.text = gameman.Instance.imageText;
        }
        else
        {
            Debug.Log("Can't find scripts");
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);    // 변환 안하고 바로 Vector3로 받아도 되겠지.
            Ray ray = Camera.main.ScreenPointToRay(theTouch);    // 터치한 좌표 레이로 바꾸엉
            RaycastHit hit;    // 정보 저장할 구조체 만들고
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
            {
                Answer current = GetComponent<QuizAnswerLoad>().m_AnswerDictionary.GetAnswer(gameman.Instance.imageText);
                //t는 이미지를 인식했을때 이미지에 이름
                if (Input.GetTouch(0).phase == TouchPhase.Began)    // 딱 처음 터치 할때 발생한다
                {
                    if (hit.collider.CompareTag("O"))
                    {
                        if (Ban == true)
                        {
                            testscore += current.score;
                            ans[0].GetComponent<Renderer>().material = Mat1;
                            ans[1].GetComponent<Renderer>().material = Mat2;
                        }
                        else
                        {
                            ans[0].GetComponent<Renderer>().material = Mat2;
                            ans[1].GetComponent<Renderer>().material = Mat1;
                        }
                    }
                    else if (hit.collider.CompareTag("X"))
                    {
                        if (Ban == true)
                        {
                            ans[0].GetComponent<Renderer>().material = Mat2;
                            ans[1].GetComponent<Renderer>().material = Mat1;
                        }
                        else
                        {
                            testscore += current.score;
                            ans[0].GetComponent<Renderer>().material = Mat1;
                            ans[1].GetComponent<Renderer>().material = Mat2;
                        }
                    }
                    Dtouch.SetActive(true);
                }
            }
        }
    }
}
