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
        string jsonData = File.ReadAllText(path);
        contest = JsonUtility.FromJson<ContestInput.ContestData>(jsonData);
    }

    [ContextMenu("To Json Data")]
    void SaveContestDataToJson()
    {
        string jsonData = JsonUtility.ToJson(contest, true);
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
            ContestTN_infT.image.color = Color.white;
        }
        else if (individual.isOn)
        {
            ContestTN_infT.DeactivateInputField();
            ContestTN_infT.image.color = Color.gray;
            ContestTN_infT.text = "";
        }

    }
    public void Save()
    {
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
