﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenechange :MonoBehaviour
{ 
    public static string Qname;
    public static int QButtonClick = -1;

    //public static Stack mUIStack = new Stack();

    public void changeMainScene()
    {
        //gameman.Instance.updatecompet();
        SceneManager.LoadScene("02.Main");
        BackSpace.Instance.Clear();
    }

    public void changeadminScene()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ContestList");
        Debug.Log(BackSpace.Instance.ToString());
    }

    public void ChangeSceneToAdMenu()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("administratorMenu"); //관리자 메뉴 화면
        Debug.Log(BackSpace.Instance.ToString());
    }

    public void ChangeSceneToQuizMenu()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizMenu");//Quiz리스트 화면
        Debug.Log(BackSpace.Instance.ToString());
    }

    public void ChangeSceneToContestSetting()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ContestMenu");//대회 세팅 화면
        Debug.Log(BackSpace.Instance.ToString());
    }

    public void ChangeSceneToCStartEnd()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("CStartEnd");//시작끝시간 페이지 화면
        Debug.Log(BackSpace.Instance.ToString());
    }

    public void ChangeSceneToRealTimeR()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("RealTimeR");//실시간 랭킹 화면
    }

    
    public void ChangeSceneToAdMenu(GameObject gameObject)
    {
        Qname = gameObject.GetComponentInChildren<Text>().text;
        Debug.Log(Qname);
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
        Debug.Log(BackSpace.Instance.ToString());
    }

    public void ChangeSceneToQuizAdd()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizAdd");//Quiz Add 화면
        Debug.Log(BackSpace.Instance.ToString());
    }
    
    public void ChangeSceneToQuizAddToChange()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("QuizAdd");//QuizMenu에서 바로 Quiz Add 화면
        Debug.Log(BackSpace.Instance.ToString());
    }//onclicks에 quizchage랑 연결

    public void OnClicked(GameObject gameObject)
    {
        Qname = gameObject.GetComponentInChildren<Text>().text;
        Debug.Log(Qname);
        ChangeSceneToQuizAddToChange();
    }

    public void Loading()
    {
        BackSpace.Instance.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("loading");
        Debug.Log(BackSpace.Instance.ToString());
    }

    public void ChangeSceneToQuizAdd(int kind)
    {
        PlayerPrefs.SetInt("ButtonClick", kind);
        ChangeSceneToQuizAdd();
    }
}
