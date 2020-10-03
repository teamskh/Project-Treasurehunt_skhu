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
    
    //public bool openendmess=false; //대회 종료

    private IEnumerator corutuine;

    public string test;

    public void Start()
    {
        Name.text = PlayerPrefs.GetString("nickna","no");
    }

    public void Sign() //대회버튼이 눌려 대회시간 돌아가기
    {
        TimeSpan times = gameman.Instance.endtime - DateTime.Now;
        test = $"{times.Days}일 {times.Hours}시간 {times.Minutes}분 {times.Seconds}초";
        endtimeT.text = test;
        Debug.Log(test);
    }
    
    public void Loadrank()
    {
        gameman.Instance.loadRankChek = true;
        Debug.Log("create");
    }

    public void Close()
    {
        //openendmess = true;
        gameman.Instance.loadRankChek = true;
    }

   /*void Update()
    {
        if(openendmess==true)
            StopCoroutine(corutuine);
    }*/

    public IEnumerator CountTime()
    {
        while (true)
        {
            //if (gameman.Instance.che == true)
            //{
                /*TimeSpan times = gameman.Instance.endtime - DateTime.Now;
                test = $"{times.Days}일 {times.Hours}시간 {times.Minutes}분 {times.Seconds}초";
                if (times.Seconds < 1) //시간 종료
                {
                    statusbar.SetActive(false);
                    
                    Endingmess.SetActive(true);

                    openendmess = true;
                    
                    endTime.SetActive(false);
                    endscoreT.text = gameman.Instance.score.ToString();
                }
                else if (gameman.Instance.score == gameman.Instance.EndScore) //최대점수 도달
                {
                    statusbar.SetActive(false);
                    Endingmess.SetActive(true);
                    endScore.SetActive(false);
                    endtimeT.text = gameman.Instance.endingTime;
                }
                endtimeT.text = test; //남은 시간

                gameman.Instance.endingTime = test;
                */
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
