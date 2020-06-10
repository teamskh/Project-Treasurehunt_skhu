using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ContestInput : MonoBehaviour
{
    public GameObject ContestPanel;
    public InputField ContestName_infT;
    public InputField ContestTN_infT;//inputfield
    public Dropdown ContestTn_DD;//dropdown
    public InputField ContestPw_infT;
    public Toggle Team;
    public Toggle individual;
    public GameObject ContestBPrefab; //대회버튼
    public GameObject Content;
    public GameObject panel;
    public GameObject toggleChoice, all_t;
    int nextNumber;
    List<string> num = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9" };

    [System.Serializable]
    public class ContestData
    {
        public int wordNumber;
        public string loadContestName;
        public string loadTeamMNumber = "2";
        public string loadContestPW;
        public bool indi = false;
        public ContestData(int wordNumber, string loadContestName, string loadTeamMNumber, string loadContestPW, bool indi)
        {
            this.wordNumber = wordNumber;
            this.loadContestName = loadContestName;
            this.loadTeamMNumber = loadTeamMNumber;
            this.loadContestPW = loadContestPW;
            this.indi = indi;
        }
     }
    public List<ContestData> contestList = new List<ContestData>();
    
    public ContestData contestData;
    [ContextMenu("To Json Data")]
    void SaveContestDataToJson()
    {
        string jsonData = JsonUtility.ToJson(contestList, true);
        string path = Path.Combine(Application.persistentDataPath, "contestData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadContestDataFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "contestData.json");
        string jsonData = File.ReadAllText(path);
        contestData = JsonUtility.FromJson<ContestData>(jsonData);
    }
   
    public void Start()
    {
        
        ContestPanel.SetActive(false);
        toggleChoice.SetActive(false);
        all_t.SetActive(false);

        //wordNumber = PlayerPrefs.GetInt("wordNumber");
        contestData.wordNumber = PlayerPrefs.GetInt("wordNumber");

        //transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, contestData.wordNumber * 112);

        LoadContestDataFromJson();
        //for (int i = 0; i < wordNumber; i++)
        for (int i = 0; i < contestData.wordNumber; i++)
        {
            GameObject CBPrefab = Instantiate(ContestBPrefab, ContestBPrefab.transform.position, Quaternion.identity) as GameObject;
            //CBPrefab.transform.GetChild(0).transform.GetComponent<Text>().text = "" + (i + 1);//number
            //string loadContestName = PlayerPrefs.GetString("words" + i);//내어너니언어합친거버튼에 나타낼문장
            CBPrefab.transform.GetChild(1).GetComponent<Text>().text = contestData.loadContestName; //number옆text
            CBPrefab.transform.SetParent(Content.transform);
        }
        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, contestData.wordNumber * 155);
    }
    public void Update()
    {
        
        if (Team.isOn)
        {
            panel.SetActive(false);
        }
        else if(individual.isOn)
        {
            panel.SetActive(true);
        }

    }
    public void AddList()
    {
        if (ContestPanel.activeSelf == true)
        {
            ContestPanel.SetActive(false);
        }
        else if (ContestPanel.activeSelf == false)
        {
            ContestPanel.SetActive(true);
            contestData.loadTeamMNumber = "2";
            HandleInputData();
        }
    }
   
    
    public void Save()
    {
        if (Team.isOn)
            {
            //if (ContestName_infT.text != "" && ContestTN_infT.text != "" && ContestPw_infT.text != "")
            if (ContestName_infT.text != "" && ContestPw_infT.text != "")
            {
                    if (PlayerPrefs.HasKey("nextNumber"))
                       nextNumber= PlayerPrefs.GetInt("nextNumber", nextNumber);
                    nextNumber++;
                    GameObject CBPrefab = Instantiate(ContestBPrefab, ContestBPrefab.transform.position,
                        Quaternion.identity) as GameObject;
                    //CBPrefab.transform.GetChild(0).transform.GetComponent<Text>().text=""+nextNumber;
                    //string loadContestName=PlayerPrefs.GetString
                    contestData.loadContestName = ContestName_infT.text;
                    //contestData.loadTeamMNumber = ContestTN_infT.text;
                    contestData.loadContestPW = ContestPw_infT.text;
                    contestData.indi = false;
                /*
                    PlayerPrefs.SetString("words" + wordNumber, contestData.loadContestName);
                    PlayerPrefs.SetString("TeamMember" + wordNumber, contestData.loadTeamMNumber);
                    PlayerPrefs.SetString("ContestPassword" + wordNumber, contestData.loadContestPW);
                    */
    /*
    PlayerPrefs.SetString("words" + contestData.wordNumber, contestData.loadContestName);
    PlayerPrefs.SetString("TeamMember" + contestData.wordNumber, contestData.loadTeamMNumber);
    PlayerPrefs.SetString("ContestPassword" + contestData.wordNumber, contestData.loadContestPW);
    */

                CBPrefab.transform.GetChild(1).transform.GetComponent<Text>().text = contestData.loadContestName;
                    ContestName_infT.text = "";
                    ContestTN_infT.text = "";
                    ContestPw_infT.text = "";
                    CBPrefab.transform.SetParent(Content.transform, false);
                //PlayerPrefs.SetInt("nextNumber", nextNumber);
                //wordNumber++;
                contestData.wordNumber++;
                //PlayerPrefs.SetInt("wordNumber" + wordNumber, wordNumber);
                PlayerPrefs.SetInt("wordNumber" + contestData.wordNumber, contestData.wordNumber);
                //Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);
                
                for (int i = 0; i < contestData.wordNumber; i++)
                {
                    SaveContestDataToJson();
                }
                Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, contestData.wordNumber * 155);
            }
                else
                {
                     all_t.SetActive(true);
                    StartCoroutine(setActiveObjinSecond(all_t, 1f));
                    //Destroy(this.all_t, 3f)
                    ContestPanel.SetActive(true);
                    Debug.Log("Empty");
                }
                /*
                Count++;
                Instantiate(ContestBPrefab, new Vector3(0, Count * 10, 0), Quaternion.identity);
                ContestList.Add(ContestBPrefab.gameObject);
                string loadContestName = PlayerPrefs.GetString("ContestName");*/

            }
            else if (individual.isOn)
            {
            if (ContestName_infT.text != "" && ContestPw_infT.text != "")
                {
                    if (PlayerPrefs.HasKey("nextNumber"))
                        PlayerPrefs.GetInt("nextNumber", nextNumber);
                    nextNumber++;
                    GameObject CBPrefab = Instantiate(ContestBPrefab, ContestBPrefab.transform.position,
                        Quaternion.identity) as GameObject;
                    //CBPrefab.transform.GetChild(0).transform.GetComponent<Text>().text=""+nextNumber;
                    //string loadContestName=PlayerPrefs.GetString
                    /*
                    string loadContestName = ContestName_infT.text;
                    string loadTeamMNumber = ContestTN_infT.text;
                    string loadContestPW = ContestPw_infT.text;*/

                    contestData.indi = true;
                contestData.loadContestName = ContestName_infT.text;
                contestData.loadTeamMNumber = "1";
                contestData.loadContestPW = ContestPw_infT.text;
                /*
                    PlayerPrefs.SetString("ContestPassword" + wordNumber, loadContestPW);
                    PlayerPrefs.SetString("TeamMember" + wordNumber, loadTeamMNumber);
                    PlayerPrefs.SetString("words" + wordNumber, loadContestName);*/
    /*
    PlayerPrefs.SetString("ContestPassword" + contestData.wordNumber, loadContestPW);
    PlayerPrefs.SetString("TeamMember" + contestData.wordNumber, loadTeamMNumber);
    PlayerPrefs.SetString("words" + contestData.wordNumber, loadContestName);
    */

                CBPrefab.transform.GetChild(1).transform.GetComponent<Text>().text = contestData.loadContestName;
                ContestName_infT.text = "";
                    ContestTN_infT.text = "";
                    ContestPw_infT.text = "";
                    CBPrefab.transform.SetParent(Content.transform, false);
                    PlayerPrefs.SetInt("nextNumber", nextNumber);

                contestData.wordNumber++;
                PlayerPrefs.SetInt("wordNumber" + contestData.wordNumber, contestData.wordNumber);
                for (int i = 0; i < contestData.wordNumber; i++)
                {
                    SaveContestDataToJson();
                }
                Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, contestData.wordNumber * 155);
            }
                else
                {
                all_t.SetActive(true);
                StartCoroutine(setActiveObjinSecond(all_t, 1f));
                ContestPanel.SetActive(true);
                Debug.Log("Empty");
                 }
             }
            else
            {
                toggleChoice.SetActive(true);
                Destroy(toggleChoice, 1);
            }

        ContestTn_DD.ClearOptions();
    }

    public void Cancel()
    {
        ContestName_infT.text = "";
        ContestTN_infT.text = "";
        ContestPw_infT.text = "";
        contestData.loadTeamMNumber = "";
        ContestTn_DD.ClearOptions();
    }

    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
    public void HandleInputData()
    {
        ContestTn_DD.AddOptions(num);
    }
    public void DropDown_Change(int index)
    {
        contestData.loadTeamMNumber = num[index];
    }
}
