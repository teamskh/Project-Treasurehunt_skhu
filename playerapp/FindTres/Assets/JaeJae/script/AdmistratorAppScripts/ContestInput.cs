using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.UI;

public class ContestInput : MonoBehaviour
{
    public GameObject ContestPanel;
    public InputField ContestName_infT;
    public InputField ContestTN_infT;
    public InputField ContestPw_infT;
    public Toggle Team;
    public Toggle individual;
    public GameObject ContestBPrefab; //대회버튼
    public GameObject Content;
    //public GameObject TeamMNumber;
    public GameObject toggleChoice, all_t;
    int nextNumber, wordNumber;
    //List<GameObject> ContestList = new List<GameObject>();

    public void Start()
    {
        ContestPanel.SetActive(false);
        toggleChoice.SetActive(false);
        all_t.SetActive(false);

        wordNumber = PlayerPrefs.GetInt("wordNumber");

        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);

        for (int i = 0; i < wordNumber; i++)
        {
            GameObject CBPrefab = Instantiate(ContestBPrefab, ContestBPrefab.transform.position, Quaternion.identity) as GameObject;
            //CBPrefab.transform.GetChild(0).transform.GetComponent<Text>().text = "" + (i + 1);//number
            string loadContestName = PlayerPrefs.GetString("words" + i);//내어너니언어합친거버튼에 나타낼문장
            CBPrefab.transform.GetChild(0).GetComponent<Text>().text = loadContestName; //number옆text
            CBPrefab.transform.SetParent(Content.transform, false);
        }
    }
    public void Update()
    {
        
        if (Team.isOn)
        {
            //TeamMNumber.SetActive(true);
            ContestTN_infT.image.color = Color.white;
        }
        else if(individual.isOn)
        {
            ContestTN_infT.DeactivateInputField();
            ContestTN_infT.image.color=Color.gray;
            ContestTN_infT.text = "";
            //TeamMNumber.SetActive(false);
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
        }
    }
    /*
    public void AddContestList()
    {
        if(ContestName_infT.text!=""&&ContestTN_infT.text!=""&& ContestPw_infT.text != "")
        {

        }
    }*/
    
    public void Save()
    {
        //PlayerPrefs.SetString("ContestName", ContestName_infT.text);
        //PlayerPrefs.SetString("TeamMember", ContestTN_infT.text);
        //PlayerPrefs.SetString("ContestPassword", ContestPw_infT.text);
        if (ContestName_infT.text == "" || ContestTN_infT.text == "" || ContestPw_infT.text== "")
        {
            all_t.SetActive(true);
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            //Destroy(this.all_t, 3f)
            ContestPanel.SetActive(true);
        }
        else
        {
            if (Team.isOn)
            {
                if (ContestName_infT.text != "" && ContestTN_infT.text != "" && ContestPw_infT.text != "")
                {
                    if (PlayerPrefs.HasKey("nextNumber"))
                        PlayerPrefs.GetInt("nextNumber", nextNumber);
                    nextNumber++;
                    GameObject CBPrefab = Instantiate(ContestBPrefab, ContestBPrefab.transform.position,
                        Quaternion.identity) as GameObject;
                    //CBPrefab.transform.GetChild(0).transform.GetComponent<Text>().text=""+nextNumber;
                    //string loadContestName=PlayerPrefs.GetString
                    string loadContestName = ContestName_infT.text;
                    string loadTeamMNumber = ContestTN_infT.text;
                    string loadContestPW = ContestPw_infT.text;

                    PlayerPrefs.SetString("ContestPassword" + wordNumber, loadContestPW);
                    PlayerPrefs.SetString("TeamMember" + wordNumber, loadTeamMNumber);
                    PlayerPrefs.SetString("words" + wordNumber, loadContestName);
                    CBPrefab.transform.GetChild(0).transform.GetComponent<Text>().text = loadContestName;
                    ContestName_infT.text = "";
                    ContestTN_infT.text = "";
                    ContestPw_infT.text = "";
                    CBPrefab.transform.SetParent(Content.transform, false);
                    //PlayerPrefs.SetInt("nextNumber", nextNumber);
                    wordNumber++;
                    PlayerPrefs.SetInt("wordNumber" + wordNumber, wordNumber);
                    Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);
                }
                else
                {
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
                    string loadContestName = ContestName_infT.text;
                    string loadTeamMNumber = ContestTN_infT.text;
                    string loadContestPW = ContestPw_infT.text;

                    PlayerPrefs.SetString("ContestPassword" + wordNumber, loadContestPW);
                    PlayerPrefs.SetString("TeamMember" + wordNumber, loadTeamMNumber);
                    PlayerPrefs.SetString("words" + wordNumber, loadContestName);
                    CBPrefab.transform.GetChild(0).transform.GetComponent<Text>().text = loadContestName;
                    ContestName_infT.text = "";
                    ContestTN_infT.text = "";
                    ContestPw_infT.text = "";
                    CBPrefab.transform.SetParent(Content.transform, false);
                    PlayerPrefs.SetInt("nextNumber", nextNumber);
                    wordNumber++;
                    PlayerPrefs.SetInt("wordNumber" + wordNumber, wordNumber);
                    Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);
                }
            }
            else
            {
                toggleChoice.SetActive(true);
                Destroy(toggleChoice, 1);
            }
        }

    }

    public void Cancel()
    {
        ContestName_infT.text = "";
        ContestTN_infT.text = "";
        ContestPw_infT.text = "";
    }

    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
    
    /*
     public void Load()
     {
         ContestName_infT.text = PlayerPrefs.GetString("ContestName");
         ContestTN_infT.text = PlayerPrefs.GetInt("TeamMember").ToString();
         ContestPw_infT.text = PlayerPrefs.GetString("ContestPassword");
     }*/
}
