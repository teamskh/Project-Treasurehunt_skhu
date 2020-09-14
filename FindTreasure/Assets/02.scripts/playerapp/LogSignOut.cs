using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogSignOut : MonoBehaviour
{
    public gameman manager;
    // 첫 화면으로 돌아가면 음악이 두번 겹치므로 배경음악과 효과음을 제거해준다.
    public void DesMusic()
    {
        Destroy(gameman.Instance.sfaudio);
        Destroy(gameman.Instance.baaudio);
        Debug.Log("음악 제거");
        //배경음악 제거
        //효과음 제거
    }

    //  회원 탈퇴
    public void Signout()
    {
        manager.SignOut();
        Debug.Log("탈퇴");
        DesMusic();
    }

    // 로그아웃
    public void LogOut()
    {
        manager.Logout();
        Debug.Log("로그아웃");
        DesMusic();
    }
}
