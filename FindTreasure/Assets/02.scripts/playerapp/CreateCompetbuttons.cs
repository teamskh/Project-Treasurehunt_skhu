﻿using Michsky.UI.Shift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CreateCompetbuttons : MonoBehaviour
{
    public GameObject Competb;

    private List<string> curlist;
    private List<GameObject> buttons = new List<GameObject>();

    private UnityAction m_ClickAction;

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

    /*
    private CompetDic scripts;

    private void Start()
    {
        
        scripts = gameObject.AddComponent<CompetDic>();
    }

    */
    // Start is called before the first frame update
    void Awake()
    {
        m_ClickAction += sfxmusic.start;
        m_ClickAction += SetActive;
        //m_ClickAction += score.click;
        //curlist = gameman.Instance.GetList(); gameman 연관 수정 필요 
        foreach (string title in curlist) {
            GameObject b = Instantiate(Competb, transform);
            b.GetComponent<RectTransform>().anchoredPosition.Set(0, (buttons.Count+1) * 60);
            foreach(Text t in b.GetComponentsInChildren<Text>()){
                if (t.gameObject.layer == 8) t.text = title;
            }
            buttons.Add(b);
            b.GetComponent<Button>().onClick.AddListener(m_ClickAction);
        }
    }
    
    void SetActive()
    {
        List.SetActive(false);
        backmid.SetActive(false);
        bar.SetActive(true);
        rankButton.SetActive(true);
    }

}
