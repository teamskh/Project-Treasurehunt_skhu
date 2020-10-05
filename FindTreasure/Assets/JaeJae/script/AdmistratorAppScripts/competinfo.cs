using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTM.Classes;
using BackEnd;

public class competinfo : MonoBehaviour
{
    [SerializeField]
    InputField ContestName_infT, ContestInfo_infT, authen_infT, ContestPw_infT;
    [SerializeField]
    Dropdown ContestTN_dbox;
    [SerializeField]
    Toggle Team, individual;
    [SerializeField]
    GameObject all_t;

    string key;
    public string title;

    Competition compet = new Competition();
    CompetitionDictionary dic;
    public void Start()
    {
        all_t.SetActive(false);
        key = AdminCurState.Instance.Competition;
    }
    public void PasswordLoad()
    {
        dic = new CompetitionDictionary();
        dic.GetCompetitions();
        if (dic.TryGetValue(key, out compet))
        {
            ContestPw_infT.text = compet.Password;
        }
    }
    
    public void memberNumberLoad()
    {
        dic = new CompetitionDictionary();
        dic.GetCompetitions();
        if (dic.TryGetValue(key, out compet))
        {
            if (compet.Mode == true)
            {
                Team.isOn = true;
                individual.isOn = false;
                ContestTN_dbox.value = compet.MaxMember - 2;
            }
            else
            {
                individual.isOn = true;
                Team.isOn = false;
            }
        }
    }

    public void competInfoLoad()
    {
        dic = new CompetitionDictionary();
        dic.GetCompetitions();
        if (dic.TryGetValue(key, out compet))
        {
            ContestName_infT.text = key;
            if (compet.Info != null)
            {
                ContestInfo_infT.text = compet.Info;
            }
            if (compet.UserPass.ToString() != null)
            {
                authen_infT.text = compet.UserPass.ToString();
            }
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
        Param param = new Param();
        param.CompetPassword(compet.Password)
            .CompetUpdate();
    }
    public void memberNumberSave()
    {
        Param param = new Param();
        compet.Mode = Team.isOn;
        if (compet.Mode == true)
        {
            Team.isOn = true;
            compet.MaxMember = ContestTN_dbox.value + 2;
            param.CompetToTeam(compet.MaxMember)
                .CompetUpdate();
        }
        else
        {
            Team.isOn = false;
            compet.MaxMember = 1;
            param.CompetToSingle()
            .CompetUpdate();
        }
    }
    public void competInfoSave()
    {
        Competition compet = new Competition();
        Param param = new Param();
        compet.Name = ContestName_infT.text;
        compet.Info = ContestInfo_infT.text;
        compet.UserPass = int.Parse(authen_infT.text);
        param.CompetName(compet.Name)
            .CompetInfo(compet.Info)
            .CompetUserpass(compet.UserPass)
            .CompetUpdate();

        FTP.ImageServerRenameDir(AdminCurState.Instance.Competition, compet.Name);
    }
    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
}