using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public /*static*/ class QuizAddSceneManager : MonoBehaviour
{
    #region ver.YJ

    //추천 : Click listener에서 달때 kind 값을 명시하는 방식으로 사용할 수 있음.
    //ex> OX버튼이 눌렸을때 매개변수 kind에 0을 입력하여 전달.
    //inspector 창에서 Click Method에 이 함수를 등록 할 때, 전달 값을 정할 수 있는 입력창이 생김
    public void ChangeSceneToQuizAdd(int kind) 
    {
        PlayerPrefs.SetInt("ButtonClick", kind);
        SceneManager.LoadScene("QuizAdd");
    }

    #endregion

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("QuizMenu");
            }
        }
    }
}
