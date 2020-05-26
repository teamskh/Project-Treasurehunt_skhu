using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ContestController : MonoBehaviour
{
    public ContestData contestData;
    [ContextMenu("To Json Data")]
    void SaveContestDataToJson()
    {
        string jsonData = JsonUtility.ToJson(contestData,true);
        string path=Path.Combine(Application.persistentDataPath,"contestData.json");
        File.WriteAllText(path, jsonData);
    }
    void LoadContestDataFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "contestData.json");
        string jsonData=File.ReadAllText(path);
        contestData= JsonUtility.FromJson<ContestData>(jsonData);
    }
}
[System.Serializable]
public class ContestData
{
    public InputField ContestName_infT;
    public InputField ContestTN_infT;
    public InputField ContestPw_infT;
    public bool True;
    public GameObject ContestBPrefab;
    //public string[] 
}
