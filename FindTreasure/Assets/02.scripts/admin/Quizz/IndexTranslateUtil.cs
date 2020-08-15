using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexTranslateUtil : MonoBehaviour
{
    List<InputField> list = new List<InputField>();
    List<GameObject> buttons = new List<GameObject>();
    List<string> cont;
    GameObject panel1;
    GameObject panel2;
    GameObject swit_b;

    public void Set(GameObject panel)
    {
        BaseSet(panel.GetComponent<RectTransform>());

        SwitchingPanel(true);

        //list 변수 초기화
        SetInputFields(panel1.GetComponent<RectTransform>());

        //buttons 변수 초기화
        SetButtons(panel2.GetComponent<RectTransform>());

        //Switching용 버튼에 클릭 리스너 달기(버튼에 내용 전달)
        Button switch_b = swit_b.GetComponent<Button>();
        //보기를 버튼으로 만들기
        switch_b?.onClick.AddListener(() => MakeButtons());
        //panel switch
        switch_b?.onClick.AddListener(() => SwitchingPanel(false));

        cont = null;
    }
    
    void BaseSet(RectTransform rt)
    {
        panel1 = rt.GetChild(0).gameObject;
        panel2 = rt.GetChild(1).gameObject;
        swit_b = rt.GetChild(2).gameObject;
    }

    void SwitchingPanel(bool p1)
    {
        panel1.SetActive(p1);
        panel2.SetActive(!p1);
        //버튼은 switch 기능을 하기 위함이기 때문에 첫화면에서 활성화 되어 있어야 한다.
        //때문에 panel1과 같이 활성화한다.
        swit_b.SetActive(p1);
    }

    //IPanel(1) 패널에서 접근(rt : IPanel(1)의 RectTransform)
    //list1,list2,list3,list4라는 이름을 가진 오브젝트에서 inputfield 컴포넌트 찾아서 다루기 편하게 등록
    void SetInputFields(RectTransform rt)
    {
        for(int i = 1; i < 5; i++)
        {
            list.Add(rt.Find("list" + i).GetComponent<InputField>() ?? null);
        }
    }

    //IPanel(2) 패널에서 접근(rt : IPanel(2)의 RectTransform)
    //No1,No2,No3,No4라는 이름을 가진 오브젝트에서 button 컴포넌트 찾아서 다루기 편하게 등록
    void SetButtons(RectTransform rt)
    {
        for(int i = 1; i < 5; i++)
        {
            buttons.Add(rt.Find("No" + i).gameObject ?? null);
        }
    }
    
    //inputfield를 버튼으로 만드는 함수
    void MakeButtons()
    {
        //보기 내용용 변수 초기화
        cont = new List<string>();

        //QuizFactory에 있는 문제 저장용 변수에 답을 저장하는 "리스너"를 등록하기 위한 컴포넌트 찾기
        QuizFactory factor = gameObject.GetComponent<QuizFactory>();
        
        //inputfield -> button
        for(int i =0;i<4;i++)
        {
            string txt = list[i].textComponent.text;
            //버튼의 내용 전달

            buttons[i].GetComponentInChildren<Text>().text = txt;
            //보기 내용용 변수에 내용 저장
            cont.Add(txt);
        }
        buttons[0].GetComponent<Button>()?.onClick.AddListener(()=>factor.SetAnswer(1));
        buttons[1].GetComponent<Button>()?.onClick.AddListener(()=>factor.SetAnswer(2));
        buttons[2].GetComponent<Button>()?.onClick.AddListener(()=>factor.SetAnswer(3));
        buttons[3].GetComponent<Button>()?.onClick.AddListener(()=>factor.SetAnswer(4));
    }

    //보기 내용용 변수 직접 참조 전달
    public List<string> GetList()
    {
        return cont;
    }
}
