using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.SceneManagement;

public class Out : MonoBehaviour
{
    public void FirstPa()
    {
        SceneManager.LoadScene("02.start");
        gameman.Instance.sfaudio.Stop();
    }

    // 회원 탈퇴 
    public void SignOut()
    {
        Debug.Log("-------------SignOut-------------");
        Backend.BMember.SignOut();
    }

    //로그아웃
    public void LogOut()
    {

        Debug.Log("-------------LogOut-------------");
        Backend.BMember.Logout();
    }
}
