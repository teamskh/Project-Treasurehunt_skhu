using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenechange :MonoBehaviour
{ 
    public static string Qname;
    public static int QButtonClick = -1;


    public void changeMainScene()
    {
        //gameman.Instance.updatecompet();
        SceneManager.LoadScene("02.Main");
        
    }

    public void changeSettingScene()
    {
        SceneManager.LoadScene("03.Setting");
    }

    public void changeadminScene()
    {
        SceneManager.LoadScene("ContestList");
    }

    public void ChangeSceneToQuizMenu()
    {
        SceneManager.LoadScene("QuizMenu");//Quiz리스트 화면
    }

    public void ChangeSceneToContestSetting()
    {
        SceneManager.LoadScene("ContestMenu");//대회 세팅 화면
    }

    public void ChangeSceneToCStartEnd()
    {
        SceneManager.LoadScene("CStartEnd");//시작끝시간 페이지 화면
    }

    public void ChangeSceneToRealTimeR()
    {
        SceneManager.LoadScene("RealTimeR");//실시간 랭킹 화면
    }

    public void ChangeSceneToAdMenu()
    {
        SceneManager.LoadScene("administratorMenu"); //관리자 메뉴 화면
    }
    public void ChangeSceneToAdMenu(Button button)
    {
        Qname = button.GetComponentInChildren<Text>().text;
        Debug.Log(Qname);
        ChangeSceneToAdMenu();
    }
    public void ChangeSceneToPWChange()
    {
        SceneManager.LoadScene("PWChange");//비밀번호 변경화면
    }


    public void ChangeSceneToModeChange()
    {
        SceneManager.LoadScene("ModeChange");//모드 변경화면
    }

    public void ChangeSceneToContestClosed()
    {
        SceneManager.LoadScene("ContestClosed");//대회폐쇄화면
    }

    public void ChangeSceneToQuizType()
    {
        SceneManager.LoadScene("QuizType");//Quiz type설정 화면
    }

    public void ChangeSceneToQuizAdd()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz Add 화면
    }
    
    public void ChangeSceneToQuizAddToChange()
    {
        SceneManager.LoadScene("QuizAdd");//QuizMenu에서 바로 Quiz Add 화면
    }//onclicks에 quizchage랑 연결
    /*
    public void OnClicked(Button button)
    {
        Qname=button.GetComponentInChildren<Text>().text;
        Debug.Log(Qname);
        ChangeSceneToQuizAddToChange();
    }*/

    public void OnClicked(GameObject gameObject)
    {
        Qname = gameObject.GetComponentInChildren<Text>().text;
        Debug.Log(Qname);
        ChangeSceneToQuizAddToChange();
    }

    public void Loading()
    {
        SceneManager.LoadScene("loading");
    }
}
