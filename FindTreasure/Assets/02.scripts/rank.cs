using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class rank : MonoBehaviour
{
    // public GameObject endTime;
    //public GameObject endScore;
    
    public GameObject statusbar;
    public GameObject Endingmess;

    public Text endtimeT;
    public static TimeSpan conTime;
    private TimeSpan Times=new TimeSpan(0,0,1);
    
    public void OnEnable()
    {
        Sign();
    }

    public void Sign()
    {
        InvokeRepeating("Timer", 0.1f, 1f);
    }

    public void Close()
    {
        gameman.Instance.loadRankChek = true;
    }

    public void Gohome()
    {
        SceneManager.LoadScene("MainLoading");
    }

    void Timer()
    {
        conTime = conTime - Times;
        endtimeT.text = $"{conTime.Days}일 {conTime.Hours}시간 {conTime.Minutes}분 {conTime.Seconds}초";
        if (conTime.Seconds<0)
        {
            endtimeT.text = "0일 0시간 0분 0초";
            Endingmess.SetActive(true);
            CancelInvoke("Timer");
        }
    }
}
