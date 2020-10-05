using Michsky.UI.Shift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System;
using TTM.Classes;

public class CreateCompetbuttons : MonoBehaviour
{
    public GameObject Competb;

    private List<string> curlist;
    private List<GameObject> buttons = new List<GameObject>();

    private UnityAction m_ClickAction;

    public List<string> ConName;

    public static bool che = false;

    //rank Ranking;
    QuizList qlist = new QuizList();

    //password에 필요
    CompetitionDictionary dic;
    Competition comp;
    [SerializeField]
    GameObject all_t, re, closePass;

    [SerializeField]
    InputField input;

    [SerializeField]
    Button YES_b;
    private string key;

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
        //YES_b = GameObject.Find("YES")?.GetComponent<Button>();
        Debug.Log(YES_b);

        TimeSpan St = PlayerContents.Instance.startTimelimit(compet);
        TimeSpan Ed = PlayerContents.Instance.endTimelimit(compet);

        if (St.Seconds > 0) //현재 시간이 종료시간보다 전이면 팝업 띄우기
            SetActiveOpenNotice();

        else if (Ed.Seconds < 0) //현재 시간이 종료시간보다 지났으면 팝업 띄우기
            SetActiveEndNotice();

        else
        {
            all_t.SetActive(false);
            re.SetActive(false);
            PassOpen.SetActive(true);
            //비밀번호 UI 띄우기
            YES_b?.onClick.AddListener(() => Password(compet));
            PlayerContents.Instance.ClickListener(compet);
            AvailableAR.MakeAct();
        }
    }

    void Password(string title)
    {
        dic = new CompetitionDictionary();
        dic.GetCompetitions();
        key = title;
        dic.TryGetValue(key, out comp);
        
        if (input.text == comp.Password)
        {
            closePass.SetActive(false);

            //InvokeRepeating("Timer", 0.1f, 1f);

            rank.conTime = PlayerContents.Instance.endTimelimit(title);
            SetActive();

            /*PlayerContents.Instance.ClickListener(title);
            AvailableAR.MakeAct();
            var TrackManager = GameObject.Find("AR Session Origin");
            if (TrackManager != null) TrackManager.AddComponent<TrackedImageInfoManager>();*/
        }
        else if (input.text.Length < 1)
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            return;
        }
        else
        {
            StartCoroutine(setActiveObjinSecond(re, 1f));
            return;
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
        NoticeOpen.SetActive(true);
        NoticeEnd.SetActive(false);
    }

    void SetActiveEndNotice() //이미 종료된 팝업
    {
        NoticeOpen.SetActive(false);
        NoticeEnd.SetActive(true);
    }

    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
}
