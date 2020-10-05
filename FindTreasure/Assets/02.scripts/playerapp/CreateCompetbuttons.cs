using Michsky.UI.Shift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System;

public class CreateCompetbuttons : MonoBehaviour
{
    public GameObject Competb;

    private List<string> curlist;
    private List<GameObject> buttons = new List<GameObject>();

    private UnityAction m_ClickAction;

    public List<string> ConName;

    public static bool che = false;

    public rank Ranking = new rank();
    QuizList qlist = new QuizList();

    [SerializeField]
    GameObject List;

    [SerializeField]
    GameObject backmid;

    [SerializeField]
    sfxmusic sfxmusic;

    [SerializeField]
    GameObject bar;

    [SerializeField]
    GameObject rankButton;

    [SerializeField]
    GameObject homeButton;

    [SerializeField]
    GameObject NoticeEnd;

    [SerializeField]
    GameObject NoticeOpen;

    [SerializeField]
    GameObject PassOpen;

    void Awake()
    {
        //m_ClickAction += sfxmusic.Go;
        //m_ClickAction += score.click;
       

        curlist = PlayerContents.Instance.CompetitionList();
        foreach (string title in curlist) {
            GameObject b = Instantiate(Competb, transform);
            b.GetComponent<RectTransform>().anchoredPosition.Set(0, (buttons.Count+1) * 60);
            foreach(Text t in b.GetComponentsInChildren<Text>()){
                if (t.gameObject.layer == 8)
                {
                    t.text = title;
                    ConName.Add(title);
                }
            }
            buttons.Add(b);
            b.GetComponent<Button>().onClick.AddListener(() =>Notice(title));
        }
    }

    void Notice(string compet)
    {
        TimeSpan St = PlayerContents.Instance.startTimelimit(compet);
        TimeSpan Ed = PlayerContents.Instance.endTimelimit(compet);

        if (St.Seconds > 0) //현재 시간이 종료시간보다 전이면 팝업 띄우기
            SetActiveOpenNotice();

        else if (Ed.Seconds < 0) //현재 시간이 종료시간보다 지났으면 팝업 띄우기
            SetActiveEndNotice();

        else
        {
            PlayerContents.Instance.ClickListener(compet);
            AvailableAR.MakeAct();
            var TrackManager = GameObject.Find("AR Session Origin");
            if (TrackManager != null) TrackManager.AddComponent<TrackedImageInfoManager>();
            //비밀번호 UI 띄우기
            if(che == false)
            {
                PassOpen.SetActive(true);
            }
            else if(che == true){
                InvokeRepeating("Timer", 0.1f, 1f);
                SetActive();
                che = false;
                //Ranking.Sign();
            }
        }
    }

    void SetActive()
    {
        List.SetActive(false);
        backmid.SetActive(false);
        bar.SetActive(true);
        rankButton.SetActive(true);
        homeButton.SetActive(true);
        Debug.Log("click");
    }

    void SetActiveOpenNotice() //대회 시작 전 팝업
    {
        Debug.Log("1");
        NoticeOpen.SetActive(true);
        NoticeEnd.SetActive(false);
    }

    void SetActiveEndNotice() //이미 종료된 팝업
    {
        Debug.Log("2");
        NoticeOpen.SetActive(false);
        NoticeEnd.SetActive(true);
    }

    void Timer()
    {
        //Ranking.Sign();
        TimeSpan times = gameman.Instance.endtime - DateTime.Now;
        string test = $"{times.Days}일 {times.Hours}시간 {times.Minutes}분 {times.Seconds}초";
        Debug.Log(test);
        Ranking.Sign(test);
    }
}
