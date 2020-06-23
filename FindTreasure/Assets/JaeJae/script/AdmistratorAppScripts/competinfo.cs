﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TTM.Classes;

public class competinfo : MonoBehaviour
{
    public InputField ContestName_infT;
    public InputField ContestInfo_infT;
    public InputField authen_infT;
    public Dropdown ContestTN_dbox;
    public InputField ContestPw_infT;
    public Toggle Team;
    public Toggle individual;
    public GameObject all_t;
    public GameObject Panel;
    public GameObject Panel2;
    public GameObject Panel3;
    string key;
    string title;
    Competition compet = new Competition();
    public void Start()
    {
        all_t.SetActive(false);
        key = scenechange.Qname;
        if (adminManager.Instance.CallCompetDic().TryGetValue(key, out compet))
        {
            Debug.Log(compet.Mode);
            ContestPw_infT.text = compet.Password;
            if (compet.Mode == true)
            {
                Team.isOn = true;
                ContestTN_dbox.value = compet.MaxMember - 2;
            }
            else
            {
                Team.isOn = false;
            }

            Debug.Log(compet.Password);
            ContestName_infT.text = key;
            if (compet.Info != null)
            {
                ContestInfo_infT.text = compet.Info;
            }
            if (compet.Userword.ToString() != null)
            {
                authen_infT.text = compet.Userword.ToString();
            }
        }
    }
    public void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("ContestList");
            }
        }
        if (Input.GetKey(KeyCode.Escape))

        {
            SceneManager.LoadScene("ContestList");
        }
    }
    public void PasswordSave()
    {
        if (ContestPw_infT.text.Length < 1)
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            return;
        }
        compet.Password= ContestPw_infT.text;
        Debug.Log(compet.Password);
        adminManager.Instance.GetComponent<CompetDic>().DelCompt(key);
        adminManager.Instance.GetComponent<CompetDic>().AddContest(key, compet);
        Panel.SetActive(false);
    }
    public void memberNumberSave()
    {
        if (compet.Mode == true)
        {
            Team.isOn = true;
            compet.MaxMember = ContestTN_dbox.value + 2;
        }
        else
        {
            Team.isOn = false;
            compet.MaxMember = 1;
        }
        adminManager.Instance.GetComponent<CompetDic>().DelCompt(key);
        adminManager.Instance.GetComponent<CompetDic>().AddContest(key, compet);
        Panel2.SetActive(false);
    }
    public void competInfoSave()
    {
        title = ContestName_infT.text;
        compet.Info= ContestInfo_infT.text;
        compet.Userword= int.Parse(authen_infT.text);
        adminManager.Instance.GetComponent<CompetDic>().DelCompt(key);
        adminManager.Instance.GetComponent<CompetDic>().AddContest(title, compet);
        Panel3.SetActive(false);
    }
    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
}
