using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class createmyrank : MonoBehaviour
{
    public string conname;
    public string rankTime;
    public int rankScore;

    public GameObject prefab, Content;//대회버튼, content화면
    int nextNumber, wordNumber;//대회버튼에 숫자, 단어개수인듯,,?
                               // Start is called before the first frame update
        void Start()
        {
            if (gameman.Instance.loadRankChek == true)
            {
                Debug.Log("ho");
                if (PlayerPrefs.HasKey("nextNumber"))
                    nextNumber = PlayerPrefs.GetInt("nextNumber");

                nextNumber++;
                GameObject go = Instantiate(prefab, prefab.transform.position, Quaternion.identity) as GameObject;
                go.transform.GetChild(0).transform.GetComponent<Text>().text = rankScore.ToString(); //점수

                string wordString = "대회이름";
                PlayerPrefs.SetString("words" + wordNumber, wordString);

                go.transform.GetChild(1).transform.GetComponent<Text>().text = rankTime; //number옆text
                go.transform.GetChild(2).transform.GetComponent<Text>().text = conname; //number옆text

                go.transform.SetParent(Content.transform);
                PlayerPrefs.SetInt("nextNumber", nextNumber);
                wordNumber++;
                PlayerPrefs.SetInt("wordNumber" + wordNumber, wordNumber);
           
                Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);
            }
            else
            {
                Debug.Log("nono");
            }

            //PlayerPrefs.DeleteAll();
            //deleted all playerprefs

            //wordNumber = PlayerPrefs.GetInt("wordNumber");
            wordNumber = PlayerPrefs.GetInt("nextNumber");
            Debug.Log(wordNumber);
            Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, wordNumber * 112);

            for (int i = 0; i < wordNumber; i++)
            {
                GameObject go = Instantiate(prefab, prefab.transform.position, Quaternion.identity) as GameObject;
                go.transform.GetChild(0).transform.GetComponent<Text>().text = rankScore.ToString(); //점수
                go.transform.GetChild(1).transform.GetComponent<Text>().text = rankTime; //number옆text
                go.transform.GetChild(2).transform.GetComponent<Text>().text = conname; //number옆text

                go.transform.SetParent(Content.transform);
            }
    }
}


