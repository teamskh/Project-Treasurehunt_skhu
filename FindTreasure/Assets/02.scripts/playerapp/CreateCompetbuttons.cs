using Michsky.UI.Shift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System;

public class CreateCompetbuttons : MonoBehaviour
{
    //public rank Rank; //rank.cs 에 접촉
    public GameObject Competb;

    private List<string> curlist;
    private List<GameObject> buttons = new List<GameObject>();

    private UnityAction m_ClickAction;

    public List<string> ConName;

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
    GameObject NoticeTime;

    [SerializeField]
    GameObject NoticeEnd;

    [SerializeField]
    GameObject NoticeOpen;

    void Awake()
    {
        //m_ClickAction += sfxmusic.Go;
        if (gameman.Instance.Opentime > DateTime.Now) //현재 시간이 종료시간보다 전이면 팝업 띄우기
            m_ClickAction += SetActiveOpebNotice;
        else if (gameman.Instance.endtime < DateTime.Now) //현재 시간이 종료시간보다 지났으면 팝업 띄우기
            m_ClickAction += SetActiveEndNotice;
        else
            m_ClickAction += SetActive;
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

            b.GetComponent<Button>().onClick.AddListener(() =>PlayerContents.Instance.ClickListener(title));
            b.GetComponent<Button>().onClick.AddListener(() =>AvailableAR.MakeAct());
            b.GetComponent<Button>().onClick.AddListener(m_ClickAction);
            
            var TrackManager = GameObject.Find("AR Session Origin");
            if(TrackManager!=null)
            b.GetComponent<Button>().onClick.AddListener(() => TrackManager.AddComponent<TrackedImageInfoManager>());
        }
    }
    
    void SetActive()
    {
        List.SetActive(false);
        backmid.SetActive(false);
        bar.SetActive(true);
        rankButton.SetActive(true);
        Debug.Log("click");
    }

    void SetActiveOpebNotice() //대회 시작 전 팝업
    {
        NoticeTime.SetActive(true);
        NoticeOpen.SetActive(true);
        NoticeEnd.SetActive(false);
    }

    void SetActiveEndNotice() //이미 종료된 팝업
    {
        NoticeTime.SetActive(true);
        NoticeOpen.SetActive(false);
        NoticeEnd.SetActive(true);
    }
}
