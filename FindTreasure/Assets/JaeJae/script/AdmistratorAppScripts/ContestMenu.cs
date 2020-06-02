using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ContestMenu : MonoBehaviour
{
    public ContestInput.ContestData contest;
    public GameObject ContestPanel;
    [ContextMenu("From Json Data")]
    void LoadContestDataFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "contestData.json");
        //string path = Path.Combine(Application.dataPath, "contestData.json");
        string jsonData = File.ReadAllText(path);
        //ContestInput.contestData = JsonUtility.FromJson<ContestInput.ContestData>(jsonData);
        //ContestInput.contest = JsonUtility.FromJson<ContestInput.ContestData>(jsonData);
        contest = JsonUtility.FromJson<ContestInput.ContestData>(jsonData);
    }

    [ContextMenu("To Json Data")]
    void SaveContestDataToJson()
    {
        //string jsonData = JsonUtility.ToJson(ContestInput.contestData, true);
        //string jsonData = JsonUtility.ToJson(ContestInput.contest, true);
        string jsonData = JsonUtility.ToJson(contest, true);
        //string path = Path.Combine(Application.dataPath, "contestData.json");
        string path = Path.Combine(Application.persistentDataPath, "contestData.json");
        File.WriteAllText(path, jsonData);
    }

    public InputField ContestName_infT;
    public InputField ContestTN_infT;
    public InputField ContestPw_infT;
    public Toggle Team;
    public Toggle individual;
    public void ChangeSceneToContestSettingChange()
    {
        //Application.LoadLevel("CSChange");//대회 정보 수정화면
        LoadContestDataFromJson();
        ContestPanel.SetActive(true);
        /*
        ContestName_infT.text = ContestInput.contestData.loadContestName;
        ContestTN_infT.text = ContestInput.contestData.loadTeamMNumber;
        ContestPw_infT.text = ContestInput.contestData.loadContestPW;
        if (ContestInput.contestData.indi == true)
        {
            individual.Select();
        }
        else if (ContestInput.contestData.indi == false)
        {
            Team.Select();
        }*/
        /*
        ContestName_infT.text = ContestInput.contest.loadContestName;
        ContestTN_infT.text = ContestInput.contest.loadTeamMNumber;
        ContestPw_infT.text = ContestInput.contest.loadContestPW;
        if (ContestInput.contest.indi = true)
        {
            individual.Select();
        }
        else if (ContestInput.contest.indi = false)
        {
            Team.Select();
        }*/
        ContestName_infT.text = contest.loadContestName;
        ContestTN_infT.text = contest.loadTeamMNumber;
        ContestPw_infT.text = contest.loadContestPW;
        if (contest.indi == true)
        {
            individual.isOn = true;
        }
        else if (contest.indi == false)
        {
            Team.isOn = true;
        }
    }
    public void Update()
    {

        if (Team.isOn)
        {
            //TeamMNumber.SetActive(true);
            ContestTN_infT.image.color = Color.white;
        }
        else if (individual.isOn)
        {
            ContestTN_infT.DeactivateInputField();
            ContestTN_infT.image.color = Color.gray;
            ContestTN_infT.text = "";
            //TeamMNumber.SetActive(false);
        }

    }
    public void Save()
    {
        /*
        ContestInput.contestData.loadContestName = ContestName_infT.text;
        ContestInput.contestData.loadTeamMNumber= ContestTN_infT.text;
        ContestInput.contestData.loadContestPW= ContestPw_infT.text;*/
        /*
        ContestInput.contest.loadContestName = ContestName_infT.text;
        ContestInput.contest.loadTeamMNumber = ContestTN_infT.text;
        ContestInput.contest.loadContestPW = ContestPw_infT.text;
        if (Team.isOn)
        {
            ContestInput.contest.indi = false;
        }
        else if (individual.isOn)
        {
            ContestInput.contest.indi = true;
        }
        */
        contest.loadContestName = ContestName_infT.text;
        contest.loadTeamMNumber = ContestTN_infT.text;
        contest.loadContestPW = ContestPw_infT.text;
        if (Team.isOn)
        {
            contest.indi = false;
        }
        else if (individual.isOn)
        {
            contest.indi = true;
        }
        SaveContestDataToJson();
    }
    public void Cancel()
    {
        LoadContestDataFromJson();
    }
}
