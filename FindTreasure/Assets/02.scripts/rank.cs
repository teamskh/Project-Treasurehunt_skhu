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
    private TimeSpan times;

    public GameObject endTime;
    public GameObject endScore;

    public GameObject statusbar;
    public GameObject Endingmess;

    public Text endtimeT;
    public Text endscoreT;
    public bool openendmess=false;

    public string test;

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

    public void Close()
    {
        openendmess = false;
    }

    IEnumerator CountTime()
    {
        while (true)
        {
            if (gameman.Instance.che == true)
            {
                TimeSpan times = gameman.Instance.endtime - DateTime.Now;
                test = $"{times.Days}일 {times.Hours}시간 {times.Minutes}분 {times.Seconds}초";
                if (times.Seconds < 2)
                {
                    statusbar.SetActive(false);다

                    if (openendmess == true)
                    {
                        Endingmess.SetActive(true); //모르겠
                    }
                    endTime.SetActive(false);
                    endscoreT.text = gameman.Instance.score.ToString();
                }
                else
                {
                    if (gameman.Instance.score == 30)
                    {
                        statusbar.SetActive(false);
                        Endingmess.SetActive(true);
                        endScore.SetActive(false);
                        endtimeT.text = gameman.Instance.endingTime;
                    }
                }
                endtimeT.text = test;
                gameman.Instance.endingTime = test;
            }
            else
            {
                DateTime str = DateTime.Now;
                endtimeT.text = str.ToString();
            }
            yield return new WaitForSeconds(1);
        }
    }
}
