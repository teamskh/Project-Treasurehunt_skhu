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
    private UnityAction m_Test; //시험

    public String te;
    public Text test;

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
        m_Test += Showing;
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
            te = title; // 버튼 이름 사용하기

            
            b.GetComponent<Button>().onClick.AddListener(m_ClickAction); //얘처럼 한줄 더


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

    void Showing()
    {
        Debug.Log(te);
        
    }
}
