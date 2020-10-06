using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenechange :MonoBehaviour
{ 
    public static int QButtonClick = -1;

    public void changeMainScene()
    {
        SceneManager.LoadScene("02.Main");
        BackSpace.Instance.Clear();
    }

    public void changeadminScene()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ContestList");
    }

    public void ChangeSceneToAdMenu()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("administratorMenu"); //관리자 메뉴 화면
    }

    public void ChangeSceneToQuizMenu()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizMenu");//Quiz리스트 화면
    }

    public void ChangeSceneToContestSetting()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ContestMenu");//대회 세팅 화면
    }

    public void ChangeSceneToCStartEnd()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("CStartEnd");//시작끝시간 페이지 화면
    }

    public void ChangeSceneToRealTimeR()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("RealTimeR");//실시간 랭킹 화면
    }

    
    public void ChangeSceneToAdMenu(GameObject gameObject)
    {
        //Qname = gameObject.GetComponentInChildren<Text>().text;
        AdminCurState.Instance.Competition = gameObject.GetComponentInChildren<Text>().text;
        ChangeSceneToAdMenu();
    }

    public void ChangeSceneToContestClosed()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ContestClosed");//대회폐쇄화면
    }

    public void ChangeSceneToQuizType()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizType");//Quiz type설정 화면
    }

    public void ChangeSceneToQuizAdd()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizAdd");//Quiz Add 화면
    }
    
    public void ChangeSceneToQuizAddToChange()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizAdd");//QuizMenu에서 바로 Quiz Add 화면
    }//onclicks에 quizchage랑 연결

    public void OnClicked(GameObject gameObject)
    {
        //Qname = gameObject.GetComponentInChildren<Text>().text;
        AdminCurState.Instance.Quiz = gameObject.GetComponentInChildren<Text>().text;
    }

    public void Loading()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("loading");
    }

    public void ChangeSceneToQuizAdd(int kind)
    {
        PlayerPrefs.SetInt("ButtonClick", kind);
        ChangeSceneToQuizAdd();
    }
}
