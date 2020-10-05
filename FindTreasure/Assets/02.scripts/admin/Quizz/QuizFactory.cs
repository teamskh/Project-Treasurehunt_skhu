using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTM.Classes;
using UnityEngine.UI;
using BackEnd;
using UnityEngine.SceneManagement;

public class QuizFactory : MonoBehaviour
{
    public RectTransform BasePanel;
    Button SaveButton;
    Button CancelButton;

    private int kind;
    int code;
    string key;
    bool newQ=true;
    private List<GameObject> panel = new List<GameObject>();
    private InputFieldUtil inputfields;
    IndexTranslateUtil ipanelCompo;
    WordAnswerUtil wpanelCompo;
    Q quiz;
    QuiDictionary dic;

    private void OnEnable()
    {
        Debug.Log("Set Initial");
        //Scene에서 맞는 패널 자체 장착
        panel?.Add(BasePanel.Find("TFPanel").gameObject ?? null);
        panel?.Add(BasePanel.Find("IPanel").gameObject ?? null);
        panel?.Add(BasePanel.Find("WPanel").gameObject ?? null);
        SaveButton = GameObject.Find("Save")?.GetComponent<Button>();
        CancelButton = GameObject.Find("Before")?.GetComponent<Button>();
    }
    void Start()
    {
        //문제 유형 전달
        kind = PlayerPrefs.GetInt("ButtonClick",-1);
        PlayerPrefs.DeleteKey("ButtonClick"); // 런타임 중복으로 값이 남지 않게 하기 위함.
        //CancelButton?.onClick.AddListener(() => Back());

        if (kind < 0)
        {
            key = scenechange.Qname;

            dic = new QuiDictionary();
            code = PlayerPrefs.GetInt("a_competition");

            dic.GetQuizz(code);
            dic.TryGetValue(key, out quiz);

            kind = quiz.Kind.Value;

            newQ = false;
            CancelButton?.onClick.AddListener(() => Cancel());
        }
        else
        {
            CancelButton?.onClick.AddListener(() => Back());
        }

        //특정 Panel 활성화
        SetPanelActive();

        //문제 추가용 inputField 세트 및 연결하는 초기화 과정
        inputfields = gameObject.AddComponent<InputFieldUtil>();
        inputfields.Quiz = quiz;

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

        //버튼 객체 있으면 Save 함수를 클릭 리스너로 등록
        SaveButton?.onClick.AddListener(() => Save());
    }

    void Cancel()
    {
        SceneManager.LoadScene("QuizMenu");
        BackSpace.Instance.Pop();
    }

    void Back()
    {
        SceneManager.LoadScene("QuizType");
        BackSpace.Instance.Pop();
    }

    void Save()
    {
        if(quiz == null)
        {
            Debug.LogError("Need to Check Answer");
            return;
        }
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

        //전달자에 kind전달
        quiz.Kind = kind;
        Debug.Log($"Kind : {quiz.Kind}");

        //Quiz가 index 종류일 경우 보기 항목 받아오기
        if (kind == 1)
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
        if (newQ == false)
            param.UpdateQuiz(quiz);
        else param.SetQuiz(quiz).InsertQuiz();

        if (newQ)
            SaveImage.Instance.SaveIMG(quiz.Title);
        else
        {
            if (key != quiz.Title) FTP.ImageServerRenameFile(AdminCurState.Instance.Competition, key, quiz.Title);
            if (SaveImage.Instance.NeedToSave)
                SaveImage.Instance.SaveIMG(quiz.Title);
        }
        Debug.Log($"Param Data : {param.ToStr()}");
        newQ = true;
        
    }
    
    //정답 전달
    public void SetAnswer(object T)
    {
        quiz = new Q();
        if (T.GetType() == typeof(int))
        {
            Debug.Log($"Index: {T}");
            gameObject.AddComponent<IndexTranslateUtil>().SelectB(T);
        }
        else if (T.GetType() == typeof(bool))
            gameObject.AddComponent<TrueFalseButtonUtil>().SelectB(T);
        else if (T.GetType() == typeof(string))
        {
            gameObject.AddComponent<WordAnswerUtil>().Set(panel[2]);
        }

        quiz.Answer = T;
        Debug.Log(quiz.Answer);
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
