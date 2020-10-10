using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class createmyrank : MonoBehaviour
{
    public GameObject CompetR;
    private List<string> curlist;
    private List<GameObject> buttons = new List<GameObject>();
    public List<string> ConName;

    public void Start()
    {


        //PlayerPrefs.DeleteAll();

            /*GameObject go = Instantiate(Rprefab, Rprefab.transform.position, Quaternion.identity) as GameObject;
            go.transform.GetChild(0).transform.GetComponent<Text>().text = PlayerPrefs.GetString("ConName");
            go.transform.GetChild(1).transform.GetComponent<Text>().text = PlayerPrefs.GetString("Times");
            go.transform.GetChild(2).transform.GetComponent<Text>().text = PlayerPrefs.GetString("Score");
            Adapter.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);*/
        /*if (gameman.Instance.loadRankChek == true)
        {
            nextNumber++;

            if (PlayerPrefs.HasKey("nextNumber"))
                nextNumber = PlayerPrefs.GetInt("nextNumber");

            GameObject go = Instantiate(prefab, prefab.transform.position, Quaternion.identity) as GameObject;
            
            string wordString = conname;
            Debug.Log("****");
            PlayerPrefs.SetString("words" + wordNumber, wordString);

            Debug.Log("*5***");
            Debug.Log(go.activeInHierarchy);
            Debug.Log(go.activeSelf);

            go.transform.GetChild(0).transform.GetComponent<Text>().text = conname; //대회 이름
            go.transform.GetChild(1).transform.GetComponent<Text>().text = rankTime; //시간
            go.transform.GetChild(2).transform.GetComponent<Text>().text = rankScore.ToString(); //점수

            Debug.Log("**8*");
            go.transform.SetParent(Content.transform);
            Debug.Log("**9*");
            PlayerPrefs.SetInt("nextNumber", nextNumber);
            wordNumber++;
            PlayerPrefs.SetInt("wordNumber" + wordNumber, wordNumber);

            Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);
            gameman.Instance.loadRankChek = false;
            
        }

        //PlayerPrefs.DeleteAll();//deleted all playerprefs

        wordNumber = PlayerPrefs.GetInt("nextNumber");
        Debug.Log("**"+wordNumber);
        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);

        for (int i = 0; i < wordNumber; i++)
        {
            GameObject go = Instantiate(prefab, prefab.transform.position, Quaternion.identity) as GameObject;
            go.transform.GetChild(0).transform.GetComponent<Text>().text = conname; //대회 이름
            go.transform.GetChild(1).transform.GetComponent<Text>().text = rankTime; //시간
            go.transform.GetChild(2).transform.GetComponent<Text>().text = rankScore.ToString(); //점수

            go.transform.SetParent(Content.transform);
        }
        */
    }

    private void OnEnable()
    {
        curlist = PlayerContents.Instance.CompetitionList();
        foreach (string title in curlist)
        {
            GameObject b = Instantiate(CompetR, transform);
            b.GetComponent<RectTransform>().anchoredPosition.Set(0, (buttons.Count + 1) * 60);
            foreach (Text t in b.transform.GetComponentsInChildren<Text>(true))
            {
                if (t.gameObject.layer == 8)
                {
                    t.text = title;
                    ConName.Add(title);
                }
            }
        }
    }
}


