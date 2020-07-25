using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordAnswerUtil : MonoBehaviour
{
    InputField wordAnswer;
   public void Set(GameObject panel)
    {
        wordAnswer = panel.GetComponentInChildren<InputField>();
    }

    //정답 내용 전달하기 위한 함수
    public string GetWordAnswer()
    {
        return wordAnswer.textComponent.text;
    }
}
