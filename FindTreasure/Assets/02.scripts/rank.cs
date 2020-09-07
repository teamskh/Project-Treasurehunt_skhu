using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class rank : MonoBehaviour
{
    public Text Name;
    //private TimeSpan times;

    public GameObject endTime;
    public GameObject endScore;

    public GameObject statusbar;
    public GameObject Endingmess;

    public Text endtimeT;
    public Text endscoreT;
    public bool openendmess=false;

    private IEnumerator corutuine;

    public string test;

    private void Start()
    {
        Name.text = PlayerPrefs.GetString("nickna");
        corutuine = CountTime();
        
    }

    public void StartCo() //대회버튼이 눌려 대회시간 돌아가기
    {
        StartCoroutine(CountTime());
        StartCoroutine(corutuine);
    }

    public void Loadrank()
    {
        gameman.Instance.loadRankChek = true;
        Debug.Log("create");
    }

    public void Close()
    {
        openendmess = true;
        gameman.Instance.loadRankChek = true;
    }

   void Update()
    {
        if(openendmess==true)
            StopCoroutine(corutuine);
    }

    public IEnumerator CountTime()
    {
        while (true)
        {
            //if (gameman.Instance.che == true)
            //{
                TimeSpan times = gameman.Instance.endtime - DateTime.Now;
                test = $"{times.Days}일 {times.Hours}시간 {times.Minutes}분 {times.Seconds}초";
                if (times.Seconds < 1)
                {
                    statusbar.SetActive(false);
                    
                    Endingmess.SetActive(true);

                    openendmess = true;
                    
                    endTime.SetActive(false);
                    //gameman.Instance.endingTime = " ";
                    endscoreT.text = gameman.Instance.score.ToString();
                }
                else
                {
                    if (gameman.Instance.score == gameman.Instance.EndScore) //서버에서 점수 가져오기
                    {
                        statusbar.SetActive(false);
                        Endingmess.SetActive(true);
                        endScore.SetActive(false);
                        gameman.Instance.score = 30;
                        endtimeT.text = gameman.Instance.endingTime;
                    }
                }
                endtimeT.text = test; //남은 시간

                gameman.Instance.endingTime = test;
            //}
            /*
            else
            {
                DateTime str = DateTime.Now;
                endtimeT.text = str.ToString();
            }*/
            yield return new WaitForSeconds(1);
        }
    }
}
