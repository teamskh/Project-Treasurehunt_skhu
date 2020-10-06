using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class rank : MonoBehaviour
{
    public GameObject statusbar;
    public GameObject Endingmess;

    public Text endtimeT;
    public static TimeSpan conTime;
    private TimeSpan Times=new TimeSpan(0,0,1);
    Player player = new Player();

    public Text EndTt;
    public Text EndSt;

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
        //gameman.Instance.loadRankChek = true;

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
            CancelInvoke("Timer");
            endtimeT.text = "0일 0시간 0분 0초";
            EndSt.text = player.score.ToString();
            transform.Find("endingScore").gameObject.SetActive(true);
            Endingmess.SetActive(true);
        }
        /*
        else if(player.score == gameman.Instance.EndScore)
        {
            CancelInvoke("Timer");
            EndTt.text = endtimeT.text;
            transform.Find("endingTime").gameObject.SetActive(true);
            Endingmess.SetActive(true);
        }*/
        Debug.Log(player.score);
    }
}
