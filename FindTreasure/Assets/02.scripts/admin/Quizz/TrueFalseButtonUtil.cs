using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrueFalseButtonUtil : MonoBehaviour
{
    Button True;
    Button False;

    public void Set(GameObject panel)
    {
        //자식 객체로 접근하면 찾는 데 시간을 줄일 수 있을거라 생각.
        RectTransform rtransform = panel.GetComponent<RectTransform>();
        var T = rtransform.Find("True") ?? null;
        True = T?.gameObject.GetComponent<Button>();

        T = rtransform.Find("False") ?? null;
        False = T?.gameObject.GetComponent<Button>();

        //리스너 등록용
        SetTFListener();
    }

    //True False 버튼에 리스너 등록
    void SetTFListener()
    {
        //QuizFactory에 저장용 객체가 있기 때문에, QuizFactory 찾아 등록
        //사용하려는 함수가 QuizFactory에서 static 함수 형태로 사용할 수 없기 때문
        QuizFactory factor = GetComponent<QuizFactory>();
        True?.onClick.AddListener(() =>factor.SetAnswer(true));
        False?.onClick.AddListener(() => factor.SetAnswer(false));
    }

}
