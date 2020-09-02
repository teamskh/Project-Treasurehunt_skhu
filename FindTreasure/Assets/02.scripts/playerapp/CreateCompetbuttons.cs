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

    private Dictionary<String,int> FindTime = new Dictionary<String,int>(); //int datetime으로 수정

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

    void Awake()
    {
        //m_ClickAction += sfxmusic.Go;
        m_ClickAction += SetActive;
        //m_ClickAction += score.click;
        curlist = PlayerContents.Instance.CompetitionList();
        foreach (string title in curlist) { //title이 버튼 이름
            GameObject b = Instantiate(Competb, transform);
            b.GetComponent<RectTransform>().anchoredPosition.Set(0, (buttons.Count+1) * 60);
            foreach(Text t in b.GetComponentsInChildren<Text>()){
                if (t.gameObject.layer == 8)
                {
                    t.text = title;
                }
            }
            buttons.Add(b);
            FindTime.Add(title,0); //대회이름과 날짜 저장 0을 변경 얘를 안쓰고 서버에서 그냥 받아도 될것 같은데?

            b.GetComponent<Button>().onClick.AddListener(() =>PlayerContents.Instance.ClickListener(title));
            //b.GetComponent<Button>().onClick.AddListener(()=> b.GetComponentInChildren<Text>());
            b.GetComponent<Button>().onClick.AddListener(m_ClickAction);
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

    void SetendTime()
    {
        List<string> conlist = new List<string>(FindTime.Keys);

        foreach(string k in conlist)
        {
            if (k == "gogo")//gogo 부분에 버튼 이름이 들어간다.
            {
                //FindTime[k] 에서 종료 시간과 점수를 가져와야 함
                //gameman.Instance.endtime = FindTime[k];
                gameman.Instance.EndScore = 0; //0부분에 서버에 올려진 대회 점수를 가져와야함
                Debug.Log("hih");
            }
        }

        gameman.Instance.start = true; //대회 버튼을 눌렀다! -> rank.cs에 코루틴이 돌아감 시간 시작
    }
}
