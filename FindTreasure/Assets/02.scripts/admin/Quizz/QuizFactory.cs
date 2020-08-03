using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTM.Classes;
using UnityEngine.UI;
using BackEnd;

public class QuizFactory : MonoBehaviour
{
    public RectTransform BasePanel;
    Button SaveButton;

    private int kind;
    private List<GameObject> panel = new List<GameObject>();
    private InputFieldUtil inputfields;
    IndexTranslateUtil ipanelCompo;
    WordAnswerUtil wpanelCompo;
    Q quiz;
    // Start is called before the first frame update

    private void OnEnable()
    {
        //활성화 하자 마자 초기화
        quiz = new Q();

        //Scene에서 맞는 패널 자체 장착
        panel?.Add(BasePanel.Find("TFPanel").gameObject ?? null);
        panel?.Add(BasePanel.Find("IPanel").gameObject ?? null);
        panel?.Add(BasePanel.Find("WPanel").gameObject ?? null);
        SaveButton = GameObject.Find("Save")?.GetComponent<Button>();
    }
    void Start()
    {
        //문제 유형 전달
        //kind = PlayerPrefs.GetInt("kind_quiz");
        kind = 1;

        //특정 Panel 활성화
        SetPanelActive();

        //문제 추가용 inputField 세트 및 연결하는 초기화 과정
        inputfields = gameObject.AddComponent<InputFieldUtil>();

        //종류에 맞는 컴포넌트 추가를 위한 Coroutine 
        //각각 컴포넌트 더하고 기본 세팅하는 기능
        switch (kind)
        {
            case 0:
                StartCoroutine(TFPanelSet());
                break;
            case 1:
                StartCoroutine(IPanelSet());
                break;
            case 2:
                StartCoroutine(WPanelSet());
                break;
        }

        //전달자에 kind전달
        quiz.Kind = kind;
        
        //버튼 객체 있으면 Save 함수를 클릭 리스너로 등록
        SaveButton?.onClick.AddListener(() => Save());
    }

    void Save()
    {
        //Title inputField에서 내용 받아오기
        if ((quiz.Title = inputfields.GetInputFieldString(0)) == "")
        {
            Debug.LogError("Title is Empty");
            return;
        }
        Debug.Log($"Title : {quiz.Title}");

        //Context inputField에서 내용 받아오기
        if ((quiz.Str = inputfields.GetInputFieldString(1)) == "")
        {
            Debug.LogError("Context is empty");
            return;
        }
        Debug.Log($"Context: {quiz.Str}");

        //점수 받아오기
        quiz.Score = inputfields.GetScore();
        Debug.Log($"Score : {quiz.Score}");

        Debug.Log($"Kind : {quiz.Kind}");

        //Quiz가 index 종류일 경우 보기 항목 받아오기
        if(kind == 1)
        {
            var list = ipanelCompo.GetList();
            if (list == null)
            {
                Debug.LogError("List is Null");
            }

            quiz.List = list.ToArray();

            foreach(var item in quiz.List)
            {
                Debug.Log($"items : {item}");
            }
            //씬 전환
        }

        //Quiz가 word종류일 경우 답 받아오기
        if(kind == 2)
        {
            var word = wpanelCompo.GetWordAnswer();
            if (word == "")
            {
                Debug.LogError("Word Answer is Empty");
                return;
            }

            SetAnswer(word);
            //씬 전환
        }

        //답 디버깅용
        switch (quiz.Answer)
        {
            case bool b:
                Debug.Log($"True / False : {b}");
                break;
            case int i:
                Debug.Log($"Index : {i}");
                break;
            case string s:
                Debug.Log($"Word : {s}");
                break;
        }

        Param param = new Param();
        param.SetQuiz(quiz).InsertQuiz();
        Debug.Log($"Param Data : {param.ToStr()}");
    }
    
    //정답 전달
    public void SetAnswer(object T)
    {
        if (T.GetType() == typeof(int))
            Debug.Log($"Index: {T}");
        quiz.Answer = T;
    }

    #region Panel Controll
    void SetPanelActive()
    {
        //전체 비활성화
        foreach(var obj in panel)
        {
            obj.SetActive(false);
        }

        //종류에 맞는 패널 활성화
        panel[kind].SetActive(true);

    }

    #endregion

    #region Each Coroutine

    IEnumerator TFPanelSet()
    {
        yield return null;
        //초기화 작업에 필요한 컴포넌트 더하기
        gameObject.AddComponent<TrueFalseButtonUtil>().Set(panel[0]);
    }

    IEnumerator IPanelSet()
    {
        yield return null;
        //보기 변수를 받아오기 위해 ipanelCompo에 컴포넌트 저장
        ipanelCompo = gameObject.AddComponent<IndexTranslateUtil>();
        ipanelCompo.Set(panel[1]);

    }

    IEnumerator WPanelSet()
    {
        //정답 변수를 받아오기 위해 wpanelCompo에 컴포넌트 저장
        yield return null;
        wpanelCompo = gameObject.AddComponent<WordAnswerUtil>();
        wpanelCompo.Set(panel[2]);
    }

    #endregion
}
