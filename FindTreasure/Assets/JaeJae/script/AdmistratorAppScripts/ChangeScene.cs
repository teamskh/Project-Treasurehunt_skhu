using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   public void ChangeSceneToQuizMenu()
    {
        Application.LoadLevel("QuizMenu");//Quiz리스트 화면
    }

    public void ChangeSceneToContestSetting()
    {
        Application.LoadLevel("ContestMenu");//대회 세팅 화면
    }

    public void ChangeSceneToCStartEnd()
    {
        Application.LoadLevel("CStartEnd");//시작끝시간 페이지 화면
    }

    public void ChangeSceneToRealTimeR()
    {
        Application.LoadLevel("RealTimeR");//실시간 랭킹 화면
    }

    public void ChangeSceneToAdMenu()
    {
        Application.LoadLevel("administratorMenu"); //관리자 메뉴 화면
    }

    public void ChangeSceneToPWChange()
    {
        Application.LoadLevel("PWChange");//비밀번호 변경화면
    }

    public void ChangeSceneToContestSettingChange()
    {
        Application.LoadLevel("CSChange");//대회 정보 수정화면
    }

    public void ChangeSceneToModeChange()
    {
        Application.LoadLevel("ModeChange");//모드 변경화면
    }

    public void ChangeSceneToContestClosed()
    {
        Application.LoadLevel("ContestClosed");//대회폐쇄화면
    }

    public void ChangeSceneToQuizType()
    {
        Application.LoadLevel("QuizType");//Quiz type설정 화면
    }

    public void ChangeSceneToQuizAdd()
    {
        Application.LoadLevel("QuizAdd");//Quiz Add 화면
    }

    public void ChangeSceneToContestList()
    {
        Application.LoadLevel("ContestList");//Quiz Add 화면
    }
}
