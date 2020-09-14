using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class WordAnswerUtil : MonoBehaviour
{
    public InputField wordAnswer;
    public Q Quiz { get; set; }

    public void Set(GameObject panel)
    {
        wordAnswer = panel.GetComponentInChildren<InputField>();
        SetWordListener();
    }

    //정답 내용 전달하기 위한 함수
    public string GetWordAnswer()
    {
        //return wordAnswer.textComponent.text;
        return wordAnswer.text;
    }

    void SetWordListener()
    {
        QuizFactory factor = GetComponent<QuizFactory>();
        wordAnswer.onEndEdit.AddListener(delegate { inputSubmitCallBack(); });
        wordAnswer.onValueChanged.AddListener(delegate { inputChangedCallBack(); });
    }

    //답 체크
    private void inputSubmitCallBack()
    {
        QuizFactory factor = GetComponent<QuizFactory>();
        Debug.Log("Input Submitted");
        factor.SetAnswer(wordAnswer.text);
    }

    //inputfield 내용 변화 체크
    private void inputChangedCallBack()
    {
        Debug.Log("Input Changed");
    }
}
