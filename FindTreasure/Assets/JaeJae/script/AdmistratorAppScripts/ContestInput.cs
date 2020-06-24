using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TTM.Classes;
using UnityEngine.SceneManagement;

public class ContestInput : MonoBehaviour
{
    public GameObject ContestPanel;
    public InputField ContestName_infT;
    public Dropdown ContestTN_dbox;
    public InputField ContestPw_infT;
    public Toggle Team;
    public Toggle individual;
    public GameObject ContestBPrefab; //대회버튼
    public GameObject Content;
    public GameObject all_t;
    int nextNumber;

    List<GameObject> ContestList = new List<GameObject>();

    //Toggle Changed Func
    public void HideTeamOption() 
    {
        if (Team.isOn)
        {
            ContestTN_dbox.enabled = true;
            //Toggle status graphic
        }
        else
        {
            ContestTN_dbox.enabled = false;
        }
    }
    
    //Save Button Click Func
    public void SaveContest()
    {
        Competition compet = new Competition();

        if (ContestName_infT.text.Length < 1)
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            return;
        }

        if (ContestPw_infT.text.Length < 1)
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            return;
        }

        compet.Mode = Team.isOn;

        if (compet.Mode)
        {
            compet.MaxMember = ContestTN_dbox.value + 2;
        }

        compet.Password = ContestPw_infT.text;

        GetComponent<CompetDic>().AddContest(ContestName_infT.text,compet);

        ContestList.Add(MakeCompetButton(ContestName_infT.text));

        ShowAddPanel(false);
        
    }

    public void LoadCompets()
    {
        List<string> vs = GetComponent<CompetDic>().getCurrentList();
        if (vs != null)
            foreach (string item in vs)
            {
                ContestList.Add(MakeCompetButton(item));
            }
    }

    private GameObject MakeCompetButton(string name)
    {
        GameObject competb = Instantiate(ContestBPrefab, ContestBPrefab.transform.position, Quaternion.identity);
        //위치 조정
        //competb.GetComponent<RectTransform>().anchoredPosition.Set(0,ContestList.Count*120);
        competb.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, (ContestList.Count+1) * 135);
        //글씨 조정
        competb.GetComponentInChildren<Text>().text = name;
        competb.transform.SetParent(Content.transform, true);

        return competb;
    }
    public void ShowAddPanel(bool a)
    {
        ContestPanel.SetActive(a);
    }

    public void Start()
    {
        ShowAddPanel(false);
        all_t.SetActive(false);

        LoadCompets();
        
    }

    public void Update()
    {
        if (ContestList.Count == 0)
            LoadCompets();
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("02.Main");
                Destroy(adminManager.Instance.gameObject);
            }
        }
        if (Input.GetKey(KeyCode.Escape))

        {
            SceneManager.LoadScene("02.Main");
            //Destroy(adminManager.Instance.gameObject);
        }
    }
    
    public void Cancel()
    {
        ContestName_infT.text = "";
        ContestPw_infT.text = "";
        Team.isOn = false;
        individual.isOn = true;
        ContestTN_dbox.SetValueWithoutNotify(0);
    }

    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
    
}
