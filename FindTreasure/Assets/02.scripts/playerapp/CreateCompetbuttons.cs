using Michsky.UI.Shift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System;

public class CreateCompetbuttons : MonoBehaviour
{
    public rank Rank; //rank.cs 에 접촉
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

    void Awake()
    {
        //m_ClickAction += sfxmusic.Go;
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
                    ConName.Add(title);/////////////////추가
                }
            }
            buttons.Add(b);

            b.GetComponent<Button>().onClick.AddListener(() =>PlayerContents.Instance.ClickListener(title));
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
}
