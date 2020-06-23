using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

using DataInfo;

using TTM.Classes;
using TTM.Save;
using BackEnd;

public class rank : MonoBehaviour
{
    public Text Name;
    public Text NowTime;
    public InputField ConTestNa;
    private TimeSpan times;

    public GameObject endTime;
    public GameObject endScore;

    public Text endtimeT;
    public Text endscoreT;

    private void Start()
    {
        Name.text = PlayerPrefs.GetString("nickna");
        gameman.Instance.score = 0;
        StartCoroutine(CountTime());
    }

    public void Loadrank()
    {
        gameman.Instance.loadRankChek = true;
        Debug.Log("create");
    }

    public void GetConname()
    {

    }

    public void SaveTS()
    {
        if (gameman.Instance.score==30) //시간 끝! 서버에서 갖고와 시간
        {
            endscoreT.text = gameman.Instance.score.ToString();
            endScore.SetActive(false);
        }
        else  //목표 점수 도달 if문 점수
        {
            endtimeT.text = gameman.Instance.endtime;
            endTime.SetActive(false);
        }
    }

    IEnumerator CountTime()
    {
        while (true)
        {
            DateTime str = DateTime.Now;
            Debug.Log(str);
            yield return new WaitForSeconds(1);
        }
    }
}
