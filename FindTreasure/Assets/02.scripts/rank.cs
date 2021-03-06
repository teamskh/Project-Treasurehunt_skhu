﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class rank : MonoBehaviour
{
    public GameObject statusbar;
    public GameObject Endingmess;

    public Text endtimeT; //상당바에 나오는 시간
    public static TimeSpan conTime;
    private TimeSpan Times=new TimeSpan(0,0,1);
    

    public Text Timet;
    public GameObject Etime; //종료 메시지
    public Text Scoret;
    public GameObject Escore;
    private int num = 0;

    public void OnEnable()
    {
        Sign();
    }

    public void Sign()
    {
        InvokeRepeating("Timer", 0.1f, 1f);
    }

    public void Gohome()
    {
        SceneManager.LoadScene("MainLoading");
    }

    void Timer()
    {
        conTime = conTime - Times;
        endtimeT.text = $"{conTime.Days}일 {conTime.Hours}시간 {conTime.Minutes}분 {conTime.Seconds}초";
        if (conTime.TotalSeconds<1)
        {
            CancelInvoke("Timer");
            endtimeT.text = "0일 0시간 0분 0초";
            Scoret.text = Player.Instance.score.ToString();

            Player.Instance.FinishCompets();

            Endingmess.SetActive(true);
            Escore.SetActive(true);
        }
        else if(Player.Instance.score == gameman.Instance.EndScore)
        {
            CancelInvoke("Timer");
            Timet.text = endtimeT.text;

            Player.Instance.FinishCompets();

            Endingmess.SetActive(true);
            Etime.SetActive(true);

        }
    }
}
