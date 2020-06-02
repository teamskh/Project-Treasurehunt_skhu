﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
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

    public void ChangeSceneToPWChange()
    {
        SceneManager.LoadScene("PWChange");//비밀번호 변경화면
    }
   
    public void ChangeSceneToContestSettingChange()
    {
        //Application.LoadLevel("CSChange");//대회 정보 수정화면
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

    public void ChangeSceneToContestList()
    {
        SceneManager.LoadScene("ContestList");//Quiz Add 화면
    }
}
